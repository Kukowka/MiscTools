using AbbRapidPostDownload;
using AbbRapidPreUploader;
using AbbRapidPreUploaderTests;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMS_Utils.Utils.BackupReading.Backup.ABB.AbbLocalDefs;

namespace AbbRapidPreUploader.Tests
{
    [TestFixture]
    public class VolvoTruckUplaodChangerTests
    {
        private static string inputSpotData = @"LOCAL PERS spotdata sd_wp432810:=[432810,1,1,1,1,-1];";

        public static IEnumerable<TestCaseData> _inputVsOutputInnerFilePaths
        {
            get
            {
                yield return new TestCaseData("Irc5UploadOrg.mod", "Irc5UploadResult.mod");
                yield return new TestCaseData("Irc6UploadOrg.mod", "Irc6UploadResult.mod");
            }
        }

        [Test]
        [TestCaseSource(nameof(_inputVsOutputInnerFilePaths))]
        public void FixSyntaxTest(string inputInnerFilePath, string outputInnerFilePath)
        {
            // Arrange
            var sut = new VolvoTruckUploadChanger();
            var inputFilePath = TestUtils.GetFilePathInTestProject(inputInnerFilePath);
            var fileContent = File.ReadAllText(inputFilePath);
            var expectedFilePath = TestUtils.GetFilePathInTestProject(outputInnerFilePath);

            // Act
            sut.ShouldFixSyntax(fileContent);
            var result = sut.FixSyntax();

            //File.WriteAllText(expectedFilePath, result);

            // Assert
            var expectedContent = File.ReadAllText(expectedFilePath);
            Assert.AreEqual(expectedContent, result);
        }


        [Test]
        public void HasSpotDataDefsInFile()
        {
            // Arrange
            var sut = new VolvoTruckUploadChanger();

            // Act
            var result = sut.ShouldFixSyntax(inputSpotData);

            // Assert
            Assert.IsTrue(result);
        }
    }
}