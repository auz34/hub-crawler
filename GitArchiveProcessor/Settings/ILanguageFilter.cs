// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILanguageFilter.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the ILanguageFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Settings
{
    /// <summary>
    /// The LanguageFilter interface.
    /// </summary>
    public interface ILanguageFilter
    {
        /// <summary>
        /// The is language accepted.
        /// </summary>
        /// <param name="language">
        /// The Language.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsLanguageAccepted(string language);
    }
}
