namespace Microsoft.Ciqs.Saw.Phases
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Ciqs.Saw.Common;

    [Phase("build", "build solution artifacts from source code")]
    public class SolutionBuilderPhase : IPhase
    {
        [Parameter("path to the directory containing solutions")]
        public string SolutionsDirectory { get; set; }
        [Parameter("NuGet packages directory")]
        public string PackagesDirectory { get; set; }
        [Parameter("solution(s) to act upon", Required=false)]
        public string[] Solutions { get; set; }

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
                
        public void Run()
        {
            var solutionsRoot = this.SolutionsDirectory;
            
            if (!solutionsRoot.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                solutionsRoot += Path.DirectorySeparatorChar;
            }
            
            var solutionSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            if (this.Solutions != null)
            {
                solutionSet.UnionWith(this.Solutions);
            }

            foreach (string solutionRoot in Directory.GetDirectories(solutionsRoot))
            {
                var solutionName = solutionRoot.Remove(0, solutionsRoot.Length).ToLower();
                
                if (this.Solutions != null && !solutionSet.Contains(solutionName))
                {
                    continue;
                }
                
                var solutionSrc = Path.Combine(solutionRoot, SolutionBuilderPhase.sourceDirectoryName);
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
                $"restore -PackagesDirectory \"{this.PackagesDirectory}\"",
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
