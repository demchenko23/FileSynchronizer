using System;
using System.Collections.Generic;
using System.Text;

namespace FileSynchronizer.Core
{
    public interface ISynchronizer
    {
        void Synchronize(string sourceDirectoryPath, string destinationDirectoryPath, bool isDeleteActive);
    }
}
