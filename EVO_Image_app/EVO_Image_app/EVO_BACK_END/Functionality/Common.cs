using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using EVO_Image_app.EVO_BACK_END.Functionality;

namespace EVO_Image_app.EVO_BACK_END
{
    class Common
    {
        public static string currentDirectory = Environment.CurrentDirectory;

        ///<summary> 
        ///Get's today's date and formats it to M-D-YYYY
        ///</summary>
        ///<returns>(String) Today's date</returns>
        public static string GetDate()
        {
            DateTime today = DateTime.Today;
            string formattedDate = today.ToString("M-d-yyyy");
            return formattedDate;
        }

        ///<summary> 
        ///Formats the date from string
        ///</summary>
        ///<returns>Date in correct format</returns>
        public static string FormatDate(DateTime date)
        {
            string formattedDate = date.ToString("M-d-yyyy");
            return formattedDate;
        }

        ///<summary>
        ///Makes a folder if there isn't one, copies images from source to destination
        /// </summary>
        /// <param name="inputSerial">Serial #</param>
        /// <param name="inputPath">Path of the Serial # folder from Daily Run Data</param>
        /// <param name="outputPath">Path of newly copied images</param>
        /// <returns>Makes a folder if there isnt one, copies images from source to destination</returns>
        public static void CopyResultsToDirectory(string inputSerial, string inputPath, string outputPath, string fileName)
        {
            if (Directory.Exists(outputPath))
            {
               // Console.WriteLine("Folder Already Exists");
                CopyResultsToDirectoryHelper(inputSerial, inputPath, outputPath, fileName);
            } else
            {
                try
                {
                    Directory.CreateDirectory(outputPath);
                    CopyResultsToDirectoryHelper(inputSerial, inputPath, outputPath, fileName);

                } catch (Exception Ex)
                {
                    Console.WriteLine("***** ERROR AT COPY RESULTS TO DIRECTORY *****\n" + Ex.Message);
                }
            }
        }

        private static void CopyResultsToDirectoryHelper(string inputSerial, string inputPath, string outputPath, string fileName)
        {
            string source = inputPath + "\\" + inputSerial;
            string destination = outputPath + "\\" +fileName;
            //Console.WriteLine(source);
            try
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(source);
                if(!sourceInfo.Exists)
                {
                    //Console.WriteLine(sourceInfo.FullName);
                    Console.WriteLine("***** Source Info missing COPY HELPER *****\n");
                    return;
                }

                DirectoryInfo destinationInfo = new DirectoryInfo(destination);

                if (!destinationInfo.Exists)
                {
                    destinationInfo.Create();
                }

                FileInfo[] files = sourceInfo.GetFiles();

                foreach(FileInfo file in files)
                {
                    string desFilePath = Path.Combine(destinationInfo.FullName, file.Name);
                    file.CopyTo(desFilePath, true);
                }

            } catch (Exception ex)
            {
                Console.WriteLine("****** CopyResultsToDirectoryHelper ERROR ******\n " + ex.Message);
            }
        }

        ///<summary>
        ///Gets Images and file names for the specific file.
        /// </summary>
        /// <param name="path">The path to the directory where the results are</param>
        /// 
        public static ProgramDetails GetProgramDetails(string path)
        {
            ProgramDetails programDetails = new ProgramDetails();
            try
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(path);
                //Console.WriteLine(sourceInfo.FullName);
                if (!sourceInfo.Exists)
                {
                    //Console.WriteLine(sourceInfo.FullName);
                    Console.WriteLine("***** GetProgramDetails missing File *****\n");
                    
                }
                else
                {
                        FileInfo[] files = sourceInfo.GetFiles();
                        ProgramObjs temp2 = new ProgramObjs(sourceInfo.Name);
                        programDetails.ProgramObject = temp2;
                        string frontImage, frontFile, backImage, backFile, leftImage, leftFile, 
                        rightImage, rightFile, bottomImage, bottomFile, topImage, topFile;
                        frontImage = frontFile = backImage = backFile = leftImage = leftFile =
                        rightImage = rightFile = bottomImage = bottomFile = topImage = topFile = "";
                    foreach (FileInfo file in files)
                        {
                            if (file.Name == "Display.jpg")
                                frontImage = file.FullName;
                            else if (file.Name == "Housing.jpg")
                                backImage = file.FullName;
                            else if (file.Name == "Left.jpg")
                                leftImage = file.FullName;
                            else if (file.Name == "Right.jpg")
                                rightImage = file.FullName;
                            else if (file.Name == "Top.jpg")
                                topImage = file.FullName;
                            else if (file.Name == "Bottom.jpg")
                                bottomImage = file.FullName;
                            else if (file.Name == "Back.csv")
                                backFile = file.FullName;
                            else if (file.Name == "Front.csv")
                                frontFile = file.FullName;
                            else if (file.Name == "Long.csv")
                            {
                                leftFile = file.FullName;
                                rightFile = file.FullName;
                            }
                            else if (file.Name == "Short.csv")
                            {
                                topFile = file.FullName;
                                bottomFile = file.FullName;
                            }
                        }

                        Front front = new Front(frontImage, frontFile);
                        Back back = new Back(backImage, backFile);
                        Top top = new Top(topImage, topFile);
                        Bottom bottom = new Bottom(bottomImage, bottomFile);
                        Left left = new Left(leftImage, leftFile);
                        Right right = new Right(rightImage, rightFile);

                    programDetails.sides[0] = front;
                    programDetails.sides[1] = back;
                    programDetails.sides[2] = top;
                    programDetails.sides[3] = bottom;
                    programDetails.sides[4] = left;
                    programDetails.sides[5] = right;
                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("****** CopyResultsToDirectoryHelper ERROR ******\n " + ex.Message);
            }

            return programDetails;
        }

        ///<summary>
        ///Reads the CSV file and displays data to the correct
        /// </summary>
        /// <param name="programDetails">Program Details generated</param>
        /// <param name="side"># corresponding to side</param>
        public static void DisplayData(Sides side, System.Windows.Forms.DataGridView dataGridView)
        {
          
            side.ReadFile();
            string[] regions = side.GetRegions();
            string[] highest = side.GetHighestDefect();
            string[] count = side.GetDefectCount();

            dataGridView.Rows.Clear();
            for (int i = 0; i <regions.Length; i++)
            {
                string[] temp = { regions[i], highest[i], count[i] };
                dataGridView.Rows.Add(temp);
            }
        }

        /// <summary>
        /// Displays the serial number and date at the textboxes 
        /// </summary>
        /// <param name="programs"></param>
        /// <param name="programObjs"></param>
        /// <param name="serialNum"></param>
        /// <param name="lastDate"></param>

        public static void DisplaySerialAndDate(List<ProgramObjs> programs, ProgramObjs programObjs, System.Windows.Forms.TextBox serialNum, System.Windows.Forms.TextBox lastDate, System.Windows.Forms.TextBox comptia, System.Windows.Forms.TextBox regions)
        {
            foreach (ProgramObjs program in programs)
            {
                if(programObjs.GetSerialNum() == program.GetSerialNum() && programObjs.GetLastDate() == program.GetLastDate())
                {
                    serialNum.Text = program.GetSerialNum();
                    lastDate.Text = program.GetLastDate();
                    string[] splitted = program.GetComptia().Split(',');
                    if (splitted[0] == "PASS")
                    {
                        comptia.Text = "PASS";
                        regions.Text = "N/A";
                    }
                    else if (splitted[0] == "FAIL")
                    {
                        comptia.Text = splitted[0] + ":" + splitted[1];
                        regions.Text = splitted[2];
                    }
                }
            }      
        }

        /// <summary>
        /// Deletes old runs saved images when starting the application.
        /// </summary>
        /// <param name="environment">Current environment the program is running in.</param>

        public static void DeleteOnStart(string environment, int flag)
        {
            string path = environment + "\\Resources\\"+ CurrentFlag(flag) + "\\";
            try
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                   // Console.WriteLine(directory);
                    Directory.Delete(directory, true);
                }
            } catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine("******* ERROR AT DeleteOnStart *******\n" + ex.Message);
            }
           
           
        }

        /// <summary>
        /// Gets a list of all current programs from the colors_program.xml file
        /// </summary>
        /// <returns></returns>
        public static List<ProgramObjs> GetCurrentPrograms()
        {
            List<ProgramObjs> objs = new List<ProgramObjs>();
            string cpPath = "c:\\EVO-3\\Parameters\\color_programs_evo_display_img.xml";
            //string cpPath = currentDirectory + "\\Resources\\color_programs.xml";
            try
            {
                using (XmlReader reader = XmlReader.Create(cpPath))
                {
                    reader.ReadToFollowing("ModelPrograms");
                    string currentModel = "";
                    while (reader.Read())
                    {

                        string currentModelAndColor = "";

                        if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Text)
                        {
                            if (reader.Value.Contains("iPhone"))
                            {
                                currentModel = reader.Value;
                            }
                            if (!reader.Name.Contains("ModelPrograms") && !reader.Name.Contains("audit") && !reader.Name.Contains("model") && !string.IsNullOrWhiteSpace(reader.Name)
                                    && !reader.Name.Contains("large") && !reader.Name.Contains("small"))
                            {
                                char[] color = reader.Name.ToCharArray();
                                color[0] = char.ToUpper(color[0]);
                                string colorUpper = new string(color);
                                if (colorUpper.Equals("Gray"))
                                {
                                    colorUpper = "Space Gray";
                                }
                                currentModelAndColor = currentModel + "," + colorUpper;
                                objs.Add(new ProgramObjs(currentModelAndColor));
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ***** ERROR At GetCurrentProgram *****\n " + ex.Message);
            }

            return objs;
        }

        /// <summary>
        /// Finds all file names of FAT-SAT files
        /// </summary>
        /// <returns>List of FAT SAT files</returns>
        public static List<FileInfo> GetAllFatSatFiles()
        {

            string path = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\";
            //string path = currentDirectory + "\\Resources\\FAT-SAT\\";

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToArray();
            List<FileInfo> result = files.ToList<FileInfo>();
            return result;
        }

        ///<summary>
        ///Gets FAT-SAT files for the specific date
        /// </summary>
        /// <returns>The FAT-SAT file for the specific date</returns>
        public static FileInfo GetFatSatFile(string date)
        {
            //string path = currentDirectory + "\\Resources\\FAT-SAT\\";
            string path = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach(FileInfo file in dir.GetFiles().OrderByDescending(f => f.LastWriteTime))
            {
                if(file.Name.Contains(date))
                {
                    return file;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds individual serial numbers based on last run program (color and model) within a fat-sat file.
        /// </summary>
        /// <param name="programObjs">list of program objects that gets configured</param>
        /// <param name="path">FAT-SAT file path</param>
        public static void FindSerials(List<ProgramObjs> programObjs, FileInfo path)
        {

            string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + path.Name;

            //string fullPath = currentDirectory + "\\Resources\\FAT-SAT\\" + path.Name;
            //Change Full Path when in prod
            var lines = File.ReadAllLines(fullPath).Reverse();
            foreach (ProgramObjs program in programObjs)
            {
                foreach (string line in lines)
                {
                    if (line.Contains(program.GetModelAndColor())){
                        if (program.GetSerialNum() == "")
                        {
                            string[] splitted = line.Split(',');
                            program.SetSerialNum(splitted[3]);
                            string[] dateSplit = splitted[0].Split(' ');
                            program.SetLastDate(dateSplit[0].Replace("/", "-"));
                            program.SetComptia(splitted[4] + "," + splitted[5] + "," + splitted[6]);
                        }
                    }
                }
            }
            
          
        }


        ///<summary>
        /// Sets the path of the out directory based on the currently running function
        /// </summary>
        /// <param name="flag">number indicating current function</param>
        public static string CurrentFlag(int flag)
        {
            string outDirPath;
            switch (flag)
            {
                case 1:
                    outDirPath = "AllLatestModels";
                    return outDirPath;
                case 2:
                    outDirPath = "ManualSearch";
                    return outDirPath;
                case 3:
                    outDirPath = "ModelSearch";
                    return outDirPath;
            }
            return null;
        }


        public static void DisplayComptiaAndRegions(ProgramObjs current, System.Windows.Forms.TextBox comptia, System.Windows.Forms.TextBox regions)
        {
           
            string[] splitted = current.GetComptia().Split(',');
            if (splitted[0] == "PASS")
            {
                comptia.Text = "PASS";
                regions.Text = "N/A";
            }
            else if (splitted[0] == "FAIL")
            {
                comptia.Text = splitted[0] + ":" + splitted[1];
                regions.Text = splitted[2];
            }
              
        }
    }

}
