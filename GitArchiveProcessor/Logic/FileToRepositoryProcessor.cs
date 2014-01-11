// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileToRepositoryProcessor.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the FileToRepositoryProcessor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Logic
{
    using AutoMapper;

    using GitArchiveProcessor.DataLayer;
    using GitArchiveProcessor.DataLayer.Models;
    using GitArchiveProcessor.Settings;

    using JSON = GitArchiveProcessor.DataLayer.Models.Json;

    /// <summary>
    /// The file to db processor.
    /// </summary>
    public class FileToRepositoryProcessor
    {
        /// <summary>
        /// The _db repository.
        /// </summary>
        private readonly IDBRepository dbRepository;

        private ILanguageFilter languageFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToRepositoryProcessor"/> class.
        /// </summary>
        /// <param name="dbRepository">
        /// The db repository.
        /// </param>
        /// <param name="languageFilter">
        /// The language Filter.
        /// </param>
        public FileToRepositoryProcessor(IDBRepository dbRepository, ILanguageFilter languageFilter)
        {
            this.dbRepository = dbRepository;
            this.languageFilter = languageFilter;
        }

        /// <summary>
        /// The process file record.
        /// </summary>
        /// <param name="fileRecord">
        /// The file record.
        /// </param>
        public void ProcessFileRecord(JSON.GitEvent fileRecord)
        {
            GitEvent gitEvent = Mapper.Map<GitEvent>(fileRecord);
            GitRepository gitRepository = this.dbRepository.FindGitRepository(gitEvent.GitRepositoryId);
            if (gitRepository == null)
            {
                gitRepository = Mapper.Map<GitRepository>(fileRecord.Repository);
                gitRepository.LastEventDateTime = gitEvent.CreatedAt;
                this.dbRepository.AddRepository(gitRepository);
            }
            else if (gitRepository.LastEventDateTime < gitEvent.CreatedAt)
            {
                gitRepository = Mapper.Map<GitRepository>(fileRecord.Repository);
                gitRepository.LastEventDateTime = gitEvent.CreatedAt;

                this.dbRepository.UpdateRepository(gitRepository);
            }

            this.dbRepository.AddEvent(gitEvent);
        }

        /// <summary>
        /// The process file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="ignoreInvalid">
        /// The ignore invalid.
        /// </param>
        public void ProcessFile(string fileName, bool ignoreInvalid = false)
        {
            foreach (var gitEvent in TextProcessor.GetGitEvents(fileName, ignoreInvalid))
            {
                if (gitEvent.Repository != null && this.languageFilter.IsLanguageAccepted(gitEvent.Repository.Language))
                {
                    this.ProcessFileRecord(gitEvent);
                }
            }
        }
    }
}
