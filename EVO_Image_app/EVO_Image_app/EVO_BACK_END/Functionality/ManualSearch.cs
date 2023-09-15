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
                                //Console.WriteLine(line);
                                string[] splitted = line.Split(',');
                                string[] dateSplit = splitted[0].Split(' ');
                                ProgramObjs program = new ProgramObjs(splitted[1] + ',' + splitted[2], splitted[3], dateSplit[0].Replace("/", "-"), splitted[4] + "," + splitted[5] + "," + splitted[6], dateSplit[1] + " " + dateSplit[2]);
                                programObjs.Add(program);
                            }
                        }
                    }

                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At FindProgramData ******\n" + ex.Message);
                }
            }

            GetImagesForSerialHelper();

        }


        private void GetImagesForSerialHelper()
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string today = Common.GetDate();
            HashSet<string> unique = new HashSet<string>();
            List<ProgramObjs> uniqueObjs = new List<ProgramObjs>();
            List<ProgramObjs> duplicates = new List<ProgramObjs>();
            List<ProgramObjs> finalList = new List<ProgramObjs>();
            foreach (ProgramObjs p in programObjs)
            {
                if (!unique.Add(p.GetSerialNum()))
                {
                    duplicates.Add(p);
                }
                else
                {
                    uniqueObjs.Add(p);
                }
            }

            int count = 0;
            foreach (ProgramObjs d in duplicates)
            {
                string currentSerial = d.GetSerialNum();

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

            foreach (ProgramObjs obj in uniqueObjs)
            {
                finalList.Add(obj);
            }

            foreach (ProgramObjs duplicate in duplicates)
            {
                finalList.Add(duplicate);
            }

            foreach (ProgramObjs f in finalList)
            {
                string serial = f.GetSerialNum();
                string lastDate = f.GetLastDate();
                string inDirPath = dailyRunData + "\\" + lastDate;
                string outDirPath = Common.currentDirectory + "\\Resources\\ManualSearch\\" + today;

                string fileName = f.GetSerialNum();
                if (lastDate != "" && lastDate != "iPhone")
                {
                    Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
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


