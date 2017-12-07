using System.IO.Abstractions;

namespace FileSynchronizer.Core
{
    internal sealed class FileManager : IFileManager
    {
        private readonly IFileSystem _fileSystem;

        public FileManager(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Copy(string sourceFilePath, string destFilePath)
        {
            _fileSystem.File.Copy(sourceFilePath, destFilePath);
        }

        public void Delete(string path)
        {
            _fileSystem.File.Delete(path);
        }
    }
}
