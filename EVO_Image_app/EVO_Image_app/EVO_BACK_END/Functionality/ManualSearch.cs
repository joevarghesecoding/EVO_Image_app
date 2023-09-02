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
        public void GetImagesForSerial(string serial)
        {
            //string dailyRunData = "C:\\EVO-3\\Save Data\\Daily Run Data";
            string dailyRunData = Common.currentDirectory + "\\Resources";
            List<FileInfo> fatSatPaths = Common.GetAllFatSatFiles();
            foreach(FileInfo fileInfo in fatSatPaths)
            {
                ProgramObjs tempObj = Common.FindProgramData(serial, fileInfo);
                programObjs.Add(tempObj);
                if (tempObj != null)
                {
                    string today = Common.GetDate();

                    string date = tempObj.GetLastDate();
                    string inDirPath = dailyRunData + "\\" + date;
                    //string outDirPath = "C:\\Users\\Joe.Varghese\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\Resources\\AllLatestModels\\" + today;
                    //string outDirPath = "C:\\Users\\Administrator\\Desktop\\EVO_Image_app\\EVO_Image_app\\EVO_Image_app\\bin\\Debug\\Resources\\AllLatestModels\\" + today;
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
