using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CountFtpFiles
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadUI();
            compareFile();
        }
      



        private void loadUI()
        {
           // skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
            this.Text = "Compare FTP files & DB Files,Ver:" + Application.ProductVersion + "(Edward_song@yeah.net)";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] f = FtpHelper.FTPGetFileList("10.62.201.224", "ate", "Wcdate", "ATE");
            foreach (string  item in f)
            {
                MessageBox.Show(item);
            }
        }

        private void txtNewProName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty (txtNewProName.Text.Trim()))
                btnAdd.Enabled= false;
            else 
                btnAdd.Enabled= true ;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewProName.Text.Trim()))
                return;

            string proname = txtNewProName.Text.Trim ();

            string sql = "INSERT INTO d_ateprolist  (proname) VALUES('" + @proname + "')";
            if (p.insertDB(sql))
            {
                MessageBox.Show("Write data to db successful...", "Write DB");
                getDBListFiles(lstProName);
            }
            else
                MessageBox.Show("Write data to db fail...", "Write DB");
       
         
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //string[] f = FtpHelper.FTPGetFileList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder);
            //foreach (string  item in f)
            //{
            //    MessageBox.Show(item);
               
            //}
            compareFile();
        }



        private void compareFile()
        {
            txtInfo.Text.Trim();
            //
            string[] ftpfiles = FtpHelper.FTPGetFileList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder);          
            //foreach (string files in ftpfiles)
            //{
            //    lstNotFind.Items.Add(files);
            //}
            List<string> dbfiles = new List<string> ();
            getDBListFiles(lstDBFileList,out  dbfiles);

            //string sql = "SELECT * FROM d_ateprolist";
            //int dbfilescount = -1;
            //List<string> dbfiles = p.queryDB(sql, out dbfilescount);
            //foreach (string item in dbfiles )
            //{
            //    lstDBFileList.Items.Add(item);
            //}
            int notfind = 0;
            lstNotFind.Items.Add("FTP存在但数据库不存在");
            foreach (string files in ftpfiles)
            {
                if (lstDBFileList.Items.IndexOf(files) != -1)
                {
                    //存在
                    dbfiles.Remove(files);
                }
                else
                {
                    //不存在
                    notfind++;
                    lstNotFind.Items.Add(files);
                }
            }
            lstNotFind.Items.Add(" ");
            lstNotFind.Items.Add("数据库存在但FTP不存在");
            if (dbfiles.Count > 0)
            {
                
               foreach (string files in dbfiles )
               {
                   lstNotFind.Items.Add(files);
               }
	
            }
            
            
                
            txtInfo.Text = "FTP存在文件:" + ftpfiles.Length + ",数据库中存在文件:" + lstDBFileList.Items.Count + ",FTP存在但数据库不存在的文件有:" + notfind + ",数据库存在但FTP不存在的文件有:" + dbfiles.Count;

        }



        private void getFtpListFiles()
        {
            lstFtpFilesList.Items.Clear();
            string[] ftpfiles = FtpHelper.FTPGetFileList(p.FtpIP, p.FtpID, p.FtpPassword, p.FtpFolder);
            foreach (string files in ftpfiles)
            {
                lstFtpFilesList.Items.Add(files);
            }
        }

        private void getDBListFiles(ListBox list)
        {
            list.Items.Clear();
            string sql = "SELECT * FROM d_ateprolist";
            int dbfilescount = -1;
            List<string> dbfiles = p.queryDB(sql, out dbfilescount);
            foreach (string item in dbfiles)
            {
                //lstDBFileList.Items.Add(item);
                list.Items.Add(item);
            }
        }

        private void getDBListFiles(ListBox list,out List<string> files)
        {
            list.Items.Clear();
            string sql = "SELECT * FROM d_ateprolist";
            int dbfilescount = -1;
            files = p.queryDB(sql, out dbfilescount);
            foreach (string item in files)
            {
                //lstDBFileList.Items.Add(item);
                list.Items.Add(item);
            }
           
        }


        private void lstNotFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lstNotFind.SelectedItem.ToString()))
                    Clipboard.SetText(lstNotFind.SelectedItem.ToString());
            }
            catch (Exception)
            {
                
   
            }
        
        }

        private void lstDBFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lstDBFileList.SelectedItem.ToString()))
                    Clipboard.SetText(lstDBFileList.SelectedItem.ToString());
            }
            catch (Exception)
            {
                

            }
        
        }

  

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabDB)
            {
                getFtpListFiles();
                getDBListFiles(lstProName);
            }
            if (e.TabPage == tabMain)
            {
                compareFile();
            }
        }

        private void lstFtpFilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lstFtpFilesList.SelectedItem.ToString()))
                    Clipboard.SetText(lstFtpFilesList.SelectedItem.ToString());
            }
            catch (Exception)
            {
                
        
            }
            
        }

        private void lstProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lstProName.SelectedItem.ToString()))
                    btnDelete.Enabled = true;
                else
                    btnDelete.Enabled = false;
            }
            catch (Exception)
            {                
         
            }
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string proname = @lstProName.SelectedItem.ToString();
            string sql = "DELETE FROM d_ateprolist WHERE proname = '" + proname + "'";
            if (p.deleteDB(sql))
            {
                MessageBox.Show("Delete data from db success...", "Delete Data");
                getDBListFiles(lstProName);
            }
            else
                MessageBox.Show("Delete data from db fail...", "Delete Data");

            
        }

        private void txtNewProName_DoubleClick(object sender, EventArgs e)
        {
            txtNewProName.SelectAll();
        }

        private void btnWriteAll_Click(object sender, EventArgs e)
        {
            if (lstFtpFilesList.Items.Count > 0)
            {
                this.Enabled = false;
                foreach (var item in lstFtpFilesList.Items)
                {
                    string proname = item.ToString().Trim();
                    string sql = "INSERT INTO d_ateprolist  (proname) VALUES('" + @proname + "')";
                    p.insertDB(sql);
                }
                MessageBox.Show("Auto write all ftp files name into db complete...", "Write All");
                getDBListFiles(lstProName);
                this.Enabled = true;
            }
            else
                return;
        }
 
    }
}
