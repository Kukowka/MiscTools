using AbbRapidPostDownload;
using AbbRapidPreUploader;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using TMS_Utils.Utils.BackupReading.Backup.ABB.AbbLocalDefs;
using TMS_Utils.Utils.BackupReading.BackupReaders.ABB;

namespace AbbRapidPreUploaderTests
{
    [TestFixture]
    public class VolvoTruckDownloadChangerTests
    {
        public static IEnumerable<TestCaseData> _inputVsOutputInnerFilePaths
        {
            get
            {
                yield return new TestCaseData("Irc5UploadResult.mod", "Irc5UploadOrg.mod", 5);
                yield return new TestCaseData("Irc6UploadResult.mod", "Irc6UploadOrg.mod", 6);
            }
        }

        [Test]
        [TestCaseSource(nameof(_inputVsOutputInnerFilePaths))]
        public void FixSyntaxTest(string inputInnerFilePath, string outputInnerFilePath, int rrsVersion)
        {
            // Arrange
            var sut = new VolvoTruckDownloadChanger(new Std_BR_DefReader(), new Std_BR_ABBPorscheProcReader());
            var inputFilePath = TestUtils.GetFilePathInTestProject(inputInnerFilePath);
            var fileContent = File.ReadAllText(inputFilePath);
            var expectedFilePath = TestUtils.GetFilePathInTestProject(outputInnerFilePath);

            // Act
            var result = sut.FixSyntaxInDownload(fileContent, rrsVersion);

            //File.WriteAllText(expectedFilePath, result);

            // Assert
            var expectedContent = File.ReadAllText(expectedFilePath);
            Assert.AreEqual(expectedContent, result);
        }

    }
}