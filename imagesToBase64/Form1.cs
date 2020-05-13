using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace imagesToBase64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            // 创建文件选择控件对象
            OpenFileDialog dialog = new OpenFileDialog();
            // 禁止多选
            dialog.Multiselect = false;
            dialog.Title = "请选择要转换成base64的图片";

            dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt1.Text = dialog.FileName;     //显示文件路径
                FileInfo myFile = new FileInfo(dialog.FileName);
                string fileExtension = Path.GetExtension(myFile.FullName);//即扩回展名答

                switch (fileExtension)
                {
                    case ".png":
                        txt2.Text = "data:image/png;base64," + ImgToBase64(dialog.FileName);
                        break;
                    case ".ico":
                        txt2.Text = "data:image/x-icon;base64," + ImgToBase64(dialog.FileName);
                        break;
                    case ".gif":
                        txt2.Text = "data:image/gif;base64," + ImgToBase64(dialog.FileName);
                        break;
                    default:
                        txt2.Text = "data:image/jpeg;base64," + ImgToBase64(dialog.FileName);
                        break;
                }
                button1.Text = "点击复制";
                button1.Enabled = true;
            }
            else
            {
                txt1.Text = "";
                txt2.Text = "";
                button1.Text = "";
                button1.Enabled = false;
            }

        }

        public string ImgToBase64(string ImageFileName)
        {
            try
            {
                Bitmap bmp = new Bitmap(ImageFileName);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Clipboard.SetDataObject(txt2.Text);
        }
    }
}
