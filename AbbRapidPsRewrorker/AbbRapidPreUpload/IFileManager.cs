namespace AbbRapidPreUploader
{
    public interface IFileManager
    {
        string FileReadAllText(string filePath);

        /// <summary>
        /// Creates a backup copy of a file by changing its extension to '.backup'.
        /// </summary>
        /// <param name="filePath">The path to the file that needs to be backed up.</param>
        /// <returns>The path to the created backup file.</returns>
        string FileCreateFileBackup(string filePath);

        void FileSaveAllText(string contents, string filePath);
    }
}