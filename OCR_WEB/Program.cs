using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:80");
var app = builder.Build();

// Serve the upload form at "/"
app.MapGet("/", () =>
{
    const string html = @"<!DOCTYPE html>
<html>
<head><title>OCR Demo</title></head>
<body>
  <h1>Upload PDF or Image for OCR</h1>
  <form method='post' action='/upload' enctype='multipart/form-data'>
    <input type='file' name='file' accept='image/*,.pdf' required /><br/>
    <input type='text' name='lang' value='eng' /><br/>
    <button type='submit'>Extract Text</button>
  </form>
</body>
</html>";
    return Results.Content(html, "text/html");
});

// Handle the uploaded file
app.MapPost("/upload", async (HttpRequest req) =>
{
    var form = await req.ReadFormAsync();
    var file = form.Files.GetFile("file");
    var lang = form["lang"].ToString() ?? "eng";
    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded");

    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    Directory.CreateDirectory(uploads);
    var filePath = Path.Combine(uploads, file.FileName);
    await using var fs = System.IO.File.Create(filePath);
    await file.CopyToAsync(fs);

    var inputPath = filePath;

    var tessdataDir = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");

    // ---------------- PDF handling (one page at a time) ----------------
    if (file.ContentType == "application/pdf" || file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Detected PDF. Rendering pages one-by-one at 300 DPI...");

        // Determine number of pages via pdfinfo
        var infoProc = Process.Start(new ProcessStartInfo
        {
            FileName = "pdfinfo",
            Arguments = $"\"{inputPath}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false
        });
        string info = await infoProc.StandardOutput.ReadToEndAsync();
        infoProc.WaitForExit();

        var match = Regex.Match(info, @"Pages:\s+(\d+)", RegexOptions.IgnoreCase);
        int pageCount = match.Success ? int.Parse(match.Groups[1].Value) : 1;

        var sb = new System.Text.StringBuilder();

        for (int p = 1; p <= pageCount; p++)
        {
            var baseName = Path.Combine(uploads, $"page-{p:D3}");
            // Render single page
            var pageProc = Process.Start("pdftoppm",
                $"-png -r 300 -f {p} -l {p} -singlefile \"{inputPath}\" \"{baseName}\"");
            pageProc.WaitForExit();

            var imgFile = baseName + ".png";
            if (!File.Exists(imgFile))
                continue;

            // OCR this page
            var tessProc = Process.Start(new ProcessStartInfo
            {
                FileName = "tesseract",
                Arguments = $"\"{imgFile}\" stdout -l {lang} --oem 1 --psm 6 --tessdata-dir \"{tessdataDir}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            sb.AppendLine(await tessProc.StandardOutput.ReadToEndAsync());
            tessProc.WaitForExit();
            sb.AppendLine("\n--- End of page ---\n");
        }

        return Results.Text(sb.ToString());
    }

    // ---------------- Single-image fallback ----------------
    {
        var tessProc = Process.Start(new ProcessStartInfo
        {
            FileName = "tesseract",
            Arguments = $"\"{inputPath}\" stdout -l {lang} --oem 1 --psm 6 --tessdata-dir \"{tessdataDir}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false
        });
        string imgText = await tessProc.StandardOutput.ReadToEndAsync();
        tessProc.WaitForExit();
        return Results.Text(imgText);
    }
});

app.Run();