// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBRepository.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   The db repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.DataLayer.Dapper
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using global::Dapper;

    using GitArchiveProcessor.DataLayer.Models;

    /// <summary>
    /// The db repository.
    /// </summary>
    public class DBRepository : IDBRepository
    {
        /// <summary>
        /// The db.
        /// </summary>
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["GitArchiveConnection"].ConnectionString);

        /// <summary>
        /// The find git repository.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="GitRepository"/>.
        /// </returns>
        public GitRepository FindGitRepository(int id)
        {
            return this.db.Query<GitRepository>("SELECT * FROM GitRepository WHERE Id=@Id", new { id }).SingleOrDefault();
        }

        /// <summary>
        /// The add repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void AddRepository(GitRepository repository)
        {
            // Id, Name, Url, Description, Watchers, Stars, Forks, IsFork, Language, LastEventDateTime
            var sql = "INSERT INTO GitRepository (Id, Name, Url, Description, Watchers, Stars, Forks, IsFork, Language, LastEventDateTime) " +
                "VALUES(@Id, @Name, @Url, @Description, @Watchers, @Stars, @Forks, @IsFork, @Language, @LastEventDateTime) ";
            
            this.db.Execute(sql, repository);
        }

        /// <summary>
        /// The update repository.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public void UpdateRepository(GitRepository repository)
        {
            var sql = "UPDATE GitRepository " +
                "SET Name = @Name," +
                "    Url = @Url," +
                "    Description = @Description," +
                "    Watchers = @Watchers," +
                "    Stars = @Stars," +
                "    Forks = @Forks," +
                "    IsFork = @IsFork," +
                "    Language = @Language," +
                "    LastEventDateTime = @LastEventDateTime " +
                "WHERE Id = @Id";

            this.db.Execute(sql, repository);
        }

        /// <summary>
        /// The add event.
        /// </summary>
        /// <param name="gitEvent">
        /// The git event.
        /// </param>
        public void AddEvent(GitEvent gitEvent)
        {
            // CreatedAt, IsPublic, EventType, Url, Actor, GitRepositoryId
            var sql = "INSERT INTO GitEvent (CreatedAt, IsPublic, EventType, Url, Actor, GitRepositoryId) " +
                "VALUES(@CreatedAt, @IsPublic, @EventType, @Url, @Actor, @GitRepositoryId) ";

            this.db.Execute(sql, gitEvent);
        }

        /// <summary>
        /// The events exist for hour.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool EventsExistForHour(DateTime hourlyArchiveDate)
        {
            DateTime start = hourlyArchiveDate;
            DateTime end = hourlyArchiveDate.AddHours(1);
            return this.db.Query<GitEvent>("SELECT TOP 1 * FROM GitEvent WHERE CreatedAt>@start AND CreatedAt<=@end", new { start, end }).Any();
        }

        /// <summary>
        /// The clear events for hour.
        /// </summary>
        /// <param name="hourlyArchiveDate">
        /// The hourly archive date.
        /// </param>
        public void ClearEventsForHour(DateTime hourlyArchiveDate)
        {
            DateTime start = hourlyArchiveDate;
            DateTime end = hourlyArchiveDate.AddHours(1);
            this.db.Execute("DELETE FROM GitEvent WHERE CreatedAt>@start AND CreatedAt<=@end", new { start, end });
        }
    }
}
