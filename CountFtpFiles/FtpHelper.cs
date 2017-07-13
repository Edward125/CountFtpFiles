using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CountFtpFiles
{
      class FtpHelper
    {
        #region FTP获取文件列表

        /// <summary>
        /// FTP获取文件列表
        /// </summary>
        /// <param name="ftpServerIP">ftpip,eg:10.62.201.224</param>
        /// <param name="ftpUserID">login id</param>
        /// <param name="ftpPassword">login password</param>
        /// <returns></returns>
        public static string[] FTPGetFileList(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            //响应结果
            StringBuilder result = new StringBuilder();
            //FTP请求
            FtpWebRequest ftpRequest = null;
            //FTP响应
            WebResponse ftpResponse = null;
            //FTP响应流
            StreamReader ftpResponsStream = null;
            try
            {
                //生成FTP请求
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                //设置文件传输类型
                ftpRequest.UseBinary = true;
                //FTP登录
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                //设置FTP方法
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                //生成FTP响应
                ftpResponse = ftpRequest.GetResponse();
                //FTP响应流
                ftpResponsStream = new StreamReader(ftpResponse.GetResponseStream());
                string line = ftpResponsStream.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = ftpResponsStream.ReadLine();
                }
                //去掉结果列表中最后一个换行
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                //返回结果
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (null);
            }
            finally
            {
                if (ftpResponsStream != null)
                {
                    ftpResponsStream.Close();
                }

                if (ftpResponse != null)
                {
                    ftpResponse.Close();
                }
            }
        }





        /// <summary>
        /// FTP获取文件列表
        /// </summary>
        /// <param name="ftpServerIP">ftpip,eg:10.62.201.224</param>
        /// <param name="ftpUserID">login id</param>
        /// <param name="ftpPassword">login password</param>
        /// <param name="folder">获取的那个文件夹</param>
        /// <returns></returns>
        public static string[] FTPGetFileList(string ftpServerIP, string ftpUserID, string ftpPassword,string folder)
        {
            //响应结果
            StringBuilder result = new StringBuilder();
            //FTP请求
            FtpWebRequest ftpRequest = null;
            //FTP响应
            WebResponse ftpResponse = null;
            //FTP响应流
            StreamReader ftpResponsStream = null;
            try
            {
                //生成FTP请求
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + @"/" +@folder +"/"));
                //设置文件传输类型
                ftpRequest.UseBinary = true;
                //FTP登录
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                //设置FTP方法
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                //生成FTP响应
                ftpResponse = ftpRequest.GetResponse();
                //FTP响应流
                ftpResponsStream = new StreamReader(ftpResponse.GetResponseStream());
                string line = ftpResponsStream.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = ftpResponsStream.ReadLine();
                }
                //去掉结果列表中最后一个换行
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                //返回结果
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (null);
            }
            finally
            {
                if (ftpResponsStream != null)
                {
                    ftpResponsStream.Close();
                }

                if (ftpResponse != null)
                {
                    ftpResponse.Close();
                }
            }
        }
        #endregion

        #region FTP下载文件

        /// <summary>
        /// FTP下载文件
        /// </summary>
        /// <param name="ftpServerIP">FTP服务器IP</param>
        /// <param name="ftpUserID">FTP登录帐号</param>
        /// <param name="ftpPassword">FTP登录密码</param>
        /// <param name="saveFilePath">保存文件路径</param>
        /// <param name="saveFileName">保存文件名</param>
        /// <param name="downloadFileName">下载文件名</param>
        public  static void FTPDownloadFile(string ftpServerIP, string ftpUserID, string ftpPassword,
            string saveFilePath, string saveFileName, string downloadFileName)
        {
            //定义FTP请求对象
            FtpWebRequest ftpRequest = null;
            //定义FTP响应对象
            FtpWebResponse ftpResponse = null;
            //存储流
            FileStream saveStream = null;
            //FTP数据流
            Stream ftpStream = null;
            try
            {
                //生成下载文件
                saveStream = new FileStream(saveFilePath + "\\" + saveFileName, FileMode.Create);
                //生成FTP请求对象
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + downloadFileName));
                //设置下载文件方法
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                //设置文件传输类型
                ftpRequest.UseBinary = true;
                //设置登录FTP帐号和密码
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                //生成FTP响应对象
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                //获取FTP响应流对象
                ftpStream = ftpResponse.GetResponseStream();
                //响应数据长度
                long cl = ftpResponse.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                //接收FTP文件流
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)                
                {
                    saveStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }

                if (saveStream != null)
                {
                    saveStream.Close();
                }

                if (ftpResponse != null)
                {
                    ftpResponse.Close();
                }
            }
        }

        #endregion

        #region FTP上传文件

        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="ftpServerIP">FTP服务器IP</param>
        /// <param name="ftpUserID">FTP登录帐号</param>
        /// <param name="ftpPassword">FTP登录密码</param>
        /// <param name="filename">上文件文件名（绝对路径）</param>
        public  static void FTPUploadFile(string ftpServerIP, string ftpUserID, string ftpPassword, string filename)
        {
            //上传文件
            FileInfo uploadFile = null;
            //上传文件流
            FileStream uploadFileStream = null;
            //FTP请求对象
            FtpWebRequest ftpRequest = null;
            //FTP流
            Stream ftpStream = null;
            try
            {
                //获取上传文件
                uploadFile = new FileInfo(filename);
                //创建FtpWebRequest对象 
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + uploadFile.Name));
                //FTP登录
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                // 默认为true，连接不会被关闭 
                // 在一个命令之后被执行 
                ftpRequest.KeepAlive = false;
                //FTP请求执行方法
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                // 指定数据传输类型 
                ftpRequest.UseBinary = true;
                // 上传文件时通知服务器文件的大小 
                ftpRequest.ContentLength = uploadFile.Length;
                // 缓冲大小设置为2kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                // 打开一个文件流读上传的文件 
                uploadFileStream = uploadFile.OpenRead();
                // 把上传的文件写入流 
                ftpStream = ftpRequest.GetRequestStream();
                // 每次读文件流的2kb 
                contentLen = uploadFileStream.Read(buff, 0, buffLength);
                // 流内容没有结束 
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream 
                    ftpStream.Write(buff, 0, contentLen);
                    contentLen = uploadFileStream.Read(buff, 0, buffLength);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (uploadFileStream != null)
                {
                    uploadFileStream.Close();
                }

                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
            }
        }

        #endregion


    }
}
