using System;

namespace FileSynchronizer.Core
{
    public interface IFileManager
    {
        void Copy(string sourceFilePath, string destFilePath);

        void Delete(string path);
    }
}
