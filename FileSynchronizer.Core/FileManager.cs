using System;
using System.IO;
using System.IO.Abstractions;

namespace FileSynchronizer.Core
{
    public class FileManager : IFileManager
    {
        private readonly IFileSystem _fileSystem;
        public FileManager(): this(new FileSystem())
        {
        }
        public FileManager(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Copy(string sourceFilePath, string destDirectoryPath)
        {
            if (!_fileSystem.Directory.Exists(destDirectoryPath))
            {
                _fileSystem.Directory.CreateDirectory(destDirectoryPath);
            }
            var fileName = _fileSystem.Path.GetFileName(sourceFilePath);
            var destFileName = _fileSystem.Path.Combine(destDirectoryPath, fileName);
            try
            {
                _fileSystem.File.Copy(sourceFilePath, destFileName);
            }
            // Handle exception if the file was already exist.
            catch (IOException e)
            {
                throw;
            }

        }

        public void Delete(string path)
        {
            if (File.Exists(path))

            {
                try
                {
                    File.Delete(path);
                }
                //Handle exception if file already being opened by another process.
                catch (IOException e)
                {
                    return;
                }
                //Handle exception if file is in read only mode.
                catch (UnauthorizedAccessException e)
                {
                    return;
                }
            }
        }
    }
}
