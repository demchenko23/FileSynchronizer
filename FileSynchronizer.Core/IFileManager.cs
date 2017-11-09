using System;

namespace FileSynchronizer.Core
{
    public class IFileManager
    {
        public virtual void CopyOperation(string sourcePath, string destPath) { }

        public virtual void DeleteOperation(string path) { }
    }
}
