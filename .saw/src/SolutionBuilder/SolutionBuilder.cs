namespace Microsoft.Ciqs.Saw.Builder
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SolutionBuilder
    {
        private string path;

        public SolutionBuilder(string path)
        {
            // replace UNIX slashes
            path = path.Replace(@"/", Path.DirectorySeparatorChar.ToString());

            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }

            this.path = path;
        }

        public void Build()
        {
            Console.WriteLine($"Building solutions in {this.path}");
        }
    }
}
