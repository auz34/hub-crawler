// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   The command line options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace hub_crawler_console
{
    using System;
    using System.Linq;

    using CommandLine;

    /// <summary>
    /// The verbose type.
    /// </summary>
    public enum VerboseType
    {
        /// <summary>
        /// The silent.
        /// </summary>
        Silent,

        /// <summary>
        /// The last only.
        /// </summary>
        LastOnly,

        /// <summary>
        /// The error only.
        /// </summary>
        ErrorOnly,

        /// <summary>
        /// The chatty.
        /// </summary>
        Chatty
    }

    /// <summary>
    /// The command line options.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the start date of crawling.
        /// </summary>
        [Option('s', "start", Required = true, HelpText = "Start hour to crawl git archive in format yyyy-mm-{01..31}-{0..23}")]
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the start date of crawling.
        /// </summary>
        [Option('e', "end", Required = true, HelpText = "End hour to crawl git archive in format yyyy-mm-{01..31}-{0..23}")]
        public string End { get; set; }

        /// <summary>
        /// Gets or sets the verbose type.
        /// </summary>
        [Option('v', "verbose", Required = false, DefaultValue = VerboseType.LastOnly, HelpText = "Indicates how and which messaged will be printed into console.")]
        public VerboseType VerboseType { get; set; }

        /// <summary>
        /// Gets the start date time.
        /// </summary>
        public DateTime StartDateTime
        {
            get
            {
                return ParseDateArgument(this.Start);
            }
        }

        /// <summary>
        /// Gets the end date time.
        /// </summary>
        public DateTime EndDateTime
        {
            get
            {
                return ParseDateArgument(this.End);
            }
        }

        /// <summary>
        /// Gets a value indicating whether is dates provided.
        /// </summary>
        public bool AreDatesProvided
        {
            get
            {
                try
                {
                    var d1 = this.StartDateTime;
                    var d2 = this.EndDateTime;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
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
