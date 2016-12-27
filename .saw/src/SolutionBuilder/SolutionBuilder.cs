namespace Microsoft.Ciqs.Saw.Builder
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Ciqs.Saw.Common;

    public class SolutionBuilder: AbstractSawPhase
    {
        private string path;
        private string packagesDirectory;
        private Action<int, string> defaultExitAction = 
            (exitCode, output) =>
            {
                if (exitCode == 0)
                {
                    Console.WriteLine("done");
                }
                else
                {
                    Console.WriteLine("failed");
                    throw new SawException(output);
                }
            };
        
        private const string sourceDirectoryName = "src";

        public SolutionBuilder(string path, string packagesDirectory)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }

            this.path = path;
            this.packagesDirectory = packagesDirectory;
        }

        public void Build()
        {
            foreach (string solutionRoot in Directory.GetDirectories(this.path))
            {
                var solutionName = solutionRoot.Remove(0, path.Length);
                var solutionSrc = Path.Combine(solutionRoot, SolutionBuilder.sourceDirectoryName);
                Console.WriteLine($"Building {solutionName}...");
                
                if (Directory.Exists(solutionSrc))
                {
                    this.RunNuGetRestore(solutionSrc);
                    this.RunMsBuild(solutionSrc, "/T:CopyAssets /P:Configuration=Release");
                }
                else
                {
                    Console.WriteLine("* nothing to build (way to go!)");                    
                }
            }
        }
        
        
        private int RunProcess(string fileName, string arguments, string workingDirectory = null, Action<int, string> exitAction = null)
        {
            using (Process p = new Process()) 
            {
                p.StartInfo.FileName = fileName;
                p.StartInfo.Arguments = arguments;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WorkingDirectory = workingDirectory;
                p.Start();
        
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                
                if (exitAction != null)
                {
                    exitAction(p.ExitCode, output);
                }
                
                return p.ExitCode;                
            }
        }
        
        private void RunNuGetRestore(string solutionSrcPath)
        {
            Console.Write("* running NuGet restore... ");
            this.RunProcess(
                "nuget.exe",
                $"restore -PackagesDirectory \"{this.packagesDirectory}\"",
                solutionSrcPath,
                this.defaultExitAction);
        }
        
        private void RunMsBuild(string solutionSrcPath, string arguments)
        {
            Console.Write($"* running MsBuild {arguments}... ");
            this.RunProcess(
                "msbuild.exe",
                arguments,
                solutionSrcPath,
                this.defaultExitAction);
        }
    }
}
