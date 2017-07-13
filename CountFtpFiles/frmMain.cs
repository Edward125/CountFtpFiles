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
        }



        private void loadUI()
        {
           // skinEngine1.SkinFile = p.AppFolder + @"\MacOS.ssk";
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
    }
}
