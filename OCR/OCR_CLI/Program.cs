using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter language(s) code (e.g., eng, chi_sim, chi_tra, jpn, or combos like eng+chi_sim):");
        string lang = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lang)) lang = "eng";

        Console.WriteLine("Enter image or PDF file name (e.g., Taiwan.png or doc.pdf):");
        string inputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputFile)) 
        {
            Console.WriteLine("No file name provided, exiting.");
            return;
        }

        string imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "images");
        string inputPath = Path.Combine(imagesDir, inputFile);
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: cannot read input file {inputPath}");
            return;
        }

        string tempImagePath = Path.Combine(imagesDir, "converted_temp.png");

        // If it's a PDF, convert the first page to PNG using ImageMagick
        if (inputFile.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            // Convert first PDF page to high-resolution PNG using ImageMagick
            Console.WriteLine("Detected PDF. Converting first page to image...");
            var convert = Process.Start("convert",
                $"-density 300 \"{inputPath}[0]\" -colorspace RGB " +
                "-normalize -deskew 40% -sharpen 0x1 " +
                $"\"{tempImagePath}\"");
            convert.WaitForExit();
            if (convert.ExitCode != 0)
            {
                Console.WriteLine("Error converting PDF to image.");
                return;
            }
            inputPath = tempImagePath;
        }

        // Run Tesseract OCR
        Console.WriteLine("Running Tesseract...");
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "tesseract",
                Arguments = $"\"{inputPath}\" stdout -l {lang} " +
                            $"--oem 1 --psm 6 --tessdata-dir \"{Path.Combine(Directory.GetCurrentDirectory(), "tessdata")}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            }
        };
        proc.Start();
        string output = proc.StandardOutput.ReadToEnd();
        proc.WaitForExit();

        Console.WriteLine("Text detected:");
        Console.WriteLine(output);
    }
}