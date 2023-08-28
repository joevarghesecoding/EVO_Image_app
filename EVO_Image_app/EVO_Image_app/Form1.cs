using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVO_Image_app.EVO_BACK_END;

namespace EVO_Image_app
{
    public partial class EVO_Image_App : Form
    {
        int side = 0;
        string today;
        string outDirPath;
        ListViewItem currentItem;
        ProgramDetails programDetails;
        string currentDirectory = Environment.CurrentDirectory;
        public EVO_Image_App()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            today = Common.GetDate();
            this.Text = "EVO Image App";
            //outDirPath = "C:\\Users\\Administrator\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today + "\\";
            outDirPath = currentDirectory + "\\Resources\\AllLatestModels\\" + today + "\\";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllLatestModels.GetLatestImages();

            //ListView controls
            listView1.Columns.Add("Current Programs", -2);
            List<ListViewItem> listViewItems = new List<ListViewItem>();

            foreach(ProgramObjs objs in AllLatestModels.programObjs)
            {
                ListViewItem temp = new ListViewItem(objs.GetModelAndColor());
                listViewItems.Add(temp);
            }

            listView1.Items.AddRange(listViewItems.ToArray());

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string today = Common.GetDate();
            ListView.SelectedListViewItemCollection program = this.listView1.SelectedItems;
            //string outDirPath = "C:\\Users\\Joe.Varghese\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today + "\\";
            
            foreach (ListViewItem item in program)
            {
                try
                {
                    programDetails = AllLatestModels.GetProgramDetails(outDirPath + item.Text);
                  
                    pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);

                    side = 0;
                    Front front = (Front)programDetails.sides[side];
                    front.ReadFile();

                

                    currentItem = item;
                } 
                catch(NullReferenceException ex)
                {
                    MessageBox.Show("Program contents not found.");
                    Console.WriteLine(ex);
                }
                

            }
            //List<ProgramDetails> programDetails = AllLatestModels.GetProgramDetails();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ProgramDetails programDetails = AllLatestModels.GetProgramDetails(outDirPath + currentItem.Text);

            if (side > 0)
            {
                side--;
            }

            if (System.IO.File.Exists(programDetails.sides[side].Image))
            {
                // Load and display the image
                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);

            }
            else
            {
                MessageBox.Show("The specified image file does not exist.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ProgramDetails programDetails = AllLatestModels.GetProgramDetails(outDirPath + currentItem.Text);

            if (side < 6)
            {
                side++;
            }
            if(side == 5)
            {
                side = 0;
            }

            if (System.IO.File.Exists(programDetails.sides[side].Image))
            {
                // Load and display the image
                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);

            }
            else
            {
                MessageBox.Show("The specified image file does not exist.");
            }
        }
    }
}
