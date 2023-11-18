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
        int type = 0;
        List<List<ListViewItem>> allLists;
        List<string> dateList;
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
                        

                        if (flag == 1)
                        {
                            foreach (ProgramObjs o in objs)
                            {
                                if (o.GetModelAndColor() == item.Text)
                                {
                                    programDetails.GetProgramDetails(outDirPath + item.Text);
                                    programDetails.ProgramObject = o;
                                    Common.DisplayData(programDetails.sides[side], dataGridView1);
                                    Common.DisplaySerialAndDate(objs, programDetails.ProgramObject, serialNum, lastDate, ComptiaBox, regionsBox);
                                    break;
                                }
                            }
                        }
                        else if (flag == 2)
                        {
                            Dictionary<string, List<ProgramObjs>> programs = function.GetManualSearchProgams();
                            foreach(var kvp in programs)
                            {
                                objs = kvp.Value;
                                foreach (ProgramObjs o in objs)
                                {
                                    if (o.GetSerialNum() + " " + o.GetLastTime().Replace(':', '-') == item.Text)
                                    {
                                        programDetails.GetProgramDetails(o.GetOutputDirectoryPath() + "\\" + o.GetSerialNum() + " " + o.GetLastTime().Replace(':', '-'));
                                        programDetails.ProgramObject = o;
                                        Common.DisplayData(programDetails.sides[side], dataGridView1);
                                        Common.DisplaySerialAndDate(objs, programDetails.ProgramObject, serialNum, lastDate, ComptiaBox, regionsBox);
                                        if (o.GetResult().Contains("PASS")){
                                            ComptiaBox.Text = o.GetResult();
                                            regionsBox.Text = "N/A";
                                        } else
                                        {
                                            ComptiaBox.Text = o.GetResult();
                                            regionsBox.Text = o.GetComptia();
                                        }
                                        pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);
                                        break;
                                    }
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
                                    programDetails.GetProgramDetails(o.GetOutputDirectoryPath() + "\\" + o.GetSerialNum() + "," + o.GetModelAndColor() + "," + o.GetResult());
                                    programDetails.ProgramObject = o;
                                    Common.DisplayData(programDetails.sides[side], dataGridView1);
                                    Common.DisplaySerialAndDate(objs, programDetails.ProgramObject, serialNum, lastDate, ComptiaBox, regionsBox);
                                    break;
                                }
                            }
                        }

                        pictureBox1.Image = Image.FromFile(programDetails.sides[side].Image);

                        side = 0;


                       

                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Program contents not found.");
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        private void ModelSearchIndexSelected(ProgramObjs obj)
        {
            if (typeIndex == null)
                typeIndex = 0;
            int type = typeIndex ?? default(int);

          
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
            originalList.Clear();
            Cursor.Current = Cursors.WaitCursor;
            string serial = textBox1.Text.ToUpper();
            if(serial.Length > 12)
            {
                string[] split = serial.Split(';');
                serial = split[0];

            }
            string pattern = "[A-Z0-9]";
            dateList = new List<string>();
            if (Regex.IsMatch(serial, pattern))
            {
                function.GetImagesForSerial(serial);
                Dictionary<string, List<ProgramObjs>> programs = function.GetManualSearchProgams();
                
                foreach(var kvp in programs)
                {
                    ListViewItem dateDivider = new ListViewItem("======" + kvp.Key + "======");
                    dateDivider.BackColor = Color.Black;
                    dateDivider.ForeColor = Color.Aqua;
                    originalList.Add(dateDivider);
                     List<ProgramObjs> objs = kvp.Value;
                    foreach(ProgramObjs obj in objs)
                    {
                        ListViewItem item = new ListViewItem(obj.GetSerialNum() + " " + obj.GetLastTime().Replace(':', '-'));
                        originalList.Add(item);
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
            dateList = new List<string>();
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
            originalList = new List<ListViewItem>();
            if (calendar.Visible)
                calendar.Visible = false;

            if (typeIndex == null)
                typeIndex = 0;
            type = typeIndex ?? default(int);
            //Console.WriteLine(type);
            if (modelIndex.HasValue && typeIndex != null)
            {
                int index = modelIndex.Value;
                string[] models = getModelSearchModels().ToArray();

                if(models[index] == "ALL" && type == 0)
                {
                    MessageBox.Show("Find the results in the Daily Run Data");
                    return;
                }
                //Model Search
                if (models[index] != "ALL")
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

                    List<ProgramObjs> modelSearch = function.GetFoundPrograms();
                   
                    Dictionary<string, List<ProgramObjs>> reorganizedModelSearch = ReorganizeFoundPrograms(modelSearch, type);
                    AddToAllList(reorganizedModelSearch);

                }
                else
                {

                    if (type != 1)
                    {
                        if (date != null)
                        {
                            function.GetAllModelImages(date, type);
                        }
                        else
                        {
                            function.GetAllModelImages(today, type);
                        }
                        List<ProgramObjs> modelSearch = function.GetFoundPrograms();
                        Dictionary<string, List<ProgramObjs>> reorganizedModelSearch = ReorganizeFoundPrograms(modelSearch, type);
                        AddToAllList(reorganizedModelSearch);
                    }
                    else if (type == 1)
                    {
                        if (date != null)
                        {
                            function.GetLintLogicImages(date);
                        }
                        else
                        {
                            function.GetLintLogicImages(today);
                        }

                        List<ProgramObjs> lintLogicCandidates = function.getLintLogicCandidates();
                        Dictionary<string, List<ProgramObjs>> reorganizedModelSearch = ReorganizeFoundPrograms(lintLogicCandidates, type);
                        AddToAllList(reorganizedModelSearch);
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
            }
        }

        private Dictionary<string, List<ProgramObjs>> ReorganizeFoundPrograms(List<ProgramObjs> programObjs, int type)
        {
            ModelSearch ms = new ModelSearch();   
            Dictionary<string, List<ProgramObjs>> hashMap = new Dictionary<string, List<ProgramObjs>>();
            //ALL
            if (type != 1)
            {
                foreach (ProgramObjs program in programObjs)
                {
                    if (!hashMap.ContainsKey(program.GetModelAndColor()))
                    {
                        List<ProgramObjs> temp = new List<ProgramObjs>();
                        temp.Add(program);
                        hashMap.Add(program.GetModelAndColor(), temp);
                    }
                    else
                    {
                        hashMap[program.GetModelAndColor()].Add(program);
                    }
                }
            }
            //Lint logic
            else if(type == 1)
            {
                foreach (ProgramObjs program in programObjs)
                {
                    if (!hashMap.ContainsKey(program.GetLintLogicResult()))
                    {
                        List<ProgramObjs> temp = new List<ProgramObjs>();
                        temp.Add(program);
                        hashMap.Add(program.GetLintLogicResult(), temp);
                    }
                    else
                    {
                        hashMap[program.GetLintLogicResult()].Add(program);
                    }
                }
            }
            //Individual comptias
            else
            {
                string t = ms.getType(type);
                foreach (ProgramObjs program in programObjs)
                {
                    if (!hashMap.ContainsKey(program.GetResult()) && program.GetResult().Contains(t))
                    {
                        List<ProgramObjs> temp = new List<ProgramObjs>();
                        temp.Add(program);
                        hashMap.Add(program.GetResult(), temp);
                    }
                    else
                    {
                        hashMap[program.GetResult()].Add(program);
                    }
                }
            }
           
            return hashMap;
        }

        private void AddToAllList(Dictionary<string, List<ProgramObjs>> hashMap)
        {
            //allLists = new List<List<ListViewItem>>();

            foreach (var kvp in hashMap)
            {
                ListViewItem dateDivider = new ListViewItem("==" + kvp.Key + "==");
                dateDivider.BackColor = Color.Black;
                dateDivider.ForeColor = Color.Aqua;
                List<ProgramObjs> temp = kvp.Value;
                List<ListViewItem> tempListView = new List<ListViewItem>();
                tempListView.Add(dateDivider);
                foreach(ProgramObjs programs in temp)
                {
                    //Console.WriteLine(programs.GetOutputDirectoryPath());
                    tempListView.Add(new ListViewItem(programs.GetSerialNum() + "," + programs.GetResult()));
                }

                allLists.Add(tempListView);
            }
        }
        
        //Password Code
        private void passwordButton_Click(object sender, EventArgs e)
        {
            //DEV
            modelsAndColorsBtn.Enabled = true;
            modelSearch.Enabled = true;
            manualSearchBtn.Enabled = true;

            //PROD
            //if (passwordTextBox.Text.ToString().ToLower().Equals("ctdievo"))
            //{
            //    modelsAndColorsBtn.Enabled = true;
            //    modelSearch.Enabled = true;
            //    manualSearchBtn.Enabled = true;
            //}
            //else
            //{
            //    MessageBox.Show("Incorrect Password");
            //}
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
