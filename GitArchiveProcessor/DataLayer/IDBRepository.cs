// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDBRepository.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the IDBRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.DataLayer
{
    using System;

    using GitArchiveProcessor.DataLayer.Models;

    /// <summary>
    /// The DBRepository interface.
    /// </summary>
    public interface IDBRepository
    {
        /// <summary>
        /// The find git repository.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="GitRepository"/>.
        /// </returns>
        GitRepository FindGitRepository(int id);

        /// <summary>
        /// The add repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void AddRepository(GitRepository repository);

        /// <summary>
        /// The update repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        void UpdateRepository(GitRepository repository);

        /// <summary>
        /// The add event.
        /// </summary>
        /// <param name="gitEvent">
        /// The git event.
        /// </param>
        void AddEvent(GitEvent gitEvent);

        /// <summary>
        /// The events exist for hour.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool EventsExistForHour(DateTime hourlyArchiveDate);

        /// <summary>
        /// The clear events for hour.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        void ClearEventsForHour(DateTime hourlyArchiveDate);
    }
}