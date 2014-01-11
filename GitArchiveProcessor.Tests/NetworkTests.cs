// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkTests.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   The network test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using FluentAssertions;

    using GitArchiveProcessor.Logic;
    using GitArchiveProcessor.Settings;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The network test.
    /// </summary>
    [TestClass]
    public class NetworkTests
    {
        /// <summary>
        /// The download single hour archive.
        /// </summary>
        [TestMethod]
        public void DownloadSingleHourArchive()
        {
            IPathProvider pathProvider = new DefaultPathProvider();
            NetworkProcessor networkProcessor = new NetworkProcessor(pathProvider);
            Random r = new Random();
            var archiveHour = new DateTime(2013, r.Next(1, 12), r.Next(1, 28), r.Next(23), 0, 0);

            networkProcessor.GetGitHubArchive(archiveHour).Wait();

            File.Exists(pathProvider.GetFilePath(archiveHour)).Should().Be(true);
        }

        /// <summary>
        /// The download few archives in parallel.
        /// </summary>
        [TestMethod]
        public void DownloadFewArchivesInParallel()
        {
            Random r = new Random();
            List<DateTime> list = new List<DateTime>();
            for (var i = 0; i < 5; i++)
            {
                list.Add(new DateTime(2013, r.Next(1, 12), r.Next(1, 28), r.Next(23), 0, 0));
            }

            IPathProvider pathProvider = new DefaultPathProvider();
            Parallel.ForEach(
                list,
                new ParallelOptions { MaxDegreeOfParallelism = 3 },
                time =>
                    {
                        NetworkProcessor networkProcessor = new NetworkProcessor(pathProvider);
                        networkProcessor.GetGitHubArchive(time).Wait();
                    });

            list.Should().OnlyContain(date => File.Exists(pathProvider.GetFilePath(date)), "All files should be created");
        }
    }
}
