// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextProcessor.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   The git archive processor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GitArchiveProcessor.DataLayer.Models.Json;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Schema;

    /// <summary>
    /// The git archive processor.
    /// </summary>
    public class TextProcessor
    {
        /// <summary>
        /// The json schema.
        /// </summary>
        public const string ArchiveRecordStringSchema = @"{
            'description': 'Single record of git hub archive',
            'type': 'object',
            'properties': {
                'public': {'type':'boolean'},
                'type': {'type':'string'},
                'repository': {
                    'description': 'The information about repository related to the git hub event',
                    'type': 'object',
                    'properties': {
                        'id': {'type':'integer'},
                        'name': {'type':'string'},
                        'url': {'type':'string'},
                        'description': {'type':'string'},
                        'watchers': {'type':'integer'},
                        'stargazers': {'type':'integer'},
                        'forks': {'type':'integer'},
                        'fork': {'type':'boolean'},
                        'language': {'type':'string'},
                    }
                }
            }
        }";

        /// <summary>
        /// The is json valid.
        /// </summary>
        /// <param name="stringJson">
        /// The string json.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsJsonValid(string stringJson)
        {
            var archiveRecordJsonSchema = JsonSchema.Parse(ArchiveRecordStringSchema);
            JObject record = JObject.Parse(stringJson);
            return record.IsValid(archiveRecordJsonSchema);
        }

        /// <summary>
        /// The deserialize git event.
        /// </summary>
        /// <param name="stringJson">
        /// The string json.
        /// </param>
        /// <returns>
        /// The <see cref="GitEvent"/>.
        /// </returns>
        public static GitEvent DeserializeGitEvent(string stringJson)
        {
            var result = JsonConvert.DeserializeObject<GitEvent>(stringJson);
            return result;
        }

        /// <summary>
        /// The get record strings.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see><cref>List</cref></see>.
        /// </returns>
        public static IEnumerable<string> GetRecordStrings(string content)
        {
            int i = 0,
                nestedDepth = 0;

            var stringBuilder = new StringBuilder();
            bool ignoreMode = false;
            while (i < content.Length)
            {
                stringBuilder.Append(content[i]);
                switch (content[i])
                {
                    case '"':
                        if (!ignoreMode)
                        {
                            ignoreMode = true;
                        }
                        else if ((i == 0) ||
                            (content[i - 1] != '\\') ||
                            (content[i - 1] == '\\' && content[i - 2] == '\\'))
                        {
                            ignoreMode = false;
                        }

                        break;
                    case '{':
                        if (!ignoreMode)
                        {
                            nestedDepth++;
                        }

                        break;
                    case '}':
                        if (!ignoreMode)
                        {
                            nestedDepth--;
                            if (nestedDepth == 0)
                            {
                                yield return stringBuilder.ToString();
                                stringBuilder = new StringBuilder();
                            }
                        }

                        break;
                }

                i++;
            }
            
            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            {
                throw new Exception("Split to records has been parsed incorrectly");
            }
        }

        /// <summary>
        /// The get git events.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="ignoreInvalid">
        /// The ignore invalid.
        /// </param>
        /// <returns>
        /// The <see><cref>IEnumerable</cref></see>.
        /// </returns>
        public static IEnumerable<GitEvent> GetGitEvents(string fileName, bool ignoreInvalid = false)
        {
            var fileContent = System.IO.File.ReadAllText(fileName);
            var fileContentCleaned = fileContent.Replace("\n", string.Empty);

            foreach (var recordString in GetRecordStrings(fileContentCleaned))
            {
                if (IsJsonValid(recordString))
                {
                    yield return DeserializeGitEvent(recordString);
                }
                else if (!ignoreInvalid)
                {
                    throw new Exception(string.Format("File {0} contains invalid record(s). First invalid record content equals to {1}", fileName, recordString));
                }
            }
        }
    }
}
