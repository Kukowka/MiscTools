using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbbRapidPreUploader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AbbRapidPreUploader.Tests
{
    [TestFixture]
    public class PsTempFileReaderTests
    {
        [Test]
        public void GetUploadFilePath_Should_Return_UploadFilePath()
        {
            // Arrange
            var fileManagerMock = new Mock<IFileManager>();
            string tmpFilePath = "dummy_path";
            string content =
                @"ROBOT_NAME,2V3V1_AR21_IRB6700_220-265_SW_IRC5_Grey
CONTROLLER_VERSION,
RRS_VERSION,VolvoTruc_6.01.irc5
MACHINE_DATA_FOLDER,\\fs125\PD\M1A22120_2206\RobotsMachineDataFiles\A37C6C28-9DFC-4417-8E74-57824D3EA6C2\
UPLOAD_FILE,c:\TestFile.mod
STUDY,2V3V1";

            fileManagerMock.Setup(fm => fm.FileReadAllText(tmpFilePath)).Returns(content);

            var sut = new PsTempFileReader(fileManagerMock.Object);

            // Act
            string result = sut.GetUploadFilePath(tmpFilePath);

            // Assert
            Assert.AreEqual(@"c:\TestFile.mod", result);
        }
    }
}