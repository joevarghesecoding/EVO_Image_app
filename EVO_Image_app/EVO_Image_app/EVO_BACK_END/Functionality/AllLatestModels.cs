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
            programObjs = Common.GetCurrentPrograms();
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
            List<FileInfo> fatSatFiles = Common.GetAllFatSatFiles();
            
            foreach (FileInfo file in fatSatFiles)
            {
                Common.FindSerials(programObjs, file);
            }
           
            
        }
    }
}
