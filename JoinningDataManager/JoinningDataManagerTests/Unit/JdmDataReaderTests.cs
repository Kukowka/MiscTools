using NUnit.Framework;
using System.Linq;

namespace JoinningDataManager.Tests
{
    [TestFixture]
    public class JdmDataReaderTests
    {
        //[Test]
        //public void ExtractVtaPoints_EachPointShouldHaveNameAndXyz()
        //{
        //    ////arrange
        //    //var sut = new JdmDataReaderXmls();

        //    ////act
        //    //var vtaPoint = sut.ExtractVtaPointsFromXmls(Program.VTA_XMLS_PATH, JdmConst.FIELD_NAMES);

        //    ////assert
        //    //Assert.AreEqual(0, vtaPoint.Count(m => m.Name is null));
        //    //Assert.AreEqual(0, vtaPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_X) is null));
        //    //Assert.AreEqual(0, vtaPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_Y) is null));
        //    //Assert.AreEqual(0, vtaPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_Z) is null));
        //    //Assert.AreEqual(0, vtaPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_PROCESS) is null));

        //}

        [Test]
        public void ExtractVdlPointsTest_EachPointShouldHaveNameAndXyz()
        {
            //arrange
            var sut = new JdmDataReaderVdlExcel();

            //act
            var vdlPoint = sut.ExtractVdlPoints(Program.VDL_EXCEL_PATH, Program.VDL_EXCEL_SHEET_NAME, JdmConst.VDL_COLUMN_CONFIG, Program.VDL_START_ROW_INDEX);

            //assert
            Assert.AreEqual(0, vdlPoint.Count(m => m.Name is null));

            //var tmp = vtaPoint.Where(m => m.GetFields2Compare(JdmConst.FIELD_NAME_Y) is null);
            Assert.AreEqual(0, vdlPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_X) is null));
            Assert.AreEqual(0, vdlPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_Y) is null));
            Assert.AreEqual(0, vdlPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_Z) is null));
            Assert.AreEqual(0, vdlPoint.Count(m => m.GetField2Compare(JdmConst.FIELD_NAME_PROCESS) is null));

        }
    }
}