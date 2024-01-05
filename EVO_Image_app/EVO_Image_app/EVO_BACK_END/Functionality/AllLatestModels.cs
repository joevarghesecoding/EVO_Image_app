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
        string today = Common.GetDate();

        public AllLatestModels() : base() { }


        /// <summary>
        /// Gets all the images for the serial numbers and creates a folder for it.
        /// </summary>
        /// <return>
        /// A directory with all the latest phone images.
        /// </return>
        override public void GetLatestImages()
        {
            string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            //string dailyRunData = Common.currentDirectory + "\\Resources";
            programObjs = GetCurrentPrograms();
            GetLatestSerials(programObjs);
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

        public override void GetModelImages(ProgramObjs program, string date, int type)
        {
            throw new NotImplementedException();
        }

        public override void GetImagesForSerial(string serial)
        {
            throw new NotImplementedException();
        }


        private void GetLatestSerials(List<ProgramObjs> programObjs)
        {
            List<FileInfo> fatSatFiles = GetAllFatSatFiles();

            foreach (FileInfo file in fatSatFiles)
            {
                FindSerials(programObjs, file);
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
            var lines = File.ReadAllLines(fullPath).Reverse();
            foreach (ProgramObjs program in programObjs)
            {
                foreach (string line in lines)
                {
                    if (line.Contains(program.GetModelAndColor()))
                    {
                        if (program.GetSerialNum() == "")
                        {
                            string[] splitted = line.Split(',');
                            program.SetSerialNum(splitted[3]);
                            string[] dateSplit = splitted[0].Split(' ');
                            string date = dateSplit[0].Replace("/", "-");
                            program.SetLastDate(date);
                            program.SetComptia(splitted[4] + "," + splitted[5] + "," + splitted[6]);
                            program.SetLastTime(dateSplit[1] + " " + dateSplit[2]);
                        }

                    }
                }
            }
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
