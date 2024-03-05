using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbbRapidPreUploader
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);

            try
            {
                Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(Environment.NewLine, args));
                Console.WriteLine(e);
                Console.ReadLine();
            }

            Console.WriteLine(Environment.NewLine + "Finished!");
        }

        private static void Run(string[] args)
        {
            var fileManager = new FileManager();
            var uploadFilePath = new PsTempFileReader(fileManager).GetDetailsFromUploadTempFile(args[0], out var rrsVersion);

            Console.WriteLine(uploadFilePath);

            var uploadFileContent = fileManager.FileReadAllText(uploadFilePath);

            var volvoUploadChanger = new VolvoTruckUploadChanger();

            if (volvoUploadChanger.ShouldFixSyntax(uploadFileContent))
            {
                var fileContentAfterChanges = volvoUploadChanger.FixSyntax();

                //Console.WriteLine(fileContentAfterChanges);
                //Console.WriteLine(uploadFilePath);

                fileManager.FileSaveAllText(fileContentAfterChanges, uploadFilePath);
            }

            //Console.ReadLine();
        }
    }
}
