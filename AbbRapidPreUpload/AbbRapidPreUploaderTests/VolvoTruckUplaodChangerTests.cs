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

        public static IEnumerable<TestCaseData> _inputVsOutputInnerFilePaths
        {
            get
            {
                yield return new TestCaseData("P2545L1ESegmentOrg.mod", "P2545L1ESegmentResult.mod");
                yield return new TestCaseData("P2540SegmentOrg.mod", "P2540SegmentResult.mod");
                yield return new TestCaseData("FML2H3SegmentOrg.mod", "FML2H3SegmentResult.mod");
                yield return new TestCaseData("FH24BSegmentAOrg.mod", "FH24BSegmentResult.mod");


            }
        }

        [Test]
        [TestCaseSource(nameof(_inputVsOutputInnerFilePaths))]
        public void FixSyntaxTest(string inputInnerFilePath, string outputInnerFilePath)
        {
            // Arrange
            var sut = new VolvoTruckUplaodChanger(new FileManager());
            var inputFilePath = TestUtils.GetFilePathInTestProject(inputInnerFilePath);
            var fileContent = File.ReadAllText(inputFilePath);
            var expectedFilePath = TestUtils.GetFilePathInTestProject(outputInnerFilePath);

            // Act
            sut.ShouldFixSyntax(fileContent);
            var result = sut.FixSyntax(fileContent, inputFilePath);

            //File.WriteAllText(expectedFilePath, result);

            // Assert
            var expectedContent = File.ReadAllText(expectedFilePath);
            Assert.AreEqual(expectedContent, result);
        }


        [Test]
        public void HasSpotDataDefsInFile()
        {
            // Arrange
            var sut = new VolvoTruckUplaodChanger(new FileManager());

            // Act
            var result = sut.ShouldFixSyntax(inputSpotData);

            // Assert
            Assert.IsTrue(result);
        }


    }
}