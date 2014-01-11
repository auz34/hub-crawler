// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DapperRepositoryTests.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   The dapper repository tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Tests
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Dapper;
    using FluentAssertions;

    using GitArchiveProcessor.DataLayer.Dapper;
    using GitArchiveProcessor.DataLayer.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The dapper repository tests.
    /// </summary>
    [TestClass]
    public class DapperRepositoryTests
    {
        /// <summary>
        /// The clear db tables.
        /// </summary>
        [TestCleanup]
        [TestInitialize]
        public void ClearDbTables()
        {
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["GitArchiveConnection"].ConnectionString);
            db.Execute("DELETE FROM GitEvent");
            db.Execute("DELETE FROM GitRepository");
        }

        /// <summary>
        /// The insert repository.
        /// </summary>
        [TestMethod]
        public void InsertRepository()
        {
            DBRepository dbRepository = new DBRepository();
            GitRepository repo = new GitRepository
                                     {
                                         Id = 1,
                                         Description = "Descr",
                                         Forks = 1,
                                         IsFork = true,
                                         Language = "Javascript",
                                         Name = "Test",
                                         Stars = 1,
                                         Url = "http:\\githib.com",
                                         Watchers = 1,
                                         LastEventDateTime = DateTime.Now
                                     };

            dbRepository.AddRepository(repo);

            dbRepository.FindGitRepository(1).Should().NotBeNull();
        }

        /// <summary>
        /// The update repository.
        /// </summary>
        [TestMethod]
        public void UpdateRepository()
        {
            DBRepository dbRepository = new DBRepository();
            GitRepository repo = new GitRepository
            {
                Id = 1,
                Description = "Descr",
                Forks = 1,
                IsFork = true,
                Language = "Javascript",
                Name = "Test",
                Stars = 1,
                Url = "http:\\githib.com",
                Watchers = 1,
                LastEventDateTime = DateTime.Now
            };

            dbRepository.AddRepository(repo);

            GitRepository newRepo = new GitRepository
            {
                Id = 1,
                Description = "Descr",
                Forks = 2,
                IsFork = true,
                Language = "Javascript",
                Name = "Test",
                Stars = 2,
                Url = "http:\\githib.com",
                Watchers = 1,
                LastEventDateTime = DateTime.Now
            };

            dbRepository.UpdateRepository(newRepo);
            
            GitRepository dbRepo = dbRepository.FindGitRepository(1);

            dbRepo.Id.Should().Be(1);
            dbRepo.Description.Should().Be("Descr");
            dbRepo.Forks.Should().Be(2);
        }

        /// <summary>
        /// The insert event.
        /// </summary>
        [TestMethod]
        public void InsertEvent()
        {
            DBRepository dbRepository = new DBRepository();
            GitRepository repo = new GitRepository
            {
                Id = 1,
                Description = "Descr",
                Forks = 1,
                IsFork = true,
                Language = "Javascript",
                Name = "Test",
                Stars = 1,
                Url = "http:\\githib.com",
                Watchers = 1,
                LastEventDateTime = DateTime.Now
            };

            dbRepository.AddRepository(repo);

            DateTime createdAt = new DateTime(2013, 11, 15, 21, 15, 0);

            GitEvent gitEvent = new GitEvent
                                    {
                                        Actor = "Me", 
                                        CreatedAt = createdAt, 
                                        GitRepositoryId = 1, 
                                        IsPublic = true, 
                                        EventType = "Psuh", 
                                        Url = "http:\\githib.com"
                                    };

            dbRepository.AddEvent(gitEvent);

            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["GitArchiveConnection"].ConnectionString);
            db.Query<GitEvent>("SELECT * FROM GitEvent").Count().Should().Be(1);
        }

        /// <summary>
        /// The events exist for hour.
        /// </summary>
        [TestMethod]
        public void EventsExistForHour()
        {
            DBRepository dbRepository = new DBRepository();
            GitRepository repo = new GitRepository
            {
                Id = 1,
                Description = "Descr",
                Forks = 1,
                IsFork = true,
                Language = "Javascript",
                Name = "Test",
                Stars = 1,
                Url = "http:\\githib.com",
                Watchers = 1,
                LastEventDateTime = DateTime.Now
            };

            dbRepository.AddRepository(repo);

            DateTime createdAt = new DateTime(2013, 11, 15, 21, 15, 0);
            DateTime createdHour = new DateTime(2013, 11, 15, 21, 0, 0);

            GitEvent gitEvent = new GitEvent
            {
                Actor = "Me",
                CreatedAt = createdAt,
                GitRepositoryId = 1,
                IsPublic = true,
                EventType = "Psuh",
                Url = "http:\\githib.com"
            };

            dbRepository.AddEvent(gitEvent);

            dbRepository.EventsExistForHour(createdHour).Should().BeTrue();
            dbRepository.EventsExistForHour(createdHour.AddDays(2)).Should().BeFalse();
        }
    }
}
