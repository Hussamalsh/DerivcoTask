﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Student.Data.Models
{
    public class Utility
    {
        public static string getAbsolutePath(string folder, bool createIfNoDirectory = false)
        {
            string rtrnPath = Path.Combine(getAppBasePath(), folder);
            if ((createIfNoDirectory) && (!System.IO.Directory.Exists(folder)))
            {
                Directory.CreateDirectory(rtrnPath);
            }
            return (rtrnPath);
        }

        public static string getAbsolutePath(string folder, string fileName, bool createIfNoDirectory = false)
        {
            string rtrnPath = Path.Combine(getAppBasePath(), folder);

            if ((createIfNoDirectory) && (!System.IO.Directory.Exists(folder)))
            {
                Directory.CreateDirectory(rtrnPath);
            }
            rtrnPath = Path.Combine(rtrnPath, fileName);

            return (rtrnPath);
        }

        public static string getAppBasePath()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(codeBase);
        }
    }
}
