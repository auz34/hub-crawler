// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitRepository.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the GitRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.DataLayer.Models.Json
{
    using Newtonsoft.Json;

    /// <summary>
    /// The git repository json.
    /// </summary>
    public class GitRepository
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the watchers.
        /// </summary>
        [JsonProperty("watchers")]
        public int Watchers { get; set; }

        /// <summary>
        /// Gets or sets the stars.
        /// </summary>
        [JsonProperty("stargazers")]
        public int Stars { get; set; }

        /// <summary>
        /// Gets or sets the forks.
        /// </summary>
        [JsonProperty("forks")]
        public int Forks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is fork.
        /// </summary>
        [JsonProperty("fork")]
        public bool IsFork { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
