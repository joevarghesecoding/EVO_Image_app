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


        public override void GetModelImages(ProgramObjs program, string date)
        {
            //string dailyRunData = Common.currentDirectory + "\\Resources";
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string today = Common.GetDate();
            try
            {
                FileInfo file = GetFatSatFile(date);
                if(file != null)
                {
                    FindSerialCount(program, file);
                    HashSet<string> unique = new HashSet<string>();
                    List<ProgramObjs> duplicates = new List<ProgramObjs>();
                    List<ProgramObjs> finalList = new List<ProgramObjs>();
                    foreach(ProgramObjs p in foundPrograms)
                    {
                        if (!unique.Add(p.GetSerialNum()))
                        {
                            duplicates.Add(p);
                        }
                    }

                    foreach(ProgramObjs d in duplicates)
                    {
                        string currentSerial = d.GetSerialNum();
                        int count = 0;
                        string current = currentSerial;
                        if (!currentSerial.Equals(current))
                            count = 0;
                        while (currentSerial.Equals(current))
                        {
                            d.SetSerialNum(currentSerial + " - " + count.ToString());
                            count++;
                            
                            current = d.GetSerialNum();
                        }
                    }
                    
                    foreach(ProgramObjs obj in foundPrograms)
                    {
                        if (unique.Contains(obj.GetSerialNum()))
                        {
                            finalList.Add(obj);
                        }
                    }

                    foreach (ProgramObjs duplicate in duplicates)
                    {
                        finalList.Add(duplicate);
                    }

                    foreach(ProgramObjs f in finalList)
                    {
                        string serial = f.GetSerialNum();
                        string lastDate = f.GetLastDate();
                        string inDirPath = dailyRunData + "\\" + date;
                        string outDirPath = Common.currentDirectory + "\\Resources\\ModelSearch\\" + today;

                        string fileName = f.GetSerialNum() + "," + f.GetModelAndColor();
                        if (date != "" && date != "iPhone")
                        {
                            Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                        }

                    }


                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No Data found for Date");
                }
            } catch(NullReferenceException ex)
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Date");
                Console.WriteLine("******* ERROR AT GetModelImages ******\n" + ex.Message);
            }
            
        }

        private void FindSerialCount(ProgramObjs program, FileInfo file)
        {
            if(file != null)
            {
                string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + file.Name;
                //string fullPath = Environment.CurrentDirectory + "\\Resources\\FAT-SAT\\" + file.Name;
                
                try
                {
                    using(StreamReader reader = new StreamReader(fullPath))
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
                                ProgramObjs temp = new ProgramObjs(program.GetModelAndColor(), serial, date, comptia, dateSplit[1] + " " + dateSplit[2]);
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

    }
}
