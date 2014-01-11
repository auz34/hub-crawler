// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitRepository.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the GitRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.DataLayer.Models
{
    using System;

    /// <summary>
    /// The repository.
    /// </summary>
    public class GitRepository
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the watchers.
        /// </summary>
        public int Watchers { get; set; }

        /// <summary>
        /// Gets or sets the stars.
        /// </summary>
        public int Stars { get; set; }

        /// <summary>
        /// Gets or sets the forks.
        /// </summary>
        public int Forks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is fork.
        /// </summary>
        public bool IsFork { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the last event date time.
        /// </summary>
        public DateTime LastEventDateTime { get; set; }
    }
}
