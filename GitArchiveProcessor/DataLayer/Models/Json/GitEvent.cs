// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitEvent.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the GitEvent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.DataLayer.Models.Json
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// The git event json.
    /// </summary>
    public class GitEvent
    {
        /// <summary>
        /// Gets or sets the created at json object property.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is public.
        /// </summary>
        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        [JsonProperty("type")]
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        [JsonProperty("actor")]
        public string Actor { get; set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        [JsonProperty("repository")]
        public GitRepository Repository { get; set; }
    }
}
