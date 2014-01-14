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
            var options = new Options();
            CommandLine.Parser.Default.ParseArgumentsStrict(
                args,
                options,
                () =>
                    {
                        Console.WriteLine("Arguments are not valid. Use -help flag to start app correctly.");
                        Environment.Exit(1);
                    });

            if (!options.AreDatesProvided)
            {
                Console.WriteLine("Both start and end dates must be provided in in format yyyy-mm-{01..31}-{0..23} format.");
                Environment.Exit(1);
            }

            if (options.StartDateTime > options.EndDateTime)
            {
                Console.WriteLine("Start date must be equal or less than end date.");
                Environment.Exit(1);
            }


            DateTime curDate = options.EndDateTime;

            var hours = new List<DateTime> { curDate };
            while (curDate > options.StartDateTime)
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
                                LogMessage(string.Format("Json for {0} succesfully downloaded", time), options);
                                IDBRepository dbRepository = new DBRepository();
                                if (!dbRepository.EventsExistForHour(time))
                                {
                                    var fileToRepositoryProcessor = new FileToRepositoryProcessor(dbRepository, languageFilter);
                                    try
                                    {
                                        fileToRepositoryProcessor.ProcessFile(pathProvider.GetFilePath(time));
                                        LogMessage(string.Format("{0} succesfully processed", time), options);
                                        
                                    }
                                    catch (Exception exception)
                                    {
                                        // If something happens we can just clear events without handling transations
                                        dbRepository.ClearEventsForHour(time);
                                        LogError(
                                            string.Format(
                                                "ERROR: Events hour {0} was not processed and therefore removed because of exception. See log for details",
                                                time),
                                            options,
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
        /// The log message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        public static void LogMessage(string message, Options options)
        {
            if (options.VerboseType == VerboseType.ErrorOnly || 
                options.VerboseType == VerboseType.Silent)
            {
                return;
            }

            if (options.VerboseType == VerboseType.LastOnly)
            {
                Console.Clear();
            }

            Console.WriteLine(message);
        }

        /// <summary>
        /// The log error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public static void LogError(string message, Options options, Exception ex)
        {
            if (options.VerboseType == VerboseType.Silent)
            {
                return;
            }

            if (options.VerboseType == VerboseType.LastOnly)
            {
                Console.Clear();
            }

            Console.WriteLine(message);
            log.Error(message, ex);
        }
    }
}
