using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter language(s) code (e.g., eng, chi_sim, chi_tra, jpn, or combos like eng+chi_sim):");
        string lang = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lang)) lang = "eng"; // default

        Console.WriteLine("Enter image file name (e.g., Taiwan.png):");
        string imageFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(imageFile)) imageFile = "Taiwan.png";

        var process = new Process();
        process.StartInfo.FileName = "tesseract";
        process.StartInfo.Arguments = $"{imageFile} stdout -l {lang} --tessdata-dir ./tessdata";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine("Text detected:");
        Console.WriteLine(output);
    }
}



// WINDOWS VERSION BELOW

// using System;
// using System.Diagnostics;
// using System.IO;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Tesseract OCR Application");
//         Console.WriteLine("-------------------------");

//         // --- Get Tesseract Executable Path ---
//         string tesseractExePath = FindTesseractExecutable();

//         if (string.IsNullOrEmpty(tesseractExePath))
//         {
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.WriteLine("\nError: Tesseract executable not found!");
//             Console.WriteLine("Please ensure Tesseract is installed and:");
//             Console.WriteLine("1. Its installation directory is added to your system's PATH environment variable, OR");
//             Console.WriteLine("2. It's installed in the default location (e.g., C:\\Program Files\\Tesseract-OCR\\tesseract.exe).");
//             Console.ResetColor();
//             Console.WriteLine("\nPress any key to exit.");
//             Console.ReadKey();
//             return;
//         }

//         Console.WriteLine($"Tesseract found at: {tesseractExePath}\n");

//         // --- User Input for OCR ---
//         Console.WriteLine("Enter language(s) code (e.g., eng, chi_sim, jpn, or combos like eng+chi_sim):");
//         string lang = Console.ReadLine();
//         if (string.IsNullOrWhiteSpace(lang))
//         {
//             lang = "eng"; // default language
//             Console.WriteLine("Using default language: eng");
//         }

//         Console.WriteLine("\nEnter image file name (e.g., Taiwan.png):");
//         Console.WriteLine("  (Make sure the image file is in the same directory as this program, or provide a full path)");
//         string imageFile = Console.ReadLine();
//         if (string.IsNullOrWhiteSpace(imageFile))
//         {
//             imageFile = "Taiwan.png"; // default image file
//             Console.WriteLine("Using default image file: Taiwan.png");
//         }

//         // Check if the image file exists
//         if (!File.Exists(imageFile))
//         {
//             Console.ForegroundColor = ConsoleColor.Yellow;
//             Console.WriteLine($"\nWarning: Image file '{imageFile}' not found in the current directory.");
//             Console.WriteLine("Please ensure the image file exists and is accessible.");
//             Console.ResetColor();
//             // You might want to exit here or prompt again
//             Console.WriteLine("\nPress any key to exit.");
//             Console.ReadKey();
//             return;
//         }

//         // --- Execute Tesseract ---
//         Console.WriteLine("\nRunning Tesseract OCR...");
//         try
//         {
//             var process = new Process();
//             process.StartInfo.FileName = tesseractExePath; // Use the found path
//             // Tesseract arguments: <input_file> stdout -l <language> --tessdata-dir <path_to_tessdata>
//             // We assume tessdata is in a 'tessdata' folder relative to the executable or the working directory
//             process.StartInfo.Arguments = $"{imageFile} stdout -l {lang} --tessdata-dir \"{Path.GetDirectoryName(tesseractExePath)}\\tessdata\"";
//             process.StartInfo.RedirectStandardOutput = true;
//             process.StartInfo.UseShellExecute = false; // Required for redirection
//             process.StartInfo.CreateNoWindow = true; // Don't show the command prompt window

//             process.Start();

//             // Read the output
//             string output = process.StandardOutput.ReadToEnd();
//             process.WaitForExit(); // Wait for Tesseract to finish

//             Console.WriteLine("\n--- Text Detected ---");
//             Console.WriteLine(output);
//             Console.WriteLine("---------------------");
//         }
//         catch (System.ComponentModel.Win32Exception ex)
//         {
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.WriteLine($"\nAn error occurred trying to start Tesseract: {ex.Message}");
//             Console.WriteLine("This usually means the Tesseract executable was not found or could not be launched.");
//             Console.ResetColor();
//         }
//         catch (Exception ex)
//         {
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
//             Console.ResetColor();
//         }

//         Console.WriteLine("\nOCR process complete. Press any key to exit.");
//         Console.ReadKey();
//     }

//     /// <summary>
//     /// Attempts to find the Tesseract executable in common locations.
//     /// </summary>
//     /// <returns>The full path to tesseract.exe if found, otherwise null.</returns>
//     static string FindTesseractExecutable()
//     {
//         string tesseractExeName = "tesseract.exe"; // For Windows
//         string tesseractExePath = null;

//         // 1. Check if Tesseract is in the system's PATH
//         try
//         {
//             using (var process = new Process())
//             {
//                 process.StartInfo.FileName = tesseractExeName;
//                 process.StartInfo.UseShellExecute = false;
//                 process.StartInfo.RedirectStandardOutput = true;
//                 process.StartInfo.CreateNoWindow = true;
//                 process.Start();
//                 process.WaitForExit(1000); // Wait up to 1 second to see if it starts
//                 if (process.ExitCode == 0 || process.ExitCode == 1) // 0 for help, 1 for no arguments
//                 {
//                     // If it starts, we assume it's in PATH and can be called directly
//                     return tesseractExeName;
//                 }
//             }
//         }
//         catch (System.ComponentModel.Win32Exception)
//         {
//             // This exception means "tesseract" wasn't found in PATH
//         }

//         // 2. Check common Windows installation paths
//         string[] commonPaths =
//         {
//             @"C:\Program Files\Tesseract-OCR\",
//             @"C:\Program Files (x86)\Tesseract-OCR\",
//             @"C:\Tesseract-OCR\"
//         };

//         foreach (var path in commonPaths)
//         {
//             string fullPath = Path.Combine(path, tesseractExeName);
//             if (File.Exists(fullPath))
//             {
//                 tesseractExePath = fullPath;
//                 break;
//             }
//         }

//         return tesseractExePath;
//     }
// }