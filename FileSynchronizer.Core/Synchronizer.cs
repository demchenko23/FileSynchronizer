using System.IO;
using System.IO.Abstractions;

namespace FileSynchronizer.Core
{
    internal sealed class Synchronizer : ISynchronizer
    {
        private string _destinationFolder;
        private string _destinationFile;
        private string _sourceFolder;
        private string _sourceFile;
        private IFileSystem _fileSystem;

        public Synchronizer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Synchronize(string sourceRootPath, string destinationRootPath, bool isDeleteActive)
        {
            if (isDeleteActive)
            {
                SynchronizeDelete(sourceRootPath, destinationRootPath);
            }

            SynchronizeCopy(sourceRootPath, destinationRootPath);
        }

        private void SynchronizeCopy(string sourceRootPath, string destinationRootPath)
        {
            string[] files = _fileSystem.Directory.GetFiles(sourceRootPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                _destinationFile = file.Replace(sourceRootPath, destinationRootPath);
                _destinationFolder = _fileSystem.Path.GetDirectoryName(_destinationFile);
                if (!_fileSystem.Directory.Exists(_destinationFolder))
                {
                    _fileSystem.Directory.CreateDirectory(_destinationFolder);
                }

                if (!_fileSystem.File.Exists(_destinationFile))
                {
                    _fileSystem.File.Copy(file, _destinationFile);
                }
            }
        }

        private void SynchronizeDelete(string sourceRootPath, string destinationRootPath)
        {
            string[] files = _fileSystem.Directory.GetFiles(destinationRootPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                _sourceFile = file.Replace(destinationRootPath, sourceRootPath);

                if (!_fileSystem.File.Exists(_sourceFile))
                {
                    _fileSystem.File.Delete(file);
                }
            }
        }
    }
}
