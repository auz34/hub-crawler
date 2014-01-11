// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitEvent.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the GitEvent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.DataLayer.Models
{
    using System;

    /// <summary>
    /// The git event.
    /// </summary>
    public class GitEvent
    {
        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is public.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        public string Actor { get; set; }

        /// <summary>
        /// Gets or sets the git repository id.
        /// </summary>
        public int GitRepositoryId { get; set; }
    }
}
