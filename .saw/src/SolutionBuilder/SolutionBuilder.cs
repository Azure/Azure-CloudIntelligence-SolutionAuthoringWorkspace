namespace Microsoft.Ciqs.Saw.Builder
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SolutionBuilder
    {
        private string path;
        private string packagesDirectory;
        
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
                    Console.Write("* running NuGet restore... ");
                    this.RunNuGetRestore(solutionSrc);
                    Console.WriteLine("done");
                    Console.Write("* building and copying assets... ");
                    this.RunMsBuild(solutionSrc);
                    Console.WriteLine("done");
                }
                else
                {
                    Console.WriteLine("* nothing to build (way to go!)");                    
                }
            }
        }
        
        private void RunNuGetRestore(string solutionSrcPath)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "nuget.exe";
                p.StartInfo.Arguments = $"restore -PackagesDirectory \"{this.packagesDirectory}\"";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WorkingDirectory = solutionSrcPath;
                p.Start();
        
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                
                if (p.ExitCode != 0)
                {
                    throw new Exception($"NuGet restore error:\n{output}");
                }
            }
        }
        
        private void RunMsBuild(string solutionSrcPath)
        {
            using (Process p = new Process()) 
            {
                p.StartInfo.FileName = "msbuild.exe";
                p.StartInfo.Arguments = "/T:CopyAssets /P:Configuration=Release";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WorkingDirectory = solutionSrcPath;
                p.Start();
        
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                if (p.ExitCode != 0)
                {
                    throw new Exception($"Unable to build solution:\n{output}");
                }
            }
        }

    }
}
