// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPathProvider.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the IPathProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Settings
{
    using System;

    /// <summary>
    /// The PathProvider interface.
    /// </summary>
    public interface IPathProvider
    {
        /// <summary>
        /// The get hourly archive url.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetHourlyArchiveUrl(DateTime hourlyArchiveDate);

        /// <summary>
        /// The get gz file path.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetGzFilePath(DateTime hourlyArchiveDate);

        /// <summary>
        /// The get gz folder path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetGzFolderPath();

        /// <summary>
        /// The get file path.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetFilePath(DateTime hourlyArchiveDate);

        /// <summary>
        /// The get file name.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetFileName(DateTime hourlyArchiveDate);

        /// <summary>
        /// The get folder path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetFolderPath();
    }
}