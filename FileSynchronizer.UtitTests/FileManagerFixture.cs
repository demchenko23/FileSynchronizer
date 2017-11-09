using System;
using NUnit.Framework;
using FileSynchronizer.Core;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;


namespace FileSynchronizer.UtitTests
{
    [TestFixture]
    public class FileManagerFixture
    {
        private string _sourceFilePath;
        private string _destDirPath;
        private string _destFilePath;
        private MockFileSystem _mockFileSystem;
        private IFileManager _fileManager;

        [SetUp]
        public void Init()
        {
            _sourceFilePath = @"C:\test\file.txt";
            _destDirPath = @"D:\test\";
            _destFilePath = Path.Combine(_destDirPath, Path.GetFileName(_sourceFilePath));
            _mockFileSystem = new MockFileSystem();
            _mockFileSystem.AddFile(_sourceFilePath, MockFileData.NullObject);
            _mockFileSystem.AddDirectory(_destDirPath);
            _fileManager = new FileManager(_mockFileSystem);
        }

        [Test]
        public void WhenFileDoesNotExists_FileShoudBeCopied()
        {
            _fileManager.Copy(_sourceFilePath, _destFilePath);
            Assert.That(_mockFileSystem.File.Exists(_destFilePath), Is.True);

        }

        [Test]
        public void WhenFileExist_ExceptionShoudBeThrown()
        {
            _mockFileSystem.AddFile(_destFilePath, MockFileData.NullObject);
            Assert.Throws<IOException>(()=> _fileManager.Copy(_sourceFilePath, _destDirPath));

        }

        [Test]
        public void WhenFileExist_FileDeletedSuccesfuly()
        {
            _fileManager.Delete(_sourceFilePath);
            Assert.That(_mockFileSystem.File.Exists(_sourceFilePath), Is.False);
        }
    }
}
