using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EVO_Image_app.EVO_BACK_END
{
    class AllLatestModels
    {
        public static List<ProgramObjs> programObjs; 
        string today = Common.GetDate();
        
        /// <summary>
        /// Gets all the images for the serial numbers and creates a folder for it.
        /// </summary>
        /// <return>
        /// A directory with all the latest phone images.
        /// </return>
        public static void GetLatestImages()
        {
            //string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string dailyRunData = Common.currentDirectory + "\\Resources";
            programObjs = Common.GetCurrentPrograms();
            GetLatestSerials(programObjs);
            string today = Common.GetDate();


            foreach (ProgramObjs obj in programObjs)
            {
                string serial = obj.GetSerialNum();
                string date = obj.GetLastDate();
                string inDirPath = dailyRunData + "\\" + date;
                //string outDirPath = "C:\\Users\\Joe.Varghese\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today;
                //string outDirPath = "C:\\Users\\Administrator\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\bin\\Debug\\Resources\\AllLatestModels\\" + today;
                string outDirPath = Common.currentDirectory + "\\Resources\\AllLatestModels\\" + today;

                string fileName = obj.GetModelAndColor();
                if (date != "" && date != "iPhone")
                {
                    Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                }
            }
        }

        public static List<ProgramObjs> GetProgramObjs()
        {
            return programObjs;
        }
        
        private static void GetLatestSerials(List<ProgramObjs> programObjs)
        {
            List<FileInfo> fatSatFiles = Common.GetAllFatSatFiles();
            foreach(FileInfo file in fatSatFiles)
            {
                Common.FindSerials(programObjs, file);
            }
        }
    }
}
