﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSynchronizer.Core
{
    public class FileManager : IFileManager
    {
        public override void CopyOperation(string sourcePath, string destPath)
        {
            try
            {
                File.Copy(sourcePath, destPath);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }

        public override void DeleteOperation(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
