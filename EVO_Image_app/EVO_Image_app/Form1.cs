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
        List<ListViewItem> originalList;
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
            dataGridView1.Columns.Add("Regions", "Region");
            dataGridView1.Columns.Add("LargestDefect", "Largest Defect");
            dataGridView1.Columns.Add("DefectCount", "Defect Count");
            Common.DeleteOnStart(currentDirectory);
            previousBtn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            AllLatestModels.GetLatestImages();

            //ListView controls
            listView1.Columns.Add("Current Programs", -2);
            originalList = new List<ListViewItem>();

            foreach (ProgramObjs objs in AllLatestModels.programObjs)
            {
                ListViewItem temp = new ListViewItem(objs.GetModelAndColor());
                originalList.Add(temp);
            }

            textBox1.Text = "Filter";
            textBox1.ForeColor = System.Drawing.Color.DimGray;
            listView1.Items.AddRange(originalList.ToArray());
            Cursor.Current = Cursors.AppStarting;
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
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

                    Common.DisplayData(programDetails.sides[side], dataGridView1);
                    Common.DisplaySerialAndDate(programDetails.ProgramObject, serialNum, lastDate);
                    
                    currentItem = item;
                    
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Program contents not found.");
                    Console.WriteLine(ex);
                }


            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            if (side > 0)
            {
                side--;
            }

            if (System.IO.File.Exists(programDetails.sides[side].Image))
            {
                // Load and display the image
                pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);
                Common.DisplayData( programDetails.sides[side], dataGridView1);
            }
            else
            {
                MessageBox.Show("The specified image file does not exist.");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        { 
            if (side < 6)
            {
                side++;
            }
            if (side == 6)
            {
                side = 0;
            }

            if (System.IO.File.Exists(programDetails.sides[side].Image))
            {
                // Load and display the image
                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);
                Common.DisplayData(programDetails.sides[side], dataGridView1);
            }
            else
            {
                MessageBox.Show("The specified image file does not exist.");
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            ClearFilter(keyword);

        }

        private void ClearFilter(string keyword)
        {
            listView1.Items.Clear();
            foreach(ListViewItem item in originalList)
            {
                if (item.Text.Contains(keyword))
                {
                    listView1.Items.Add(item);
                }
            }
        }
 


        private void nextButton_MouseEnter(object sender, EventArgs e)
        {
            nextButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            nextButton.BackColor = Color.DarkGray;

            previousBtn.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            previousBtn.BackColor = Color.DarkGray;

        }

        private void nextButton_MouseLeave(object sender, EventArgs e)
        {
            nextButton.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            nextButton.BackColor = Color.LightGray;

            previousBtn.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            previousBtn.BackColor = Color.LightGray;
        }
    }
}
