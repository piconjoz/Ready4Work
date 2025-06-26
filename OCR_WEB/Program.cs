using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

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
    var tempImage = Path.Combine(uploads, "tmp.png");
    if (file.ContentType == "application/pdf" || file.FileName.EndsWith(".pdf"))
    {
        Console.WriteLine("Detected PDF. Converting first page to high-resolution image...");
        var p = Process.Start("convert",
            $"-density 300 \"{inputPath}[0]\" " +
            "-colorspace RGB -normalize -deskew 40% -sharpen 0x1 " +
            $"\"{tempImage}\"");
        p.WaitForExit();
        if (p.ExitCode != 0)
        {
            Console.WriteLine("Error converting PDF to image.");
            return Results.Text("Error converting PDF to image.");
        }
        inputPath = tempImage;
    }

    var proc = Process.Start(new ProcessStartInfo
    {
        FileName = "tesseract",
        Arguments = $"\"{inputPath}\" stdout -l {lang} --oem 1 --psm 6 --tessdata-dir \"{Path.Combine(Directory.GetCurrentDirectory(), "tessdata")}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false
    });
    string text = await proc.StandardOutput.ReadToEndAsync();
    proc.WaitForExit();

    return Results.Text(text);
});

app.Run();