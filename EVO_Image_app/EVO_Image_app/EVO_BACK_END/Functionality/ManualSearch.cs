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

        public ManualSearch() : base() { }

        /// <summary>
        /// Gets images for the serial number and creates a folder for it.
        /// </summary>
        /// <return>
        /// A directory with the serial's images.
        /// </return>
        override public void GetImagesForSerial(string serial)
        {
            
            // string dailyRunData = Common.currentDirectory + "\\Resources";
            List<FileInfo> fatSatPaths = Common.GetAllFatSatFiles();
            foreach(FileInfo fileInfo in fatSatPaths)
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
                                //Console.WriteLine(line);
                                string[] splitted = line.Split(',');
                                string[] dateSplit = splitted[0].Split(' ');
                                ProgramObjs program = new ProgramObjs(splitted[1] + ',' + splitted[2], serial, dateSplit[0].Replace("/", "-"), splitted[4] + "," + splitted[5] + "," + splitted[6]);
                                programObjs.Add(program);
                            }
                        }
                    }
                    GetImagesForSerialHelper(serial);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At FindProgramData ******\n" + ex.Message);
                }
            }

        }

        private void GetImagesForSerialHelper(string serial)
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string today = Common.GetDate();
            string[] dates = new string[programObjs.Count];
            int[] count = new int[dates.Length];
            int i = 0;
            foreach(ProgramObjs program in programObjs)
            {
                dates[i] = program.GetLastDate();
                i++;
            }
            for(int j = 0; j < count.Length - 1; j++)
            {
                int next = j + 1;
               
                if(next < count.Length && dates[j] == dates[next])
                {
                    count[next] = count[j] + 1;
                }
            }

            for(int k = 0; k < count.Length; k++)
            {
                if (count[k] > 0)
                {
                    programObjs.ElementAt(k).SetSerialNum(serial + " - " + (count[k] - 1).ToString());
                    programObjs.ElementAt(k).SetLastDate(dates[k]);
                }
                else
                {
                    programObjs.ElementAt(k).SetSerialNum(serial);
                    programObjs.ElementAt(k).SetLastDate(dates[k]);
                }
                    
                //Console.WriteLine(programObjs.ElementAt(k).GetSerialNum() + " " + dates[k]);
            }

            

            foreach (ProgramObjs f in programObjs)
            {
                string newSerial = f.GetSerialNum();
                string lastDate = f.GetLastDate();
                string inDirPath = dailyRunData + "\\" + lastDate;
                //Console.WriteLine(inDirPath);    
                string outDirPath = Common.currentDirectory + "\\Resources\\ManualSearch\\" + today;
                string fileName = f.GetSerialNum();
                if (lastDate != "" && lastDate != "iPhone")
                {
                    Common.CopyResultsToDirectory(newSerial, inDirPath, outDirPath, fileName + " " + f.GetLastDate());
                }

            }
        }


        public override void GetLatestImages()
        {
            throw new NotImplementedException();
        }

        public override void GetModelImages(ProgramObjs program, string date)
        {
            throw new NotImplementedException();
        }
    }
}
