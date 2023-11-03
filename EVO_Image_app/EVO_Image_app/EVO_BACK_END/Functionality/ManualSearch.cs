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
                                ProgramObjs program = new ProgramObjs(modelAndColor, serial + " " + lastTime.Replace(':',' '), lastDate, result, comptia, lastTime);
                                string outDirPath = Path.GetFullPath(Common.currentDirectory + "\\Resources\\ManualSearch\\" + lastDate);
                                program.SetOutputDirectoryPath(outDirPath);
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

            RenameSerials();

        }


        private void RenameSerials()
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string today = Common.GetDate();
            foreach (ProgramObjs f in programObjs)
            {
                
                string serial = f.GetSerialNum();
                //Console.WriteLine(serial);
                string lastDate = f.GetLastDate();
                string inDirPath = dailyRunData + "\\" + lastDate;
                string outDirPath = Path.GetFullPath(Common.currentDirectory + "\\Resources\\ManualSearch\\" + lastDate);
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

        public override void GetModelImages(ProgramObjs program, string date, int type)
        {
            throw new NotImplementedException();
        }

        public override void GetAllModelImages(string date, int type)
        {
            throw new NotImplementedException();
        }

        public override void GetLintLogicImages(string date)
        {
            throw new NotImplementedException();
        }
    }

}


