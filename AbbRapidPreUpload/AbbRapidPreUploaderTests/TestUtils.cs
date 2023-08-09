using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbbRapidPreUploaderTests
{
    public static class TestUtils
    {
        public static string GetFilePathInTestProject(string fileNameWithExtension)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var projectDir = new DirectoryInfo(buildDir).Parent.Parent.FullName;
            var filePath = projectDir + @"\TestFiles\" + fileNameWithExtension;
            return filePath;
        }
    }
}
