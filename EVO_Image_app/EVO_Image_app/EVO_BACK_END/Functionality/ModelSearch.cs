﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    class ModelSearch : Functions
    {
        

        public ModelSearch() : base() 
        {
            programObjs = GetCurrentPrograms();
        }

        private string[] types = { "ALL", "LINTLOGIC FLIPS", "PASS", "FAIL", "MAC", "MBG", "MEA", "DEA", "D9J", "DAC", "B9J", "D92", "B92", "D9C", "B91" };

        public string getType(int index)
        {
            return types[index];
        }

        

        public override void GetAllModelImages(ProgramObjs program, string date, int type)
        {
            //foundPrograms = new List<ProgramObjs>();
            string currentType = types[type];
            try
            {
                FileInfo file = GetFatSatFile(date);
                if (file != null)
                {
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] splitted = line.Split(',');
                            string modelAndColor = splitted[1] + "," + splitted[2];
                            string serial = splitted[3];
                            string comptia = splitted[4] + "," + splitted[5] + "," + splitted[6];
                            string[] dateSplit = splitted[0].Split(' ');
                            if (modelAndColor.Contains(program.GetModelAndColor()) && comptia.Contains(currentType))
                            {
                                ProgramObjs temp = new ProgramObjs(modelAndColor, serial, date, splitted[4] + " " + splitted[5], comptia, dateSplit[1] + " " + dateSplit[2]);
                                foundPrograms.Add(temp);
                            }
                            
                        }
                    }
                }

                SendToDirectory(foundPrograms, date, false);
            }
            catch (NullReferenceException ex)
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Date");
                Console.WriteLine("******* ERROR AT GetAllModelImages ******\n" + ex.Message);
            }
        }

        public override void GetLintLogicImages(string date)
        {
            LintLogicCandidates = new List<ProgramObjs>();
            string logsPath = "C:\\EVO-3\\Save Data\\Logs\\Device\\" + date;

            DirectoryInfo dir = new DirectoryInfo(logsPath);
            FileInfo[] files = dir.GetFiles();
            string serial = "", time = "";
            foreach (FileInfo file in files)
            {
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if(line.Contains("SCANNED ID"))
                        {
                            //Console.WriteLine(line);
                            serial = line.Split(' ')[2];
                            //Console.WriteLine("SERIAL : " + serial);
                        }
                        if(line.Contains("START DATE/TIME:"))
                        {
                            string[] splitted = line.Split(' ');
                            time = splitted[3] + " " + splitted[4];
                        }
                        if (line.Contains("LintCheck_Grade-PASS"))
                        {
                            ProgramObjs temp = new ProgramObjs(serial, date, time, "LintCheck_Grade-PASS");
                            LintLogicCandidates.Add(temp);
                            break;
                        }
                        else if (line.Contains("LintCheck_Grade-MAC"))
                        {
                            ProgramObjs temp = new ProgramObjs(serial, date, time, "LintCheck_Grade-MAC");
                            LintLogicCandidates.Add(temp);
                            break;
                        }
                    }
                }
            }

            CollectLintLogicCandidatesInfo(date);
            SendToDirectory(LintLogicCandidates, date, true);

        }

        private void CollectLintLogicCandidatesInfo(string date)
        {
            FileInfo fatSat = GetFatSatFile(date);
            using(StreamReader reader = new StreamReader(fatSat.FullName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (ProgramObjs obj in LintLogicCandidates)
                    {
                        if (line.Contains(obj.GetSerialNum()))
                        {
                            string[] splitted = line.Split(',');
                            string serial = splitted[3];
                            string[] dateSplit = splitted[0].Split(' ');
                            string comptia = splitted[4] + "," + splitted[5] + "," + splitted[6];
                            obj.SetModelAndColor(splitted[1] + "," + splitted[2]);
                            obj.SetResult(splitted[4] + " " + splitted[5]);
                            obj.SetComptia(comptia);
                        }
                    }
                }      
            } 
        }

       private void SendToDirectory(List<ProgramObjs> programObjs, string date, bool lintLogicFlag)
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            foreach (ProgramObjs f in programObjs)
            {
                string serial = f.GetSerialNum();
                string lastDate = f.GetLastDate();
                string inDirPath = dailyRunData + "\\" + date;
                string outDirPath = Path.GetFullPath(Common.currentDirectory + "\\Resources\\ModelSearch\\" + date + "\\" + f.GetModelAndColor() + "\\" + f.GetResult());
                
                if (lintLogicFlag)
                {
                    outDirPath = Path.GetFullPath(Common.currentDirectory + "\\Resources\\ModelSearch\\" + date + "\\" + f.GetLintLogicResult()) + "\\" + f.GetModelAndColor();
                    //Console.WriteLine(outDirPath);
                }
                f.SetOutputDirectoryPath(outDirPath);
                
                string fileName = f.GetSerialNum() + "," + f.GetModelAndColor() + "," + f.GetResult();
                if (date != "" && date != "iPhone")
                {
                    Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                }

            }
        }

        public override void GetModelImages(ProgramObjs program, string date, int type)
        {
        //    foundPrograms = new List<ProgramObjs>();
        //    try
        //    {
        //        FileInfo file = GetFatSatFile(date);
        //        if(file != null)
        //        {
        //            FindSerialCount(program, file);
        //            HashSet<string> unique = new HashSet<string>();
        //            List<ProgramObjs> duplicates = new List<ProgramObjs>();
        //            List<ProgramObjs> finalList = new List<ProgramObjs>();
        //            foreach(ProgramObjs p in foundPrograms)
        //            {
        //                if (!unique.Add(p.GetSerialNum()))
        //                {
        //                    duplicates.Add(p);
        //                }
        //            }

        //            foreach(ProgramObjs d in duplicates)
        //            {
        //                string currentSerial = d.GetSerialNum();
        //                int count = 0;
        //                string current = currentSerial;
        //                if (!currentSerial.Equals(current))
        //                    count = 0;
        //                while (currentSerial.Equals(current))
        //                {
        //                    d.SetSerialNum(currentSerial + " - " + count.ToString());
        //                    count++;
                            
        //                    current = d.GetSerialNum();
        //                }
        //            }
                    
        //            foreach(ProgramObjs obj in foundPrograms)
        //            {
        //                if (unique.Contains(obj.GetSerialNum()))
        //                {
        //                    finalList.Add(obj);
        //                }
        //            }

        //            foreach (ProgramObjs duplicate in duplicates)
        //            {
        //                finalList.Add(duplicate);
        //            }

        //            SendToDirectory(foundPrograms, date, false);


        //        }
        //        else
        //        {
        //            System.Windows.Forms.MessageBox.Show("No Data found for Date");
        //        }
        //    } catch(NullReferenceException ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show("Incorrect Date");
        //        Console.WriteLine("******* ERROR AT GetModelImages ******\n" + ex.Message);
        //    }
            
        }

        private void FindSerialCount(ProgramObjs program, FileInfo file)
        {
            if(file != null)
            {
                string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + file.Name;

                try
                {
                    using (StreamReader reader = new StreamReader(fullPath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(program.GetModelAndColor()))
                            {
                                string[] splitted = line.Split(',');
                                string serial = splitted[3];
                                string[] dateSplit = splitted[0].Split(' ');
                                string date = dateSplit[0].Replace("/", "-");
                                string comptia = splitted[4] + "," + splitted[5] + "," + splitted[6];
                                ProgramObjs temp = new ProgramObjs(program.GetModelAndColor(), serial, date, splitted[4] + " " + splitted[5], comptia, dateSplit[1] + " " + dateSplit[2]);
                                foundPrograms.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At FindSerialCount ******\n" + ex.Message);
                }
            }
        }

        ///<summary>
        ///Gets FAT-SAT files for the specific date
        /// </summary>
        /// <returns>The FAT-SAT file for the specific date</returns>
        public FileInfo GetFatSatFile(string date)
        {
            //string path = currentDirectory + "\\Resources\\FAT-SAT\\";
            string path = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.GetFiles().OrderByDescending(f => f.LastWriteTime))
            {
                if (file.Name.Contains(date))
                {
                    return file;
                }
            }
            return null;
        }

        public override void GetImagesForSerial(string serial)
        {
            throw new NotImplementedException();
        }

        public override void GetLatestImages()
        {
            throw new NotImplementedException();
        }

        public override void GetAllModelImages(ProgramObjs program, string date)
        {
            try
            {
                FileInfo file = GetFatSatFile(date);
                if (file != null)
                {
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] splitted = line.Split(',');
                            string modelAndColor = splitted[1] + "," + splitted[2];
                            string serial = splitted[3];
                            string comptia = splitted[4] + "," + splitted[5] + "," + splitted[6];
                            string[] dateSplit = splitted[0].Split(' ');
                            if (modelAndColor.Contains(program.GetModelAndColor()))
                            {
                                ProgramObjs temp = new ProgramObjs(modelAndColor, serial, date, splitted[4] + " " + splitted[5], comptia, dateSplit[1] + " " + dateSplit[2]);
                                foundPrograms.Add(temp);
                            }

                        }
                    }
                }

                SendToDirectory(foundPrograms, date, false);
            }
            catch (NullReferenceException ex)
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Date");
                Console.WriteLine("******* ERROR AT GetAllModelImages ******\n" + ex.Message);
            }
        }
    }
}
