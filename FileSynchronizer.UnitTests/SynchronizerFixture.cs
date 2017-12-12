using System.IO.Abstractions.TestingHelpers;
using FileSynchronizer.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSynchronizer.UnitTests
{
    [TestFixture]
    public class SynchronizerFixture

    {
        private string _sourceRootPath;
        private string _destinationRootPath;
        private bool _isDeleteActive;
        private MockFileSystem _mockFileSystem;
        private ISynchronizer _synchronizer;
        private List<string> _filesPathList;

        [SetUp]
        public void Init()
        {
            string _tempFolderPath;
            string _tempFilePath;

            _sourceRootPath = @"C:\Synchronizer\Storage1\";
            _destinationRootPath = @"D:\Storage1\";
            _isDeleteActive = false;
            _mockFileSystem = new MockFileSystem();
            _filesPathList = new List<string> { };

            for (int i = 1; i < 3; i++)
            {
                _tempFolderPath = _sourceRootPath + "Folder" + i;
                _mockFileSystem.AddDirectory(_tempFolderPath);
                for (int j = 1; j < 3; j++)
                {
                    _tempFilePath = _tempFolderPath + "\\file" + j + ".txt";
                    _mockFileSystem.AddFile(_tempFilePath, MockFileData.NullObject);
                }
            }

            for (int i = 1; i < 2; i++)
            {
                _tempFolderPath = _destinationRootPath + "Folder" + i;
                _mockFileSystem.AddDirectory(_tempFolderPath);
                for (int j = 1; j < 2; j++)
                {
                    _tempFilePath = _tempFolderPath + "\\file" + j + ".txt";
                    _mockFileSystem.AddFile(_tempFilePath, MockFileData.NullObject);
                }
            }

            _synchronizer = new Synchronizer(_mockFileSystem);
        }

        [Test]
        public void FilesCopyedSuccesfully()
        {
            _synchronizer.Synchronize(_sourceRootPath, _destinationRootPath, _isDeleteActive);

            List<string> sourceFilesPath = GetAllFiles(_sourceRootPath);
            List<string> destinationFilesPath = GetAllFiles(_destinationRootPath);

            Assert.That(IsFilesExistInDestinationLists(sourceFilesPath, destinationFilesPath), Is.True);
        }

        [Test]
        public void SynchronizeFilesWithDeletingSuccesfully()
        {
            _isDeleteActive = true;
            string sourceFilePath;
            List<string> filesToDeleteInDestinationDirectory = new List<string> { };
            List<string> sourceFilesPath = GetAllFiles(_sourceRootPath);
            List<string> destinationFilesPath = GetAllFiles(_destinationRootPath);

            foreach (var filePath in destinationFilesPath)
            {
                sourceFilePath = filePath.Replace(_destinationRootPath, _sourceRootPath);
                if (sourceFilesPath.IndexOf(sourceFilePath) == -1)
                {
                    filesToDeleteInDestinationDirectory.Add(filePath);
                }
            }
            _synchronizer.Synchronize(_sourceRootPath, _destinationRootPath, _isDeleteActive);
            List<string> destinationFilesPathAfterSynchronization = GetAllFiles(_destinationRootPath);
            Assert.That(IsFileDeleted(filesToDeleteInDestinationDirectory, destinationFilesPathAfterSynchronization), Is.True);

        }

        private List<string> GetAllFiles(string directoryPath)
        {
            string[] files = _mockFileSystem.Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                _filesPathList.Add(file);
            }
            return _filesPathList;
        }

        private bool IsFilesExistInDestinationLists(List<string> sourceFilesPath, List<string> destinationFilesPath)
        {
            string destFilePath;
            bool isFileExist = true;

            foreach (var filePath in sourceFilesPath)
            {
                destFilePath = filePath.Replace(_sourceRootPath, _destinationRootPath);
                if (destinationFilesPath.IndexOf(destFilePath) == -1)
                {
                    isFileExist = false;
                    break;
                }
            }
            return isFileExist;
        }

        private bool IsFileDeleted(List<string> filesToDeleteInDestinationDirectory, List<string> destinationFilesPathAfterSynchronization)
        {
            bool isFileDeleted = true;
            foreach (var filePath in filesToDeleteInDestinationDirectory)
            {
                if (destinationFilesPathAfterSynchronization.IndexOf(filePath) != -1)
                {
                    isFileDeleted = false;
                    break;
                }
            }
            return isFileDeleted;
        }
    }
}
