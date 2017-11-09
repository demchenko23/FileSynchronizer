using System;

namespace FileSynchronizer.Core
{
    public interface IFileManager
    {
        void CopyOperation(string sourcePath, string destPath);

        void DeleteOperation(string path);
    }
}
