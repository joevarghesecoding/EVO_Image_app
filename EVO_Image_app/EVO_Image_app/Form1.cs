using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        ProgramDetails programDetails;
        string currentDirectory = Environment.CurrentDirectory;
        Functions function;
        string date;
        int? modelIndex;
        int? typeIndex;
        int flag = 0;
        List<List<ListViewItem>> allLists;
        public EVO_Image_App()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            today = Common.GetDate();
            //outDirPath = "C:\\Users\\Administrator\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today + "\\";
            
            dataGridView1.Columns.Add("Regions", "Region");
            dataGridView1.Columns.Add("LargestDefect", "Largest Defect");
            dataGridView1.Columns.Add("DefectCount", "Defect Count");
            previousBtn.BackgroundImageLayout = ImageLayout.Center;
            findBtn.Enabled = false;
            modelDropDown.Enabled = false;
            typeDropDown.Enabled = false;
            calendarBtn.Enabled = false;
            searchBtn.Enabled = false;
            //originalList = new List<ListViewItem>();
            //allLists = new List<List<ListViewItem>>();
            textBox1.Enabled = false;
            calendar.Visible = false;
            calendar.MaxDate = DateTime.Today;
            ComptiaBox.ReadOnly = true;
            regionsBox.ReadOnly = true;
            this.Text = "EVO Image Viewer Tool v1.4";
            modelsAndColorsBtn.Enabled = false;
            modelSearch.Enabled = false;
            manualSearchBtn.Enabled = false;
        }

        //Password Protection
        

        //All Models And Colors button
        private void button1_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            flag = 1;
            Common.DeleteOnStart(currentDirectory, flag);
            function = new AllLatestModels();
            function.GetLatestImages();
            originalList = new List<ListViewItem>();
            allLists = new List<List<ListViewItem>>();
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
                if (!item.ToString().Contains('='))
                {
                    try
                    {
                        programDetails = new ProgramDetails();
                        programDetails.GetProgramDetails(outDirPath + item.Text);

                        if (flag == 1)
                        {
                            foreach (ProgramObjs o in objs)
                            {
                                if (o.GetModelAndColor() == item.Text)
                                {
                                    programDetails.ProgramObject = o;
                                    break;
                                }
                            }
                        }
                        else if (flag == 2)
                        {
                           
                            foreach (ProgramObjs o in objs)
                            {
                                if (o.GetSerialNum() == item.Text)
                                {
                                    outDirPath = currentDirectory + "\\Resources\\" + Common.CurrentFlag(flag) + "\\" + o.GetLastDate() + "\\";
                                    programDetails.GetProgramDetails(outDirPath + o.GetSerialNum());
                                    programDetails.ProgramObject = o;
                                    // Console.WriteLine(programDetails.ProgramObject.GetSerialNum() + " " + programDetails.ProgramObject.GetLastTime());
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
                                    outDirPath = currentDirectory + "\\Resources\\" + Common.CurrentFlag(flag) + "\\" + o.GetLastDate() + "\\";
                                    programDetails.GetProgramDetails(outDirPath + o.GetSerialNum() +","+ o.GetModelAndColor());
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
            allLists = new List<List<ListViewItem>>();
            listView1.Columns.Add("Current Programs", -2);
            listView1.Items.Clear();
        }

        //Find button next to manual search
        private void findBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string serial = textBox1.Text.ToUpper();
            if(serial.Length > 12)
            {
                string[] split = serial.Split(';');
                serial = split[0];

            }
            string pattern = "[A-Z0-9]";

            originalList.Clear();
            if(Regex.IsMatch(serial, pattern))
            {
                function.GetImagesForSerial(serial);
                List<ProgramObjs> objs = function.GetProgramObjs();
                List<string> dateList = new List<string>();
                string outDirPath = Common.currentDirectory + "\\Resources\\ManualSearch\\";
                DirectoryInfo dateFolders = new DirectoryInfo(outDirPath);
                DirectoryInfo[] eachDate = dateFolders.GetDirectories();
                foreach(DirectoryInfo each in eachDate)
                {
                    DirectoryInfo[] units = each.GetDirectories();
                    //Array.Sort(units, (a, b) => a.CreationTime.CompareTo(b.CreationTime));
                    foreach (DirectoryInfo unit in units)
                    {
                        foreach(ProgramObjs obj in objs)
                        {
                            if (unit.Name.Contains(obj.GetSerialNum()))
                            {
                                ListViewItem dateDivider = new ListViewItem("======" + obj.GetLastDate() + "======");
                                ListViewItem item = new ListViewItem(unit.Name);

                                if (!dateList.Contains(obj.GetLastDate()))
                                {
                                    dateList.Add(obj.GetLastDate());
                                    originalList.Add(dateDivider);
                                    originalList.Add(item);
                                }
                                else
                                {
                                    originalList.Add(item);
                                }
                            }
                        }
                        
                    }
                }                
            }
            else
            {
                MessageBox.Show("Program Not Found");
            }
            listView1.Items.Clear();
            listView1.Items.AddRange(originalList.ToArray());
            Cursor.Current = Cursors.AppStarting;
        }

        private void modelSearch_Click(object sender, EventArgs e)
        {
            flag = 3;
            listView1.Columns.Add("Current Programs", -2);
            listView1.Items.Clear();
            Common.DeleteOnStart(currentDirectory, flag);
            originalList = new List<ListViewItem>();
            allLists = new List<List<ListViewItem>>();
            function = new ModelSearch();
            modelDropDown.Enabled = true;
            typeDropDown.Enabled = true;
            calendarBtn.Enabled = true;
            searchBtn.Enabled = true;
            List<string> models = getModelSearchModels();
            List<string> types = getModelSearchTypes();
            modelDropDown.Items.AddRange(models.ToArray());
            typeDropDown.Items.AddRange(types.ToArray());

        }

        private List<string> getModelSearchModels()
        {
            List<ProgramObjs> modelSearch = function.GetProgramObjs();
            string fullPathModels = Common.currentDirectory + "//Resources//models_type.txt";
            List<string> models = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(fullPathModels))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        models.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("***** ERROR At getModelSearchModels() ******\n" + ex.Message);
            }
            
            foreach (ProgramObjs obj in modelSearch)
            {
                models.Add(obj.GetModelAndColor());
                
            }

            return models;
        }

        private List<string> getModelSearchTypes()
        {
            string fullPathAsIs = Common.currentDirectory + "//Resources//result_types_as_is.txt";
            string fullPathMSS = Common.currentDirectory + "//Resources//result_types_mss.txt";

            List<string> types = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(fullPathAsIs))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        types.Add(line);
                    }
                }
                using (StreamReader reader = new StreamReader(fullPathMSS))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        types.Add(line);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("***** ERROR At getModelSearchTypes() ******\n" + ex.Message);
            }
            return types;
        }

        private void modelDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
             modelIndex = modelDropDown.SelectedIndex;
        }

        private void typeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeIndex = typeDropDown.SelectedIndex;
        }

        private void calendarBtn_Click(object sender, EventArgs e)
        {
            calendar.BringToFront();
            if (calendar.Visible)
                calendar.Visible = false;
            else
            {
                calendar.BringToFront();
                calendar.Visible = true;
            }
                

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            
            SelectionRange selectedRange = calendar.SelectionRange;

            DateTime selectedDate = selectedRange.Start;
            date = Common.FormatDate(selectedDate);

        }

        //Model search button
        private void searchBtn_Click(object sender, EventArgs e)
        {
            ModelSearch ms = new ModelSearch();
            int type = 0;
            originalList = new List<ListViewItem>();
            if (calendar.Visible)
                calendar.Visible = false;

            if (typeIndex == null)
                typeIndex = 0;
            type = typeIndex ?? default(int);
            Console.WriteLine(type);
            if (modelIndex.HasValue && typeIndex != null)
            {
                int index = modelIndex.Value;
                string[] models = getModelSearchModels().ToArray();
                
                if(models[index] != "ALL")
                {
                    ProgramObjs program = new ProgramObjs(models[index]);
                    if (date != null)
                    {
                        function.GetModelImages(program, date, type);
                    }
                    else
                    {
                        function.GetModelImages(program, today, type);
                    }

                }
                else
                {
                    if (date != null)
                    {
                        function.GetAllModelImages(date);
                    }
                    else
                    {
                        function.GetAllModelImages(today);
                    }
                }
               

                List<ProgramObjs> modelSearch = function.GetFoundPrograms();
                
                List<string> dateList = new List<string>();
                string typeDir = "";
                string outDirPath = Common.currentDirectory + "\\Resources\\ModelSearch\\";
               
                DirectoryInfo dateFolders = new DirectoryInfo(outDirPath);
                DirectoryInfo[] eachDate = dateFolders.GetDirectories();

                if (models[index] != "ALL")
                {
                    foreach (DirectoryInfo each in eachDate)
                    {
                        DirectoryInfo[] eachModels = each.GetDirectories();

                        foreach (DirectoryInfo eachModel in eachModels)
                        {
                            DirectoryInfo[] eachResults = eachModel.GetDirectories();
                            if (type == 0)
                            {
                                foreach (DirectoryInfo eachResult in eachResults)
                                {
                                    DirectoryInfo[] eachSerials = eachResult.GetDirectories();
                                    foreach (DirectoryInfo eachSerial in eachSerials)
                                    {
                                        foreach (ProgramObjs obj in modelSearch)
                                        {
                                            if (eachSerial.Name.Contains(obj.GetSerialNum()))
                                            {
                                                ListViewItem dateDivider = new ListViewItem("==" + obj.GetLastDate() + " " + obj.GetModelAndColor() + " " + ms.getType(type) + "==");
                                                dateDivider.BackColor = Color.Black;
                                                dateDivider.ForeColor = Color.Aqua;
                                                ListViewItem item = new ListViewItem(eachSerial.Name);

                                                if (!dateList.Contains(obj.GetLastDate() + " " + obj.GetModelAndColor()))
                                                {
                                                    dateList.Add(obj.GetLastDate() + " " + obj.GetModelAndColor());
                                                    originalList.Add(dateDivider);
                                                    originalList.Add(item);
                                                }
                                                else
                                                {
                                                    originalList.Add(item);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                foreach (DirectoryInfo eachResult in eachResults)
                                {
                                    string t = ms.getType(type);
                                    if (eachResult.Name.Contains(t))
                                    {
                                        foreach (ProgramObjs obj in modelSearch)
                                        {
                                            DirectoryInfo[] eachSerials = eachResult.GetDirectories();
                                            foreach (DirectoryInfo eachSerial in eachSerials)
                                            {
                                                if (eachSerial.Name.Contains(obj.GetSerialNum()))
                                                {
                                                    ListViewItem dateDivider = new ListViewItem("==" + obj.GetLastDate() + " " + obj.GetModelAndColor() + " " + ms.getType(type) + "==");
                                                    dateDivider.BackColor = Color.Black;
                                                    dateDivider.ForeColor = Color.Aqua;
                                                    ListViewItem item = new ListViewItem(eachSerial.Name);

                                                    if (!dateList.Contains(obj.GetLastDate() + " " + obj.GetModelAndColor()))
                                                    {
                                                        dateList.Add(obj.GetLastDate() + " " + obj.GetModelAndColor());
                                                        originalList.Add(dateDivider);
                                                        originalList.Add(item);
                                                    }
                                                    else
                                                    {
                                                        originalList.Add(item);
                                                    }
                                                }
                                            }
                                        }
                                        if (type != 3)
                                            break;
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    foreach (DirectoryInfo each in eachDate)
                    {
                        DirectoryInfo[] eachResults = each.GetDirectories();
                        foreach (DirectoryInfo eachResult in eachResults)
                        {
                            DirectoryInfo[] eachSerials = eachResult.GetDirectories();
                            foreach (DirectoryInfo eachSerial in eachSerials)
                            {
                                foreach (ProgramObjs obj in modelSearch)
                                {
                                    if (eachSerial.Name.Contains(obj.GetSerialNum()))
                                    {
                                        ListViewItem dateDivider = new ListViewItem("==" + obj.GetLastDate() + " " + obj.GetModelAndColor() + " " + ms.getType(type) + "==");
                                        dateDivider.BackColor = Color.Black;
                                        dateDivider.ForeColor = Color.Aqua;
                                        ListViewItem item = new ListViewItem(eachSerial.Name);

                                        if (!dateList.Contains(obj.GetLastDate() + " " + obj.GetModelAndColor()))
                                        {
                                            dateList.Add(obj.GetLastDate() + " " + obj.GetModelAndColor());
                                            originalList.Add(dateDivider);
                                            originalList.Add(item);
                                        }
                                        else
                                        {
                                            originalList.Add(item);
                                        }
                                    }
                                }
                            }

                        }
                    }

                    allLists.Add(originalList);
                    //originalList.Clear();
                    textBox1.Text = "Filter";
                    textBox1.Enabled = true;
                    textBox1.ForeColor = System.Drawing.Color.DimGray;
                    listView1.Items.Clear();
                    foreach (List<ListViewItem> list in allLists)
                    {
                        listView1.Items.AddRange(list.ToArray());
                    }
                    //listView1.Items.AddRange(allLists.ToArray());

                }
            }
           
        }

        //Password Code
        private void passwordButton_Click(object sender, EventArgs e)
        {
            if(passwordTextBox.Text.ToString().ToLower().Equals("ctdievo"))
            {
                modelsAndColorsBtn.Enabled = true;
                modelSearch.Enabled = true;
                manualSearchBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Incorrect Password");
            }
        }

        private void passwordEnter_Click(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (passwordTextBox.Text.ToString().ToLower().Equals("ctdievo"))
                {
                    modelsAndColorsBtn.Enabled = true;
                    modelSearch.Enabled = true;
                    manualSearchBtn.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }
            }
        }

        private void textbox1Enter_Click(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if(flag == 2)
                {
                    findBtn_Click(sender, e);
                }
            }
        }

        
    }
}
