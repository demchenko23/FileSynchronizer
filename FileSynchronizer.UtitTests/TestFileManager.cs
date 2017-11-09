using System;
using NUnit.Framework;
using FileSynchronizer.Core;
using System.IO;

namespace FileSynchronizer.UtitTests
{
    [TestFixture]
    public class TestFileManager
    {
        [Test]
        public void CheckCopyOperation()
        {
            string sourcePath = @"C:\Users\Home\Desktop\source\1.txt";
            string destPath = @"C:\Users\Home\Desktop\dest\1.txt";
            FileManager fileManager = new FileManager();
            fileManager.CopyOperation(sourcePath, destPath);
            Assert.That(File.Exists(destPath).Equals(true));

        }

        [Test]
        public void CheckDeleteOperation()
        {
            string path = @"C:\Users\Home\Desktop\dest\1.txt";
            FileManager fileManager = new FileManager();
            fileManager.DeleteOperation(path);
            Assert.That(File.Exists(path).Equals(false));
        }
    }
}
