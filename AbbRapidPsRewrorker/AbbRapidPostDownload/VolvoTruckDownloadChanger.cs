using AbbRapidPreUploader;
using System;
using System.Linq;
using TMS_Utils.Utils.BackupReading.Backup.ABB;
using TMS_Utils.Utils.BackupReading.Backup.ABB.AbbLocalDefs;
using TMS_Utils.Utils.BackupReading.BackupReaders.ABB;

namespace AbbRapidPostDownload
{
    public class VolvoTruckDownloadChanger
    {
        public const int REQUIRED_NR_OF_PARAM_FOR_RRS5 = 5;
        public const int REQUIRED_NR_OF_PARAM_FOR_RRS6 = 1;

        private readonly Std_BR_DefReader _localDefReader;
        private Std_BR_ABBPorscheProcReader _reader;

        public VolvoTruckDownloadChanger(Std_BR_DefReader localDefReader, Std_BR_ABBPorscheProcReader reader)
        {
            _localDefReader = localDefReader;
            _reader = reader;
        }


        public bool ShouldFixSyntaxInDownload(string downloadFileContent, int rrsVersion)
        {
            var allDefs = _localDefReader.GetModuleDefinitions(downloadFileContent);
            var result = allDefs.Where(m => m.DefType == Std_BR_DefLine.DataTypes.SPOTDATA).Any((Std_BR_DefLine m) => Need2CorrectSpotData(m, rrsVersion));

            return result;
        }

        private bool Need2CorrectSpotData(Std_BR_DefLine defLine, int rrsVersion)
        {
            var nrOfParams = defLine.GetNumberOfParameterInDefVal();

            if (rrsVersion == 5)
            {
                if (nrOfParams > REQUIRED_NR_OF_PARAM_FOR_RRS5)
                    return true;
            }

            else if (rrsVersion == 6)
            {
                if (nrOfParams > REQUIRED_NR_OF_PARAM_FOR_RRS6)
                    return true;
            }

            else
                throw new ArgumentOutOfRangeException(nameof(rrsVersion));

            return false;
        }

        public string FixSyntaxInDownload(string uploadFileContent, int rrsVersion)
        {
            uploadFileContent = AddMissingSpotDataParams(uploadFileContent, rrsVersion);

            uploadFileContent = SwapSpotDataWithGunData(uploadFileContent);

            return uploadFileContent;
        }

        private string SwapSpotDataWithGunData(string uploadFileContent)
        {
            var procesInFile = _reader.ReadAllProcPrgInModFile(uploadFileContent, "", out _);

            foreach (var proc in procesInFile)
            {
                foreach (Std_BR_AbbPorscheBackupPoint point in proc.ProcessPoints)
                {
                    if (VolvoTruckUploadChanger.HasPointSpotMotion(point))
                    {
                        var newPointCall = VolvoTruckUploadChanger.SwapSpotDataWithGunData(point);
                        uploadFileContent = uploadFileContent.Replace(point.CallDefAsText, newPointCall);
                    }
                }
            }

            return uploadFileContent;
        }

        private string AddMissingSpotDataParams(string uploadFileContent, int rrsVersion)
        {
            var allDefs = _localDefReader.GetModuleDefinitions(uploadFileContent);

            foreach (Std_BR_DefLine allDef in allDefs)
            {
                if (allDef.DefType == Std_BR_DefLine.DataTypes.SPOTDATA)
                {
                    string newValue = AddMissingSpotDataParams(allDef, rrsVersion);
                    uploadFileContent = uploadFileContent.Replace(allDef.DefAsText, newValue);
                }
            }

            return uploadFileContent;
        }

        private string AddMissingSpotDataParams(Std_BR_DefLine line, int rrsVersion)
        {
            var withoutEnding = line.DefAsText.Substring(0, line.DefAsText.Length - 2);

            if (rrsVersion == 5)
                withoutEnding += ",0];";
            else if (rrsVersion == 6)
                withoutEnding += ",1,-1];";

            return withoutEnding;
        }
    }
}