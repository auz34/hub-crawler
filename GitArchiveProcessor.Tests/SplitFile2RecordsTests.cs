// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplitFile2RecordsTests.cs" company="auzSoft">
//   MIT
// </copyright>
// <summary>
//   Defines the SplitFile2RecordsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GitArchiveProcessor.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GitArchiveProcessor.Logic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class SplitFile2RecordsTests
    {
        /// <summary>
        /// The sum of items equal to content. First test.
        /// </summary>
        [TestMethod]
        public void SumOfItemsEqualToContent1()
        {
            const string FileName = @"C:\gitjson\2013-05-11-9.json";
            this.GenericSumOfItemsEqualToContentTest(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Second test.
        /// </summary>
        [TestMethod]
        public void SumOfItemsEqualToContent2()
        {
            const string FileName = @"C:\gitjson\2012-04-11-15.json";
            this.GenericSumOfItemsEqualToContentTest(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Third test.
        /// </summary>
        [TestMethod]
        public void SumOfItemsEqualToContent3()
        {
            const string FileName = @"C:\gitjson\2013-05-11-0.json";
            this.GenericSumOfItemsEqualToContentTest(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fourth test.
        /// </summary>
        [TestMethod]
        public void SumOfItemsEqualToContent4()
        {
            const string FileName = @"C:\gitjson\2013-05-11-21.json";
            this.GenericSumOfItemsEqualToContentTest(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        public void SumOfItemsEqualToContent5()
        {
            const string FileName = @"C:\gitjson\2013-05-27-21.json";
            this.GenericSumOfItemsEqualToContentTest(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        public void SumOfItemsEqualToContent6()
        {
            const string FileName = @"C:\gitjson\2013-08-21-14.json";
            this.GenericSumOfItemsEqualToContentTest(FileName);
        }

        /// <summary>
        /// The all records are valid. First test.
        /// </summary>
        [TestMethod]
        public void AllRecordsAreValid1()
        {
            const string FileName = @"C:\gitjson\2013-05-11-9.json";
            this.GenericAllRecordsAreValid(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Second test.
        /// </summary>
        [TestMethod]
        public void AllRecordsAreValid2()
        {
            const string FileName = @"C:\gitjson\2012-04-11-15.json";
            this.GenericAllRecordsAreValid(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Third test.
        /// </summary>
        [TestMethod]
        public void AllRecordsAreValid3()
        {
            const string FileName = @"C:\gitjson\2013-05-11-0.json";
            this.GenericAllRecordsAreValid(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fourth test.
        /// </summary>
        [TestMethod]
        public void AllRecordsAreValid4()
        {
            const string FileName = @"C:\gitjson\2013-05-11-21.json";
            this.GenericAllRecordsAreValid(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        public void AllRecordsAreValid5()
        {
            const string FileName = @"C:\gitjson\2013-05-27-21.json";
            this.GenericAllRecordsAreValid(FileName);
        }

        /// <summary>
        /// The all records can be deserialized 1.
        /// </summary>
        [TestMethod]
        public void AllRecordsCanBeDeserialized1()
        {
            const string FileName = @"C:\gitjson\2013-05-11-9.json";
            this.GenericAllRecordsCanBeDeserialized(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Second test.
        /// </summary>
        [TestMethod]
        public void AllRecordsCanBeDeserialized2()
        {
            const string FileName = @"C:\gitjson\2012-04-11-15.json";
            this.GenericAllRecordsCanBeDeserialized(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Third test.
        /// </summary>
        [TestMethod]
        public void AllRecordsCanBeDeserialized3()
        {
            const string FileName = @"C:\gitjson\2013-05-11-0.json";
            this.GenericAllRecordsCanBeDeserialized(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fourth test.
        /// </summary>
        [TestMethod]
        public void AllRecordsCanBeDeserialized4()
        {
            const string FileName = @"C:\gitjson\2013-05-11-21.json";
            this.GenericAllRecordsCanBeDeserialized(FileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        public void AllRecordsCanBeDeserialized5()
        {
            const string FileName = @"C:\gitjson\2013-05-27-21.json";
            this.GenericAllRecordsCanBeDeserialized(FileName);
        }

        /// <summary>
        /// The get string items.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see><cref>List</cref></see>.
        /// </returns>
        private IEnumerable<string> GetStringItems(string fileName)
        {
            string fileContent = System.IO.File.ReadAllText(fileName).Replace("\n", string.Empty);
            return TextProcessor.GetRecordStrings(fileContent);
        }

        /// <summary>
        /// The generic sum of items equal to content test.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        private void GenericSumOfItemsEqualToContentTest(string fileName)
        {
            string fileContent = System.IO.File.ReadAllText(fileName);
            string fileContentCleaned = fileContent.Replace("\n", string.Empty);
            List<string> items = TextProcessor.GetRecordStrings(fileContentCleaned).ToList();

            int length = items.Sum(s => s.Length);

            /* Unfortunately not all files follow rule that records are delimited by '\n' separator
            int count = fileContent.Split('\n').Count(s => !string.IsNullOrEmpty(s)); */

            Assert.AreEqual(length, fileContentCleaned.Length);
            
            /* Assert.AreEqual(count, items.Count); */

            Assert.AreEqual(fileContentCleaned, string.Join(string.Empty, items));
        }

        /// <summary>
        /// The generic all records are valid.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        private void GenericAllRecordsAreValid(string fileName)
        {
            IEnumerable<string> items = this.GetStringItems(fileName);

            var allJsonValid = items.Aggregate(true, (current, json) => current & TextProcessor.IsJsonValid(json));

            Assert.AreEqual(true, allJsonValid);
        }

        /// <summary>
        /// The generic all records can be deserialized.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        private void GenericAllRecordsCanBeDeserialized(string fileName)
        {
            IEnumerable<string> items = this.GetStringItems(fileName);
            foreach (var json in items)
            {
                try
                {
                    TextProcessor.DeserializeGitEvent(json);
                }
                catch (Exception ex)
                {
                    Assert.Fail("Expected no exception, but got: " + ex.Message);
                }
            }
        }
    }
}
