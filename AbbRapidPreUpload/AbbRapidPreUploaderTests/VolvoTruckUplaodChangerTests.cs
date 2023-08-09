using AbbRapidPreUploader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AbbRapidPreUploaderTests;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AbbRapidPreUploader.Tests
{
    [TestFixture]
    public class VolvoTruckUplaodChangerTests
    {
        private static string inputSpotData = @"LOCAL PERS spotdata sd_wp432810:=[432810,1,1,1,1,-1];";

        [Test]
        public void FixSyntaxTest()
        {
            // Arrange
            var sut = new VolvoTruckUplaodChanger(new FileManager());
            var inputFilePath = TestUtils.GetFilePathInTestProject("P2545L1ESegmentOrg.mod");
            var fileContent = File.ReadAllText(inputFilePath);
            var expectedFilePath = TestUtils.GetFilePathInTestProject("P2545L1ESegmentResult.mod");
            var expectedContent = File.ReadAllText(expectedFilePath);


            // Act
            var result = sut.FixSyntax(fileContent);


            //File.WriteAllText(outputFilePath, result);


            // Assert
            Assert.AreEqual(expectedContent, result);
        }


        [Test]
        public void HasSpotDataDefsInFile()
        {
            // Arrange
            var sut = new VolvoTruckUplaodChanger(new FileManager());

            // Act
            var result = sut.HasSpotDataDefsInText(inputSpotData);

            // Assert
            Assert.IsTrue(result);
        }


    }
}