using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace VulnerableService
{
    public class TaskRunner
    {
        FlagGenerator flagGenerator = new FlagGenerator();

        public void RunBackup()
        {
            string backupLocation = @"C:\DevTools\Backup.exe";

            if (File.Exists(backupLocation))
            {
                if (!File.Exists(@"C:\DevTools\BackupSolved.txt"))
                {
                    string challengeFlag = flagGenerator.GenerateFlag("BackupSolved");

                    System.IO.File.WriteAllText(@"C:\DevTools\BackupSolved.txt", challengeFlag);

                    Process.Start(backupLocation);
                }
            }
        }

        public void RunHelperFunctions()
        {
            string helperDllLocation = @"C:\DevTools\Helper.dll";

            if (File.Exists(helperDllLocation))
            {
                if (!File.Exists(@"C:\DevTools\HelperSolved.txt"))
                {
                    string challengeFlag = flagGenerator.GenerateFlag("HelperSolved");

                    System.IO.File.WriteAllText(@"C:\DevTools\HelperSolved.txt", challengeFlag);

                    Process helperDllProcess = new Process();

                    ProcessStartInfo helperStartInfo = new ProcessStartInfo("rundll32.exe");
                    helperStartInfo.UseShellExecute = false;
                    helperStartInfo.RedirectStandardOutput = false;
                    helperStartInfo.Arguments = @"C:\DevTools\Helper.dll,EntryPoint";

                    helperDllProcess.StartInfo = helperStartInfo;

                    helperDllProcess.Start();
                }
            }
        }

        public void RunRegistryCommand()
        {
            string executablePath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\VulnerableService", "CleanupScriptLocation", null);

            if (executablePath != null)
            {
                if (File.Exists(executablePath))
                {
                    if (!File.Exists(@"C:\DevTools\CleanupSolved.txt"))
                    {
                        string challengeFlag = flagGenerator.GenerateFlag("CleanupSolved");

                        System.IO.File.WriteAllText(@"C:\DevTools\CleanupSolved.txt", challengeFlag);

                        Process.Start(executablePath);
                    }
                }
            }
        }
    }
}
