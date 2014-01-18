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
    using System.IO;
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
        /// The sample file 1.
        /// </summary>
        private const string SampleFile1 = @"TestData\2013-05-11-9.json";

        /// <summary>
        /// The sample file 2.
        /// </summary>
        private const string SampleFile2 = @"TestData\2012-04-11-15.json";

        /// <summary>
        /// The sample file 3.
        /// </summary>
        private const string SampleFile3 = @"TestData\2013-05-11-0.json";

        /// <summary>
        /// The sample file 4.
        /// </summary>
        private const string SampleFile4 = @"TestData\2013-05-11-21.json";

        /// <summary>
        /// The sample file 5.
        /// </summary>
        private const string SampleFile5 = @"TestData\2013-05-27-21.json";

        /// <summary>
        /// The sample file 6.
        /// </summary>
        private const string SampleFile6 = @"TestData\2013-08-21-14.json";

        /// <summary>
        /// The sum of items equal to content. First test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void SumOfItemsEqualToContent1()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile1);
            this.GenericSumOfItemsEqualToContentTest(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Second test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void SumOfItemsEqualToContent2()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile2);
            this.GenericSumOfItemsEqualToContentTest(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Third test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void SumOfItemsEqualToContent3()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile3);
            this.GenericSumOfItemsEqualToContentTest(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fourth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void SumOfItemsEqualToContent4()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile4);
            this.GenericSumOfItemsEqualToContentTest(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void SumOfItemsEqualToContent5()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile5);
            this.GenericSumOfItemsEqualToContentTest(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void SumOfItemsEqualToContent6()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile6);
            this.GenericSumOfItemsEqualToContentTest(fileName);
        }

        /// <summary>
        /// The all records are valid. First test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsAreValid1()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile1);
            this.GenericAllRecordsAreValid(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Second test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsAreValid2()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile2);
            this.GenericAllRecordsAreValid(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Third test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsAreValid3()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile3);
            this.GenericAllRecordsAreValid(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fourth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsAreValid4()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile4);
            this.GenericAllRecordsAreValid(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsAreValid5()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile5);
            this.GenericAllRecordsAreValid(fileName);
        }

        /// <summary>
        /// The all records can be deserialized 1.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsCanBeDeserialized1()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile1);
            this.GenericAllRecordsCanBeDeserialized(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Second test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsCanBeDeserialized2()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile2);
            this.GenericAllRecordsCanBeDeserialized(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Third test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsCanBeDeserialized3()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile3);
            this.GenericAllRecordsCanBeDeserialized(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fourth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsCanBeDeserialized4()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile1);
            this.GenericAllRecordsCanBeDeserialized(fileName);
        }

        /// <summary>
        /// The sum of items equal to content. Fifth test.
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData", "TestData")]
        public void AllRecordsCanBeDeserialized5()
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, SampleFile5);
            this.GenericAllRecordsCanBeDeserialized(fileName);
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
            string fileContent = File.ReadAllText(fileName).Replace("\n", string.Empty);
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
            string fileContent = File.ReadAllText(fileName);
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
