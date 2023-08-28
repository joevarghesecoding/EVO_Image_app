using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EVO_Image_app.EVO_BACK_END
{
    class AllLatestModels
    {
        public static List<ProgramObjs> programObjs; 
        string today = Common.GetDate();
        static string currentDirectory = Environment.CurrentDirectory;
        /// <summary>
        /// Gets all the images for the serial numbers and creates a folder for it.
        /// </summary>
        /// <return>
        /// A directory with all the latest phone images.
        /// </return>
        public static void GetLatestImages()
        {
            //string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string dailyRunData = currentDirectory + "\\Resources";
            programObjs = GetCurrentPrograms();
            GetLatestSerials(programObjs);
            string today = Common.GetDate();

            //foreach (ProgramObjs obj in Pobjs)
            //{
            //    Console.WriteLine(obj.GetModelAndColor());
            //    Console.WriteLine(obj.GetSerialNum());
            //    Console.WriteLine(obj.GetLastDate());
            //}
            
            //Console.WriteLine(currentDirectory);

            foreach (ProgramObjs obj in programObjs)
            {
                string serial = obj.GetSerialNum();
                string date = obj.GetLastDate();
                string inDirPath = dailyRunData + "\\" + date;
                //string outDirPath = "C:\\Users\\Joe.Varghese\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today;
                //string outDirPath = "C:\\Users\\Administrator\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\bin\\Debug\\Resources\\AllLatestModels\\" + today;
                string outDirPath = currentDirectory + "\\Resources\\AllLatestModels\\" + today;

                string fileName = obj.GetModelAndColor();
                if (date != "" && date != "iPhone")
                {
                    Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                }
            }
        }

        public static ProgramDetails GetProgramDetails(string outDirPath)
        {
            //string today = Common.GetDate();
            //string outDirPath = "C:\\Users\\Joe.Varghese\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today;
            return Common.GetProgramDetails(outDirPath);
        }

        private static List<ProgramObjs> GetCurrentPrograms()
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

                        if(reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Text)
                        {
                            if (reader.Value.Contains("iPhone"))
                            {
                                //Console.WriteLine("Model: " + reader.Value);
                                currentModel = reader.Value;
                                //Console.WriteLine(" Current Model: " + reader.Value);

                            }
                            if (!reader.Name.Contains("ModelPrograms") && !reader.Name.Contains("audit") && !reader.Name.Contains("model") && !string.IsNullOrWhiteSpace(reader.Name)
                                    && !reader.Name.Contains("large") && !reader.Name.Contains("small"))
                            {
                                //Console.WriteLine("Color: " + reader.Name);
                                char[] color = reader.Name.ToCharArray();
                                color[0] = char.ToUpper(color[0]);
                                string colorUpper = new string(color);
                                if (colorUpper.Equals("Gray"))
                                {
                                    colorUpper = "Space Gray";
                                }
                                currentModelAndColor = currentModel + "," + colorUpper;
                                //Console.WriteLine("CurrentModelAndcolor: " + currentModelAndColor);
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

            //foreach(ProgramObjs obj in objs)
            //{
            //    Console.WriteLine(obj.GetModelAndColor());
            //}
            return objs;
        }

        private static List<FileInfo> GetAllFatSatFiles()
        {
           
            //string path = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\";
            string path = currentDirectory + "\\Resources\\FAT-SAT\\";

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToArray();
            //Array.Reverse(files);
            List<FileInfo> result = files.ToList<FileInfo>();
            return result;
        }

        private static void FindSerials(List<ProgramObjs> programObjs, FileInfo path)
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
                                if(objs.GetSerialNum() == "")
                                {
                                    string[] splitted = line.Split(',');
                                    objs.SetSerialNum(splitted[3]);
                                    string[] dateSplit = splitted[0].Split(' ');
                                    objs.SetLastDate(dateSplit[0].Replace("/","-"));
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
            //foreach(ProgramObjs obj in programObjs)
            //{
            //    Console.WriteLine(obj.GetModelAndColor());
            //    Console.WriteLine(obj.GetSerialNum());
            //    Console.WriteLine(obj.GetLastDate());
            //}
        }

        private static void GetLatestSerials(List<ProgramObjs> programObjs)
        {
            List<FileInfo> fatSatFiles = GetAllFatSatFiles();
            foreach(FileInfo file in fatSatFiles)
            {
                FindSerials(programObjs, file);
            }
        }
    }
}
