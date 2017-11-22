using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Abstractions;

namespace FileSynchronizer.Core
{
    internal sealed class Synchronizer : ISynchronizer
    {
        private string [] _folders;
        private string [] _files;
        private string _destinationFolder;
        private string _destinationFile;
        private string _sourceFolder;
        private string _sourceFile;
        private IFileManager _fileManager;

        public Synchronizer(IFileManager fileManager)
        {
            _fileManager = fileManager;
                //new FileManager(new FileSystem());
        }

        public void Synchronize(string sourceDirectoryPath, string destinationDirectoryPath, bool isDeleteActive)
        {
            if (isDeleteActive)
            {
                SynchronizeDelete(sourceDirectoryPath, destinationDirectoryPath);
            }
            SynchronizeCopy(sourceDirectoryPath, destinationDirectoryPath);
        }

        private void SynchronizeCopy(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            _folders = Directory.GetDirectories(sourceDirectoryPath);
            _files = Directory.GetFiles(sourceDirectoryPath);

            if (_folders.Length > 0)
            {
                foreach (var folder in _folders)
                {
                    _destinationFolder = Path.Combine(destinationDirectoryPath, Path.GetDirectoryName(folder));
                    SynchronizeCopy(folder, _destinationFolder);
                }
            }
            if (_files.Length > 0)
            {
                foreach (var file in _files)
                {
                    _destinationFile = Path.Combine(destinationDirectoryPath, Path.GetFileName(file));

                    if (File.Exists(_destinationFile))
                    {
                        return;
                    }
                    else
                    {
                        _fileManager.Copy(file, _destinationFile);
                    }
                }
            }
        }

        private void SynchronizeDelete(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            _folders = Directory.GetDirectories(destinationDirectoryPath);
            _files = Directory.GetFiles(destinationDirectoryPath);

            if (_folders.Length > 0)
            {
                foreach (var folder in _folders)
                {
                    _sourceFolder = Path.Combine(sourceDirectoryPath, Path.GetDirectoryName(folder));
                    SynchronizeDelete(folder, _sourceFolder);
                }
            }
            if (_files.Length > 0)
            {
                foreach (var file in _files)
                {
                    _sourceFile = Path.Combine(sourceDirectoryPath, Path.GetFileName(file));

                    if (File.Exists(_sourceFile))
                    {
                        return;
                    }
                    else
                    {
                        _fileManager.Delete(file);
                    }
                }
            }
        }
    }
}
