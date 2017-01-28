namespace SamplesExtractor
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;

    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            var samplesDirectory = this.Context.Parameters["SamplesDirectory"];

            var assembly = Assembly.GetExecutingAssembly();
            var nameSpace = Assembly.GetExecutingAssembly().GetName().Name;
            var resourceName = nameSpace + ".Samples.zip";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                var zipArchive = new ZipArchive(stream);
                foreach (var entry in zipArchive.Entries)
                {
                    var outputFile = Path.Combine(samplesDirectory, entry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

                    using (Stream inStream = entry.Open())
                    using (Stream outStream = File.Create(outputFile))
                    {
                        inStream.CopyTo(outStream);
                    }

                    this.Context.LogMessage(entry.FullName);
                    Console.WriteLine(entry.FullName);
                }
            }
        }
    }
}
