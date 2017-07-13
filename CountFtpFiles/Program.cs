using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CountFtpFiles
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!p.checkFolder())
            {
                MessageBox.Show("Create app folder fail,program will exit.", "Create Folder Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                System.Threading.Thread.Sleep(1000);
               // SplashForm.CloseSplash();
                Environment.Exit(0);
            }

            if (!File.Exists(@".\IrisSkin4.dll"))
            {
                if (!downloadIrisSkin4())
                {
                    System.Threading.Thread.Sleep(1000);
                   // SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }


            if (!File.Exists(@".\System.Data.SQLite.dll"))
            {
                if (!downloadSqliteDll())
                {
                    System.Threading.Thread.Sleep(1000);
                    //SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }

            if (!File.Exists(@".\SQLite.Interop.dll"))
            {
                if (!downloadSqliteInterop())
                {
                    System.Threading.Thread.Sleep(1000);
                   // SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }




            if (!File.Exists(p.AppFolder + @"\MacOS.ssk"))
            {
                if (!downloadSkin())
                {
                    System.Threading.Thread.Sleep(1000);
                    //SplashForm.CloseSplash();
                    Environment.Exit(0);
                }
            }

            Application.Run(new frmMain());






          
        }



        /// <summary>
        /// download irisskin4.dll
        /// </summary>
        /// <returns></returns>
        private static bool downloadIrisSkin4()
        {
            string filePath = @".\IrisSkin4.dll";
            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.IrisSkin4;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    //  File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }

        private static bool downloadSqliteDll()
        {
            string filePath = @".\System.Data.SQLite.dll";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.System_Data_SQLite;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    //  File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }

        private static bool downloadSqliteInterop()
        {
            string filePath = @".\SQLite.Interop.dll";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.SQLite_Interop;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    //  File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// download skin file
        /// </summary>
        /// <returns></returns>
        private static bool downloadSkin()
        {
            string filePath = p.AppFolder + @"\MacOS.ssk";

            if (!File.Exists(filePath))
            {
                byte[] template = Properties.Resources.MacOS;
                FileStream stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    stream.Write(template, 0, template.Length);
                    stream.Close();
                    stream.Dispose();
                    File.SetAttributes(filePath, FileAttributes.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            return true;

        }
    }
}
