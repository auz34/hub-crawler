// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkProcessor.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the NetworkProcessor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GitArchiveProcessor.Logic
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using GitArchiveProcessor.Settings;

    using Ionic.Zip;
    using Ionic.Zlib;

    /// <summary>
    /// The network processor.
    /// </summary>
    public class NetworkProcessor
    {
        /// <summary>
        /// The path provider.
        /// </summary>
        private readonly IPathProvider pathProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkProcessor"/> class.
        /// </summary>
        /// <param name="pathProvider">
        /// The path provider.
        /// </param>
        public NetworkProcessor(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        /// <summary>
        /// The get git hub archive.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The archive hour date.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task GetGitHubArchive(DateTime hourlyArchiveDate)
        {
            string gzFilePathInCache = this.pathProvider.GetGzFilePath(hourlyArchiveDate);
            if (!File.Exists(this.pathProvider.GetFilePath(hourlyArchiveDate)))
            {
                if (!File.Exists(gzFilePathInCache))
                {
                    if (!Directory.Exists(this.pathProvider.GetGzFolderPath()))
                    {
                        Directory.CreateDirectory(this.pathProvider.GetGzFolderPath());
                    }

                    using (var webClient = new WebClient())
                    {
                        await webClient.DownloadFileTaskAsync(this.pathProvider.GetHourlyArchiveUrl(hourlyArchiveDate), gzFilePathInCache);
                    }
                }

                using (FileStream readFileStream = new FileStream(gzFilePathInCache, FileMode.Open))
                {
                    using (GZipStream gzipStream = new GZipStream(readFileStream, CompressionMode.Decompress))
                    {
                        const int Size = 4096;
                        byte[] buffer = new byte[Size];

                        using (FileStream writeFileStream = new FileStream(this.pathProvider.GetFilePath(hourlyArchiveDate), FileMode.Create))
                        {
                            int count = 0;
                            do
                            {
                                count = gzipStream.Read(buffer, 0, Size);
                                if (count > 0)
                                {
                                    writeFileStream.Write(buffer, 0, count);
                                }
                            }
                            while (count > 0);
                        }
                    }
                }
            }
        }
    }
}
