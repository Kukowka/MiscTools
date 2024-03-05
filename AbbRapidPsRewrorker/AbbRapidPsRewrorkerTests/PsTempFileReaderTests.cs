using AbbRapidPreUploader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AbbRapidPreUploader.Tests
{
    [TestFixture]
    public class PsTempFileReaderTests
    {
        [Test]
        public void GetDetailsFromUploadTempFile_Should_Return_UploadFilePath()
        {
            // Arrange
            var fileManagerMock = new Mock<IFileManager>();
            string tmpFilePath = "dummy_path";
            string content =
                @"ROBOT_NAME,2V3V1_AR21_IRB6700_220-265_SW_IRC5_Grey
CONTROLLER_VERSION,
RRS_VERSION,VolvoTruck_6.01.irc5
MACHINE_DATA_FOLDER,\\fs125\PD\M1A22120_2206\RobotsMachineDataFiles\A37C6C28-9DFC-4417-8E74-57824D3EA6C2\
UPLOAD_FILE,c:\TestFile.mod
STUDY,2V3V1";

            fileManagerMock.Setup(fm => fm.FileReadAllText(tmpFilePath)).Returns(content);

            var sut = new PsTempFileReader(fileManagerMock.Object);

            // Act
            string result = sut.GetDetailsFromUploadTempFile(tmpFilePath, out var rrsVersion);

            // Assert
            Assert.AreEqual(@"c:\TestFile.mod", result);
            Assert.AreEqual(6, rrsVersion);
        }

        private static string _downloadConfigFile1 =
                @"ROBOT_NAME,2V3V1_AR21_IRB6700_220-265_SW_IRC5_Grey
CONTROLLER_VERSION,
RRS_VERSION,VolvoTruck_6.10.irc5
MACHINE_DATA_FOLDER,\\fs125\PD\M1A22120_2206\RobotsMachineDataFiles\A37C6C28-9DFC-4417-8E74-57824D3EA6C2\
DOWNLOAD_FOLDER,C:\Users\kukowka\Desktop
DOWNLOAD_FILES,P2545L1ESegment.mod,P2545L1ESegment.log
STUDY,2V3V1";

        public static IEnumerable<TestCaseData> _downloadConfigFiles
        {
            get
            {
                yield return new TestCaseData(_downloadConfigFile1, @"C:\Users\kukowka\Desktop\P2545L1ESegment.mod");
                yield return new TestCaseData(_downloadConfigFile2, @"C:\Users\kukowka\Desktop\P2545L1ESegment.mod");
            }
        }


        private static string _downloadConfigFile2 =
        @"ROBOT_NAME,2V3V1_AR21_IRB6700_220-265_SW_IRC5_Grey
CONTROLLER_VERSION,
RRS_VERSION,VolvoTruck_6.10.irc5_VIBN
MACHINE_DATA_FOLDER,\\fs125\PD\M1A22120_2206\RobotsMachineDataFiles\A37C6C28-9DFC-4417-8E74-57824D3EA6C2\
DOWNLOAD_FOLDER,C:\Users\kukowka\Desktop
DOWNLOAD_FILES,P2545L1ESegment.mod,P2545L1ESegment.log
STUDY,2V3V1";



        [Test]
        [TestCaseSource(nameof(_downloadConfigFiles))]
        public void GetDetailsFromDownloadTempFile_Should_Return_UploadFilePath(string inputFileContent, string expectedFilePath)
        {
            // Arrange
            var fileManagerMock = new Mock<IFileManager>();
            string tmpFilePath = "dummy_path";

            fileManagerMock.Setup(fm => fm.FileReadAllText(tmpFilePath)).Returns(inputFileContent);

            var sut = new PsTempFileReader(fileManagerMock.Object);

            // Act
            string result = sut.GetDetailsFromDownloadTempFile(tmpFilePath, out var rrsVersion);

            // Assert
            Assert.AreEqual(expectedFilePath, result);
            Assert.AreEqual(6, rrsVersion);
        }
    }
}