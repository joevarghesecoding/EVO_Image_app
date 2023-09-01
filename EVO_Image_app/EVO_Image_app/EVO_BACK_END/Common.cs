﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

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
               // Console.WriteLine(sourceInfo.FullName);
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

        public static void DisplaySerialAndDate(ProgramObjs programObjs, System.Windows.Forms.TextBox serialNum, System.Windows.Forms.TextBox lastDate)
        {
            List<ProgramObjs> programs = AllLatestModels.GetProgramObjs();
            foreach (ProgramObjs program in programs)
            {
                if(programObjs.GetModelAndColor() == program.GetModelAndColor())
                {
                    serialNum.Text = program.GetSerialNum();
                    lastDate.Text = program.GetLastDate();
                }
            }      
        }

        /// <summary>
        /// Deletes old runs saved images when starting the application.
        /// </summary>
        /// <param name="environment">Current environment the program is running in.</param>

        public static void DeleteOnStart(string environment)
        {
            string path = environment + "\\Resources\\AllLatestModels";
            string[] directories = Directory.GetDirectories(path);
            foreach(string directory in directories)
            {
                Directory.Delete(directory, true);
            }
        }

        /// <summary>
        /// Gets a list of all current programs from the colors_program.xml file
        /// </summary>
        /// <returns></returns>
        public static List<ProgramObjs> GetCurrentPrograms()
        {
            List<ProgramObjs> objs = new List<ProgramObjs>();
            //string cpPath = "c:\\EVO-3\\Parameters\\color_programs_evo_display_img.xml";
            string cpPath = currentDirectory + "\\Resources\\color_programs.xml";
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

            //string path = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\";
            string path = currentDirectory + "\\Resources\\FAT-SAT\\";

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToArray();
            List<FileInfo> result = files.ToList<FileInfo>();
            return result;
        }

        /// <summary>
        /// Finds individual serial numbers based on last run program (color and model) within a fat-sat file.
        /// </summary>
        /// <param name="programObjs">list of program objects that gets configured</param>
        /// <param name="path">FAT-SAT file path</param>
        public static void FindSerials(List<ProgramObjs> programObjs, FileInfo path)
        {

            //string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + path.Name;
            string fullPath = currentDirectory + "\\Resources\\FAT-SAT\\" + path.Name;
            //Change Full Path when in prod
            foreach (ProgramObjs objs in programObjs)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(objs.GetModelAndColor()))
                            {
                                if (objs.GetSerialNum() == "")
                                {
                                    string[] splitted = line.Split(',');
                                    objs.SetSerialNum(splitted[3]);
                                    string[] dateSplit = splitted[0].Split(' ');
                                    objs.SetLastDate(dateSplit[0].Replace("/", "-"));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At Find Serials ******\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Finds color and model data within a fat-sat file using a serial number.
        /// </summary>
        /// <param name="serial">serial number</param>
        /// <param name="path">FAT-SAT file path</param>
        public static ProgramObjs FindProgramData(string serial, FileInfo path)
        {

            //string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + path.Name;
            string fullPath = currentDirectory + "\\Resources\\FAT-SAT\\" + path.Name;
            //Change Full Path when in prod
             try
                {
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(serial))
                            {
                                string[] splitted = line.Split(',');
                                string[] dateSplit = splitted[0].Split(' ');

                                ProgramObjs program = new ProgramObjs(splitted[1] + ',' + splitted[2], serial, dateSplit[0].Replace("/", "-"));
                                return program;
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At FindProgramData ******\n" + ex.Message);
                }

               return null;
            }
        }
    }

