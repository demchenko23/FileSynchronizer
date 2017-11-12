using System;
using NUnit.Framework;
using FileSynchronizer.Core;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;


namespace FileSynchronizer.UtitTests
{
    [TestFixture]
    public class TestFileManager
    {
        public static string sourceFilePath = @"C:\file.txt";
        public static string destDirPath = @"D:\";
        public static string destFilePath = Path.Combine(destDirPath, Path.GetFileName(sourceFilePath));

        [Test]
        public void Copy_SourceFileExists_DestinationDirDoesNotExist_DestinationFileCopied()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(sourceFilePath, MockFileData.NullObject);
            var fileManager = new FileManager(mockFileSystem);
            fileManager.Copy(sourceFilePath, destDirPath);
            Assert.That(mockFileSystem.File.Exists(destFilePath), Is.True);

        }
        [Test]
        public void Copy_SourceFileExists_DestinationDirAndFileExists_CatchIOException()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(sourceFilePath, MockFileData.NullObject);
            mockFileSystem.AddFile(destFilePath, MockFileData.NullObject);
            var fileManager = new FileManager(mockFileSystem);
            Assert.Throws<IOException>(()=> fileManager.Copy(sourceFilePath, destDirPath));

        }
        [Test]
        public void Delete_FileExist_FileDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(sourceFilePath, MockFileData.NullObject);
            var fileManager = new FileManager(mockFileSystem);
            fileManager.Delete(sourceFilePath);
            mockFileSystem.RemoveFile(sourceFilePath);
            Assert.That(mockFileSystem.File.Exists(sourceFilePath), Is.False);
        }
    }
}
