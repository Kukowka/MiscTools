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
            var uploadFilePath = new PsTempFileReader(fileManager).GetUploadFilePath(args[0]);

            Console.WriteLine(uploadFilePath);

            var uploadFileContent = fileManager.FileReadAllText(uploadFilePath);

            var volvoUploadChanger = new VolvoTruckUplaodChanger(fileManager);

            if (volvoUploadChanger.ShouldFixSyntax(uploadFileContent))
            {
                fileManager.FileCreateFileBackup(uploadFilePath);
                var fileContentAfterChanges = volvoUploadChanger.FixSyntax(uploadFileContent, uploadFilePath);

                //Console.WriteLine(fileContentAfterChanges);
                //Console.WriteLine(uploadFilePath);

                fileManager.FileSaveAllText(fileContentAfterChanges, uploadFilePath);
            }
        }
    }
}
