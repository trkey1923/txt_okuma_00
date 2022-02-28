using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace txt_okuma_00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int Sayac=0;
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Adı", 150);
            listView1.Columns.Add("Yolu", 300);
            listView1.Columns.Add("Boyut", 50);
            listView1.Columns.Add("İçerik", 1200);

        }

        private void btnYol_Click(object sender, EventArgs e)

        {
            Sayac = 0;
            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
            ListViewDoldur(textBox1.Text);
        }

        private void ListViewDoldur(String yol)
        {
            listView1.Items.Clear();
            Directory.CreateDirectory(yol);
            string[] yoldakiDosyalar = Directory.GetFiles(yol, "*.txt");
            if (yoldakiDosyalar.Length < 100)
            {
                label1.Text = "Dosya Sayısı: " + yoldakiDosyalar.Length.ToString();
                Sayac = yoldakiDosyalar.Length;
            }
            else
            {
                label1.Text = "Dosya Sayısı 0-70 / " + yoldakiDosyalar.Length.ToString();
                Sayac=70;
                label1.ForeColor = Color.Red;
            }
            for (int i = 0; i < Sayac; i++)
            {
                string[] DosyaBilgileri = DosyaBilgileriniAl(yoldakiDosyalar[i]);
                var satır = new ListViewItem(DosyaBilgileri);
                listView1.Items.Add(satır);
            }
        }

        private String[] DosyaBilgileriniAl(string v)
        {
            String[] DosyaBilgileri = new String[4];
            string yol = v;
            FileInfo fi = new FileInfo(yol);
            long dosyaBoyutu = fi.Length;
            String dosyaAdı = fi.Name;
            String icerik = DosyaOku(v);
            DosyaBilgileri[0] = dosyaAdı;
            DosyaBilgileri[1] = yol;
            DosyaBilgileri[2] = dosyaBoyutu.ToString();
            DosyaBilgileri[3] = icerik;

            return DosyaBilgileri;
        }

        private string DosyaOku(string v)
        {
            StreamReader streamReader = new StreamReader(v);
            char[] karakterler = new char[500];
            streamReader.ReadBlock(karakterler, 0, 500);
            streamReader.Close();
            String Metin = new String(karakterler);
            return Metin;
        }

        private void btnHepsiSec_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem satır in listView1.Items)
            {
                satır.Checked = true;
            }
        }

        private void btnDigerSec_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem satır in listView1.Items)
            {
                if (satır.Checked == true)
                {
                    satır.Checked = false;
                }
                else
                    satır.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
                    foreach (ListViewItem satır in listView1.Items)
                {
                    if (satır.Checked==true)
                    {
                        //MessageBox.Show(satır.SubItems[1].Text);
                        string yol=satır.SubItems[1].Text;
                        File.Delete(yol);

                    }
                }
            
            ListViewDoldur(textBox1.Text);
        }
    }
}
