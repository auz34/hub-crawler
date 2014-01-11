// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultLanguageFilter.cs" company="auzSoft">
//   MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace GitArchiveProcessor.Settings
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// The default language filter.
    /// </summary>
    public class DefaultLanguageFilter: ILanguageFilter
    {
        /// <summary>
        /// The allowed languages.
        /// </summary>
        private readonly HashSet<string> allowedLanguages;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLanguageFilter"/> class.
        /// </summary>
        public DefaultLanguageFilter()
        {
            string languages = ConfigurationManager.AppSettings["Languages"];
            if (!string.IsNullOrEmpty(languages))
            {
                string[] arrLanguages = languages.Split(',');
                if (arrLanguages.Any(s => !string.IsNullOrEmpty(s.Trim())))
                {
                    this.allowedLanguages = new HashSet<string>(arrLanguages.Where(s => !string.IsNullOrEmpty(s)).Select(s => s.Trim()));
                }
            }
        }

        /// <summary>
        /// The is language accepted.
        /// </summary>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLanguageAccepted(string language)
        {
            if (this.allowedLanguages != null)
            {
                return this.allowedLanguages.Contains(language);
            }

            return true;
        }
    }
}
