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
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
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
                                string[] splitted = line.Split(',');
                                string[] dateSplit = splitted[0].Split(' ');
                                ProgramObjs program = new ProgramObjs(splitted[1] + ',' + splitted[2], serial, dateSplit[0].Replace("/", "-"), splitted[4] + "," + splitted[5] + "," + splitted[6]);
                                programObjs.Add(program);

                                if (program != null)
                                {
                                    string today = Common.GetDate();

                                    string date = program.GetLastDate();
                                    string inDirPath = dailyRunData + "\\" + date;
                                    string outDirPath = Common.currentDirectory + "\\Resources\\ManualSearch\\" + today;

                                    string fileName = serial;
                                    if (date != "" && date != "iPhone")
                                    {
                                        Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                                    }
                                    break;
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
