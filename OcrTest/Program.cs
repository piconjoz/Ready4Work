using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter language(s) code (e.g., eng, chi_sim, chi_tra, jpn, or combos like eng+chi_sim):");
        string lang = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lang)) lang = "eng"; // default

        Console.WriteLine("Enter image or PDF file name (e.g., Taiwan.png or doc.pdf):");
        string inputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputFile)) inputFile = "Taiwan.png";

        string inputPath = Path.Combine("images", inputFile);
        string tempImagePath = Path.Combine("images", "converted_temp.png");

        // If it's a PDF, convert to PNG first
        if (inputFile.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Detected PDF. Converting first page to image...");
            var convertProc = new Process();
            convertProc.StartInfo.FileName = "convert";
            convertProc.StartInfo.Arguments = $"\"{inputPath}[0]\" \"{tempImagePath}\""; // Convert first page
            convertProc.StartInfo.UseShellExecute = false;
            convertProc.StartInfo.RedirectStandardOutput = true;
            convertProc.StartInfo.RedirectStandardError = true;
            convertProc.Start();
            string convertOutput = convertProc.StandardOutput.ReadToEnd();
            string convertError = convertProc.StandardError.ReadToEnd();
            convertProc.WaitForExit();

            if (convertProc.ExitCode != 0)
            {
                Console.WriteLine("Error converting PDF to image:");
                Console.WriteLine(convertError);
                return;
            }

            inputPath = tempImagePath;
        }

        var process = new Process();
        process.StartInfo.FileName = "tesseract";
        process.StartInfo.Arguments = $"{inputPath} stdout -l {lang} --tessdata-dir ./tessdata";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine("Text detected:");
        Console.WriteLine(output);
    }
}