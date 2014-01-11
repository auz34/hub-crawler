// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultPathProvider.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the DefaultPathProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Settings
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The default path provider.
    /// </summary>
    public class DefaultPathProvider : IPathProvider
    {
        /// <summary>
        /// The base url pattern.
        /// </summary>
        private const string BaseUrlPattern = @"http://data.githubarchive.org/{0}.gz";

        /// <summary>
        /// The gz path pattern.
        /// </summary>
        private const string GzPathPattern = @"{0}\GitHubArchive\gz\{1}.gz";

        /// <summary>
        /// The cache folder path pattern.
        /// </summary>
        private const string CacheFolderPathPattern = @"{0}\GitHubArchive\";

        private const string GzCacheFolderPathPattern = @"{0}\GitHubArchive\gz\";

        /// <summary>
        /// The get hourly archive url.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHourlyArchiveUrl(DateTime hourlyArchiveDate)
        {
            return string.Format(BaseUrlPattern, this.GetFileName(hourlyArchiveDate));
        }

        /// <summary>
        /// The get gz file path.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetGzFilePath(DateTime hourlyArchiveDate)
        {
            return string.Format(GzPathPattern, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), this.GetFileName(hourlyArchiveDate));
        }

        /// <summary>
        /// The get gz folder path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetGzFolderPath()
        {
            return string.Format(GzCacheFolderPathPattern, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        /// <summary>
        /// The get file path.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFilePath(DateTime hourlyArchiveDate)
        {
            return string.Format("{0}{1}", this.GetFolderPath(), this.GetFileName(hourlyArchiveDate));
        }

        /// <summary>
        /// The get file name.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileName(DateTime hourlyArchiveDate)
        {
            return string.Format(
                "{0}-{1}-{2}-{3}.json",
                hourlyArchiveDate.Year,
                hourlyArchiveDate.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),
                hourlyArchiveDate.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),
                hourlyArchiveDate.Hour);
        }

        /// <summary>
        /// The get folder path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFolderPath()
        {
            return string.Format(CacheFolderPathPattern, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }
    }
}
