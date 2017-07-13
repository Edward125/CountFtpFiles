using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CountFtpFiles
{
    class p
    {
        public static string AppFolder = @".\" + Application.ProductName;

        public static string FtpIP = string.Empty;
        public static string FtpID = string.Empty;
        public static string FtpPassword = string.Empty;
        public static string FtpFolder = string.Empty;


        /// <summary>
        /// check app folder
        /// </summary>
        /// <returns></returns>
        public static bool checkFolder()
        {
            if (!Directory.Exists(AppFolder))
            {
                try
                {
                    Directory.CreateDirectory(AppFolder);
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return true;
        }
    }
}
