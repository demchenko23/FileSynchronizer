using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FileSynchronizer.Core;
using System.IO.Abstractions.TestingHelpers;

namespace FileSynchronizer.UtitTests
{
    [TestFixture]
    public class SynchronizerFixture
    {
        private string _sourceDirectoryPath;
        private string _destinationDirectoryPath;
        private bool _isDeleteActive;
        private MockFileSystem _mockFileSystem;
        private IFileManager _fileManager;
        private ISynchronizer _synchronizer;
        private string tempFolderPath;
        private string tempFilePath;

        [SetUp]
        public void Init()
        {
            _sourceDirectoryPath = @"C:\Synchronizer\Storage1\";
            _destinationDirectoryPath = @"D:\Storage1\";
            _isDeleteActive = false;
            _mockFileSystem = new MockFileSystem();

            for (int i = 0; i < 3; i++)
            {
                tempFolderPath = _sourceDirectoryPath + "Folder" + i;
                _mockFileSystem.AddDirectory(tempFolderPath);
                for (int j = 0; j < 3; j++)
                {
                    tempFilePath = tempFolderPath + "\\file" + j + ".txt";
                    _mockFileSystem.AddFile(tempFilePath, MockFileData.NullObject);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                tempFolderPath = _destinationDirectoryPath + "Folder" + i;
                _mockFileSystem.AddDirectory(tempFolderPath);
                for (int j = 0; j < 2; j++)
                {
                    tempFilePath = tempFolderPath + "\\file" + j + ".txt";
                    _mockFileSystem.AddFile(tempFilePath, MockFileData.NullObject);
                }
            }

            _fileManager = new FileManager(_mockFileSystem);

        }

        [Test]
        public void FilesCopyedSuccesfuly()
        {
            _synchronizer = new Synchronizer(_fileManager);
            _synchronizer.Synchronize(_sourceDirectoryPath, _destinationDirectoryPath, false);
        }
    }
}
