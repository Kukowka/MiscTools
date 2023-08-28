using System;
using System.IO;

namespace AbbRapidPreUploader
{
    public class FileManager : IFileManager
    {
        public string FileReadAllText(string filePath) => File.ReadAllText(filePath);

        /// <summary>
        /// Creates a backup copy of a file by changing its extension to '.backup'.
        /// </summary>
        /// <param name="filePath">The path to the file that needs to be backed up.</param>
        /// <returns>The path to the created backup file.</returns>
        public string FileCreateFileBackup(string filePath)
        {

            try
            {
                // Check if the file exists at the specified path
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The specified file does not exist.", filePath);

                // Get the directory and filename from the provided file path
                string directory = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                // Create a new file name for the backup copy with .backup extension
                string backupFileName = fileName + ".backup";
                string backupFilePath = Path.Combine(directory, backupFileName);

                // Copy the file to the backup file path
                if (!File.Exists(backupFilePath))
                    File.Copy(filePath, backupFilePath);

                return backupFilePath;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the backup process
                // You can log the exception or take appropriate actions here
                throw new Exception("An error occurred while creating the file backup.", ex);
            }
        }


        public void FileSaveAllText(string contents, string filePath) => File.WriteAllText(filePath, contents);
    }
}