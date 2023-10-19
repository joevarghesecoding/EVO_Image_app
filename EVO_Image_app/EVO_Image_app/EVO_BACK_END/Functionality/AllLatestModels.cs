using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    class AllLatestModels : Functions
    {

        string cachePath = Environment.CurrentDirectory + "\\Resources\\cache.txt";

        public AllLatestModels() : base() 
        {
            programObjs = GetCurrentPrograms();
            GetLatestSerials(programObjs);
        }


        /// <summary>
        /// Gets all the images for the serial numbers and creates a folder for it.
        /// </summary>
        /// <return>
        /// A directory with all the latest phone images.
        /// </return>
        override public void GetLatestImages()
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string today = Common.GetDate();


            foreach (ProgramObjs obj in programObjs)
            {
                string serial = obj.GetSerialNum();
                string date = obj.GetLastDate();
                string inDirPath = dailyRunData + "\\" + date;
                string outDirPath = Common.currentDirectory + "\\Resources\\AllLatestModels\\" + today;

                string fileName = obj.GetModelAndColor();
                if (date != "" && date != "iPhone")
                {
                    Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                }
            }
        }

        public override void GetModelImages(ProgramObjs program, string date)
        {
            throw new NotImplementedException();
        }

        public override void GetImagesForSerial(string serial)
        {
            throw new NotImplementedException();
        }


        private void GetLatestSerials(List<ProgramObjs> programObjs)
        {
            DateTime fiveDaysAgo = getFiveDaysAgoDate();
            List<FileInfo> fatSatFiles = GetAllFatSatFiles();
            createCache(programObjs, fatSatFiles);
            foreach (FileInfo file in fatSatFiles)
            {
                string fd = file.Name.Split(' ')[0];
                DateTime fileDate = DateTime.Parse(fd);
                if(!compareDates(fileDate, fiveDaysAgo))
                {
                    FindSerials(programObjs, file);
                }
                else
                {
                    FindSerialsWithCache(programObjs, fatSatFiles);
                }
                
            }
        }

        private bool compareDates(DateTime fileDate, DateTime fiveDaysAgo)
        {
            TimeSpan difference = fileDate - fiveDaysAgo;
            TimeSpan five = TimeSpan.FromDays(-5);
            return difference <= five;
        }

        private DateTime getFiveDaysAgoDate()
        {
            DateTime now = DateTime.Today;
            DateTime fiveDaysAgo = now.AddDays(-5);
            return fiveDaysAgo;
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void createCache(List<ProgramObjs> programObjs, List<FileInfo> fatSatFiles)
        {
            FileInfo cache = new FileInfo(cachePath);
            if (!cache.Exists)
            {
                using (FileStream fs = File.Create(cachePath))
                {
                    foreach (ProgramObjs program in programObjs)
                    {
                        AddText(fs, program.GetModelAndColor());
                    }
                }

                foreach(FileInfo file in fatSatFiles)
                {
                    FindSerials(programObjs, file);
                }
            }
        }

        /// <summary>
        /// Finds individual serial numbers based on last run program (color and model) within a fat-sat file.
        /// </summary>
        /// <param name="programObjs">list of program objects that gets configured</param>
        /// <param name="path">FAT-SAT file path</param>
        private void FindSerials(List<ProgramObjs> programObjs, FileInfo path)
        {

            string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + path.Name;
            //string fullPath = currentDirectory + "\\Resources\\FAT-SAT\\" + path.Name;
            //Change Full Path when in prod
            var lines = File.ReadAllLines(fullPath).Reverse();
            foreach (ProgramObjs program in programObjs)
            {
                foreach (string line in lines)
                {
                    if (line.Contains(program.GetModelAndColor()))
                    {
                        if(program.GetSerialNum() == "")
                        {
                            updateProgramObjs(program, line);
                        }
                        
                    }
                }
            }
        }

        private void updateProgramObjs(ProgramObjs program, string line)
        {
            string[] splitted = line.Split(',');
            program.SetSerialNum(splitted[3]);
            string[] dateSplit = splitted[0].Split(' ');
            string date = dateSplit[0].Replace("/", "-");
            program.SetLastDate(date);
            program.SetComptia(splitted[4] + "," + splitted[5] + "," + splitted[6]);
            program.SetLastTime(dateSplit[1] + " " + dateSplit[2]);
            using (FileStream fileStream = new FileStream(cachePath, FileMode.Open, FileAccess.Read))
            {
                // Create a StreamReader to read from the FileStream
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    // Read the text from the file
                    string l;
                    while ((l = reader.ReadLine()) != null)
                    {
                        if (l.Contains(program.GetModelAndColor()))
                        {
                            l = l.Replace(l, program.GetModelAndColor() + ":" + date);
                        }
                    }
                }
            }
        }

        private void FindSerialsWithCache(List<ProgramObjs> programObjs, List<FileInfo> fatSatFiles)
        {
            foreach(ProgramObjs program in programObjs)
            {
                if(program.GetSerialNum() == "")
                {
                    using (FileStream fileStream = new FileStream(cachePath, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            // Read the text from the file
                            string l;
                            while ((l = reader.ReadLine()) != null)
                            {
                                if (l.Contains(program.GetModelAndColor()))
                                {
                                    string date = l.Split(':')[1];
                                    foreach(FileInfo file in fatSatFiles)
                                    {
                                        if (file.Name.Contains(date))
                                        {
                                            var lines = File.ReadAllLines(file.FullName).Reverse();
                                            foreach (string line in lines)
                                            {
                                                if (line.Contains(program.GetModelAndColor()))
                                                {
                                                    updateProgramObjs(program, line);
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

     
    }
}
