using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVO_Image_app.EVO_BACK_END;
using EVO_Image_app.EVO_BACK_END.Functionality;

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
        Functions function;
        string date;
        int? modelIndex;
        int flag = 0;
        public EVO_Image_App()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            today = Common.GetDate();
            this.Text = "EVO Image App";
            //outDirPath = "C:\\Users\\Administrator\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today + "\\";
            
            dataGridView1.Columns.Add("Regions", "Region");
            dataGridView1.Columns.Add("LargestDefect", "Largest Defect");
            dataGridView1.Columns.Add("DefectCount", "Defect Count");
            previousBtn.BackgroundImageLayout = ImageLayout.Center;
            findBtn.Enabled = false;
            modelDropDown.Enabled = false;
            calendarBtn.Enabled = false;
            searchBtn.Enabled = false;
            originalList = new List<ListViewItem>();
            textBox1.Enabled = false;
            calendar.Visible = false;
            calendar.MaxDate = DateTime.Today;
            ComptiaBox.ReadOnly = true;
            regionsBox.ReadOnly = true;
        }

        //All Models And Colors button
        private void button1_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            flag = 1;
            Common.DeleteOnStart(currentDirectory, flag);
            function = new AllLatestModels();
            function.GetLatestImages();
            originalList = new List<ListViewItem>();
            textBox1.Enabled = true;
            //ListView controls
            listView1.Columns.Add("Current Programs", -2);
            List<ProgramObjs> allLatestModelsObjs = function.GetProgramObjs();
            foreach (ProgramObjs objs in allLatestModelsObjs)
            {
                ListViewItem temp = new ListViewItem(objs.GetModelAndColor());
                originalList.Add(temp);
            }

            textBox1.Text = "Filter";
            
            textBox1.ForeColor = System.Drawing.Color.DimGray;
            listView1.Items.AddRange(originalList.ToArray());
            Cursor.Current = Cursors.AppStarting;
        }

        //List view containing programs
        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComptiaBox.Text = "";
            regionsBox.Text = "";
            //string today = Common.GetDate();
            ListView.SelectedListViewItemCollection program = this.listView1.SelectedItems;
            //string outDirPath = "C:\\Users\\Joe.Varghese\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today + "\\";
            outDirPath = currentDirectory + "\\Resources\\" + Common.CurrentFlag(flag) + "\\" + today + "\\";
            List<ProgramObjs> objs = function.GetProgramObjs();
          
            foreach (ListViewItem item in program)
            {
                try
                {
                    //Console.WriteLine(outDirPath + item.Text);
                    programDetails = Common.GetProgramDetails(outDirPath + item.Text);
                    
                    if(flag == 1)
                    {
                        foreach (ProgramObjs o in objs)
                        {
                           if(o.GetModelAndColor() == item.Text)
                            {
                                programDetails.ProgramObject = o;
                                break;
                            }
                        }
                    }
                    else if(flag == 2)
                    {
                        
                        foreach (ProgramObjs o in objs)
                        {
                            programDetails = Common.GetProgramDetails(outDirPath + o.GetSerialNum() + " " + o.GetLastDate());
                            if (o.GetSerialNum() + " " + o.GetLastDate() == item.Text)
                            {
                                programDetails.ProgramObject = o;
                                break;
                            }
                        }
                    }
                    else if (flag == 3)
                    {
                        string[] text = item.Text.Split(',');
                        objs = function.GetFoundPrograms();
                        foreach (ProgramObjs o in objs)
                        {
                            if (o.GetSerialNum() == text[0])
                            {
                                programDetails.ProgramObject = o;
                                break;
                            }
                        }
                    }
                    
                    pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);

                    side = 0;

                    Common.DisplayData(programDetails.sides[side], dataGridView1);
                    Common.DisplaySerialAndDate(objs, programDetails.ProgramObject, serialNum, lastDate, ComptiaBox, regionsBox);
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Program contents not found.");
                    Console.WriteLine(ex);
                }


            }
        }

        //Previous button
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

        //Next Button
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

        //Search/Filter Box
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        //Search/Filter box
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            if(flag == 1 || flag == 3)
            {
                ClearFilter(keyword);
            }
            

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

        //Manual Search button
        private void manualSearchBtn_Click(object sender, EventArgs e)
        {
            flag = 2;
            Common.DeleteOnStart(currentDirectory, flag);
            function = new ManualSearch();
            textBox1.Text = "Enter Serial and click Search";
            textBox1.ForeColor = System.Drawing.Color.DimGray;
            textBox1.Enabled = true;
            findBtn.Enabled = true;
            originalList = new List<ListViewItem>();
            listView1.Columns.Add("Current Programs", -2);
            listView1.Items.Clear();
        }

        //Find button next to manual search
        private void findBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string serial = textBox1.Text.ToUpper();
            string pattern = "[A-Z0-9]";


            if(Regex.IsMatch(serial, pattern))
            {
                function.GetImagesForSerial(serial);
                List<ProgramObjs> programs = function.GetProgramObjs();
                foreach (ProgramObjs program in programs)
                {
                    //Console.WriteLine(program.GetSerialNum() + " " + program.GetLastDate());
                    ListViewItem temp = new ListViewItem(program.GetSerialNum() + " " + program.GetLastDate());
                    listView1.Items.Add(temp);
                }
               
            }
            else
            {
                MessageBox.Show("Program Not Found");
            }

            Cursor.Current = Cursors.AppStarting;
        }

        private void modelSearch_Click(object sender, EventArgs e)
        {
            flag = 3;
            listView1.Columns.Add("Current Programs", -2);
            listView1.Items.Clear();
            Common.DeleteOnStart(currentDirectory, flag);
            function = new ModelSearch();
            modelDropDown.Enabled = true;
            calendarBtn.Enabled = true;
            searchBtn.Enabled = true;
            string[] models = getModelSearchModels();
            modelDropDown.Items.AddRange(models);
        }

        private string[] getModelSearchModels()
        {
            List<ProgramObjs> modelSearch = function.GetProgramObjs();
            string[] models = new string[modelSearch.Count];
            int i = 0;
            foreach (ProgramObjs obj in modelSearch)
            {
                models[i] = obj.GetModelAndColor();
                i++;
            }

            return models;
        }

        private void modelDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
             modelIndex = modelDropDown.SelectedIndex;
        }

        private void calendarBtn_Click(object sender, EventArgs e)
        {
            if (calendar.Visible)
                calendar.Visible = false;
            else
                calendar.Visible = true;

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
            SelectionRange selectedRange = calendar.SelectionRange;

            DateTime selectedDate = selectedRange.Start;
            date = Common.FormatDate(selectedDate);

        }


        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (calendar.Visible)
                calendar.Visible = false;
            outDirPath = currentDirectory + "\\Resources\\" + Common.CurrentFlag(flag) + "\\" + today + "\\";
            if (modelIndex.HasValue)
            {
                int index = modelIndex.Value;
                string[] models = getModelSearchModels();
                
                ProgramObjs program = new ProgramObjs(models[index]);
                if (date != null)
                    function.GetModelImages(program, date);
                else
                    function.GetModelImages(program, today);
                List<ProgramObjs> modelSearch = function.GetFoundPrograms();

                originalList = new List<ListViewItem>();
                foreach (ProgramObjs objs in modelSearch)
                {
                    //Console.WriteLine(objs.GetSerialNum());
                    ListViewItem temp = new ListViewItem(objs.GetSerialNum() + "," + objs.GetModelAndColor());
                    originalList.Add(temp);
                }

                textBox1.Text = "Filter";
                textBox1.Enabled = true;
                textBox1.ForeColor = System.Drawing.Color.DimGray;
                listView1.Items.Clear();
                listView1.Items.AddRange(originalList.ToArray());

  
            }
           
        }

       
    }
}
