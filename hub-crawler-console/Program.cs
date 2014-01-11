// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace hub_crawler_console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GitArchiveProcessor.DataLayer;
    using GitArchiveProcessor.DataLayer.Dapper;
    using GitArchiveProcessor.Logic;
    using GitArchiveProcessor.Settings;

    using log4net;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            log.Debug("Start application");

            if (args.Length != 2)
            {
                Console.WriteLine("Two and only two command line parameters must be provided");
                Environment.Exit(1);
            }

            DateTime date1 = DateTime.MinValue, date2 = DateTime.MaxValue;

            try
            {
                date1 = ParseDateArgument(args[0]);
                date2 = ParseDateArgument(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            DateTime start = (date1 > date2) ? date2 : date1,
                     end = (date1 > date2) ? date1 : date2,
                     curDate = end;

            var hours = new List<DateTime> { curDate };
            while (curDate > start)
            {
                curDate = curDate.AddHours(-1);
                hours.Add(curDate);
            }

            IPathProvider pathProvider = new DefaultPathProvider();
            ILanguageFilter languageFilter = new DefaultLanguageFilter();

            Parallel.ForEach(
                hours,
                new ParallelOptions { MaxDegreeOfParallelism = 3 },
                time =>
                {
                    var networkProcessor = new NetworkProcessor(pathProvider);
                    networkProcessor.GetGitHubArchive(time)
                        .ContinueWith(
                            _ =>
                            {
                                Console.WriteLine("Json for {0} succesfully downloaded", time);
                                IDBRepository dbRepository = new DBRepository();
                                if (!dbRepository.EventsExistForHour(time))
                                {
                                    var fileToRepositoryProcessor = new FileToRepositoryProcessor(dbRepository, languageFilter);
                                    try
                                    {
                                        fileToRepositoryProcessor.ProcessFile(pathProvider.GetFilePath(time));
                                        Console.WriteLine("{0} succesfully processed", time);
                                    }
                                    catch (Exception exception)
                                    {
                                        // If something happens we can just clear events without handling transations
                                        dbRepository.ClearEventsForHour(time);
                                        Console.WriteLine(
                                            "ERROR: Events hour {0} was not processed and therefore removed because of exception. See log for details",
                                            time);

                                        log.Error(
                                            string.Format("Events hour {0} was not processed and therefore removed because of exception. See log for details", time),
                                            exception);
                                    }
                                }
                            })
                        .Wait();
                });

            Console.WriteLine("Completed...");
            Console.ReadLine();
        }

        /// <summary>
        /// The parse date argument.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        private static DateTime ParseDateArgument(string argument)
        {
            var dateParts = argument.Split('-').Select(int.Parse).ToArray();
            return new DateTime(dateParts[0], dateParts[1], dateParts[2], dateParts[3], 0, 0);
        }
    }
}
