using AbbRapidPreUploader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_Utils.Utils.BackupReading.Backup.ABB.AbbLocalDefs;
using TMS_Utils.Utils.BackupReading.BackupReaders.ABB;

namespace AbbRapidPostDownload
{
    internal class Program
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
            var uploadFilePath = new PsTempFileReader(fileManager).GetDetailsFromDownloadTempFile(args[0], out var rrsVersion);

            Console.WriteLine(uploadFilePath);
            //Console.WriteLine("jestem");

            var uploadFileContent = fileManager.FileReadAllText(uploadFilePath);


            var volvoUploadChanger = new VolvoTruckDownloadChanger(new Std_BR_DefReader(), new Std_BR_ABBPorscheProcReader());

            if (volvoUploadChanger.ShouldFixSyntaxInDownload(uploadFileContent, rrsVersion))
            {
                var fileContentAfterChanges = volvoUploadChanger.FixSyntaxInDownload(uploadFileContent, rrsVersion);

                //Console.WriteLine(fileContentAfterChanges);
                //Console.WriteLine(uploadFilePath);

                fileManager.FileSaveAllText(fileContentAfterChanges, uploadFilePath);
            }

            //Console.ReadLine();
        }
    }
}
