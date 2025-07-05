using backend.Components.Resume.DTOs;
using backend.Components.Resume.Models;
using backend.Components.Resume.Repository;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace backend.Components.Resume.Services;

public class ResumeService : IResumeService
{
    private readonly IResumeRepository _repo;
    private readonly IWebHostEnvironment _env;

    public ResumeService(IResumeRepository repo, IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }

    public async Task<ResumeResponseDto> UploadAsync(int applicantId, UploadResumeDto dto)
    {
        // Override: delete any existing resume for this applicant
        await _repo.DeleteByApplicantIdAsync(applicantId);

        // 1. Save file to disk
        var uploads = Path.Combine(_env.ContentRootPath, "uploads");
        Directory.CreateDirectory(uploads);
        var fileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
        var filePath = Path.Combine(uploads, fileName);
        await using (var stream = File.Create(filePath))
            await dto.File.CopyToAsync(stream);

        // 2. Extract text, handling multi-page PDFs
        var tessdataDir = Path.Combine(_env.ContentRootPath, "tessdata");
        var sb = new StringBuilder();

        if (filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            // Determine page count via pdfinfo
            var infoProc = Process.Start(new ProcessStartInfo
            {
                FileName = "pdfinfo",
                Arguments = $"\"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            }) ?? throw new InvalidOperationException("Could not start pdfinfo process");
            string info = await infoProc.StandardOutput.ReadToEndAsync();
            infoProc.WaitForExit();

            var match = Regex.Match(info, @"Pages:\s+(\d+)", RegexOptions.IgnoreCase);
            int pageCount = match.Success ? int.Parse(match.Groups[1].Value) : 1;

            for (int p = 1; p <= pageCount; p++)
            {
                var baseName = Path.Combine(uploads, $"page-{p:D3}");
                // Render single page to PNG
                var pageProc = Process.Start("pdftoppm", $"-png -r 300 -f {p} -l {p} -singlefile \"{filePath}\" \"{baseName}\"")
                    ?? throw new InvalidOperationException($"Could not start pdftoppm for page {p}");
                pageProc.WaitForExit();
                var imgFile = baseName + ".png";
                if (!File.Exists(imgFile)) continue;

                // OCR this page
                var tessProc = Process.Start(new ProcessStartInfo
                {
                    FileName = "tesseract",
                    Arguments = $"\"{imgFile}\" stdout -l eng --oem 1 --psm 6",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }) ?? throw new InvalidOperationException("Could not start tesseract process for PDF page");
                sb.AppendLine(await tessProc.StandardOutput.ReadToEndAsync());
                tessProc.WaitForExit();
                sb.AppendLine("\n--- End of page ---\n");
            }
        }
        else
        {
            // Single-image fallback
            var tessProc = Process.Start(new ProcessStartInfo
            {
                FileName = "tesseract",
                Arguments = $"\"{filePath}\" stdout -l eng --oem 1 --psm 6",
                RedirectStandardOutput = true,
                UseShellExecute = false
            }) ?? throw new InvalidOperationException("Could not start tesseract process for fallback");
            sb.AppendLine(await tessProc.StandardOutput.ReadToEndAsync());
            tessProc.WaitForExit();
        }

        var text = sb.ToString();

        // 3. Persist
        var resume = new backend.Components.Resume.Models.Resume
        {
            ApplicantId = applicantId,
            ResumePath = filePath,
            ResumeText = text,
            UploadedAt = DateTime.UtcNow
        };
        var saved = await _repo.CreateAsync(resume);

        // 4. Return DTO
        return new ResumeResponseDto
        {
            ResumeId    = saved.ResumeId,
            ApplicantId = saved.ApplicantId,
            ResumePath  = saved.ResumePath,
            ResumeText  = saved.ResumeText,
            UploadedAt  = saved.UploadedAt
        };
    }

    public async Task<IEnumerable<ResumeResponseDto>> ListByApplicantAsync(int applicantId)
    {
        var list = await _repo.GetByApplicantIdAsync(applicantId);
        return list.Select(r => new ResumeResponseDto
        {
            ResumeId    = r.ResumeId,
            ApplicantId = r.ApplicantId,
            ResumePath  = r.ResumePath,
            ResumeText  = r.ResumeText,
            UploadedAt  = r.UploadedAt
        });
    }

    public async Task DeleteAsync(int resumeId)
    {
        await _repo.DeleteAsync(resumeId);
    }
}