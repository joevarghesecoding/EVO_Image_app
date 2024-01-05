using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    class ManualSearch : Functions
    {
        

        public ManualSearch() : base() 
        {
            programObjs = new List<ProgramObjs>();
            manualSearchProgams = new Dictionary<string, List<ProgramObjs>>();
        }

      
        /// <summary>
        /// Gets images for the serial number and creates a folder for it.
        /// </summary>
        /// <return>
        /// A directory with the serial's images.
        /// </return>
        override public void GetImagesForSerial(string serial)
        {

            // string dailyRunData = Common.currentDirectory + "\\Resources";
            List<FileInfo> fatSatPaths = GetAllFatSatFiles();

            foreach (FileInfo fileInfo in fatSatPaths)
            {
                string fullPath = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\" + fileInfo.Name;
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
                                string modelAndColor = splitted[1] + ',' + splitted[2];
                                string lastDate = dateSplit[0].Replace("/", "-");
                                string result = splitted[4] + "," + splitted[5];
                                string lastTime = dateSplit[1] + " " + dateSplit[2];
                                string comptia = splitted[6];
                                ProgramObjs program = new ProgramObjs(modelAndColor, serial, lastDate, result, comptia, lastTime.Replace(':', '-'));
                                string outDirPath = Path.GetFullPath(Common.currentDirectory + "\\Resources\\ManualSearch\\" + lastDate);
                                program.SetOutputDirectoryPath(outDirPath);
                                //programObjs.Add(program);
                               
                                if (manualSearchProgams.ContainsKey(lastDate))
                                {
                                    manualSearchProgams[lastDate].Add(program);
                                }
                                else
                                {
                                    List<ProgramObjs> newDateList = new List<ProgramObjs>();
                                    newDateList.Add(program);
                                    manualSearchProgams.Add(lastDate, newDateList);
                                }

                                
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At FindProgramData ******\n" + ex.Message);
                }
            }
            RenameSerials(manualSearchProgams, serial);
            SendData(manualSearchProgams);

        }

        private void RenameSerials(Dictionary<string, List<ProgramObjs>> dictionary, string serial)
        {
            foreach (var kvp in dictionary)
            {
                List<ProgramObjs> temp = kvp.Value;
                List<string> duplicates = new List<string>();
                HashSet<string> unique = new HashSet<string>();
                foreach (ProgramObjs p in temp)
                {
                    if (!unique.Add(p.GetSerialNum()))
                    {
                        duplicates.Add(p.GetSerialNum());
                        var count = duplicates.Count(s => s == p.GetSerialNum());
                        if(count == 1)
                        {
                            p.SetSerialNum(p.GetSerialNum() + " - 0");
                        }
                        if(count > 1)
                        {
                            p.SetSerialNum(p.GetSerialNum() + " - " + (count - 1).ToString());
                        }
                    }
                }
            }
        }


        private void SendData(Dictionary<string, List<ProgramObjs>> manualSearchProgams)
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";

            foreach(var kvp in manualSearchProgams)
            {
                List<ProgramObjs> programs = kvp.Value;
                foreach(ProgramObjs f in programs)
                {
                    string serial = f.GetSerialNum();
                    string lastDate = f.GetLastDate();
                    string inDirPath = dailyRunData + "\\" + lastDate;
                    string outDirPath = Path.GetFullPath(Common.currentDirectory + "\\Resources\\ManualSearch\\" + lastDate);
                    string fileName = f.GetSerialNum() + " " + f.GetLastTime().Replace(':', '-');
                    if (lastDate != "" && lastDate != "iPhone")
                    {
                        Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                    }
                }
            }
        }


        public override void GetLatestImages()
        {
            throw new NotImplementedException();
        }

        public override void GetModelImages(ProgramObjs program, string date, int type)
        {
            throw new NotImplementedException();
        }

        public override void GetAllModelImages(ProgramObjs program, string date, int type)
        {
            throw new NotImplementedException();
        }

        public override void GetLintLogicImages(string date)
        {
            throw new NotImplementedException();
        }

        public override void GetAllModelImages(ProgramObjs program, string date)
        {
            throw new NotImplementedException();
        }
    }

}


