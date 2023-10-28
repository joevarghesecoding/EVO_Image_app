using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using EVO_Image_app.EVO_BACK_END.Functionality;

namespace EVO_Image_app.EVO_BACK_END
{
    class Common
    {
        public static string currentDirectory = Environment.CurrentDirectory;

        ///<summary> 
        ///Get's today's date and formats it to M-D-YYYY
        ///</summary>
        ///<returns>(String) Today's date</returns>
        public static string GetDate()
        {
            DateTime today = DateTime.Today;
            string formattedDate = today.ToString("M-d-yyyy");
            return formattedDate;
        }

        ///<summary> 
        ///Formats the date from string
        ///</summary>
        ///<returns>Date in correct format</returns>
        public static string FormatDate(DateTime date)
        {
            string formattedDate = date.ToString("M-d-yyyy");
            return formattedDate;
        }

        ///<summary>
        ///Makes a folder if there isn't one, copies images from source to destination
        /// </summary>
        /// <param name="inputSerial">Serial #</param>
        /// <param name="inputPath">Path of the Serial # folder from Daily Run Data</param>
        /// <param name="outputPath">Path of newly copied images</param>
        /// <returns>Makes a folder if there isnt one, copies images from source to destination</returns>
        public static void CopyResultsToDirectory(string inputSerial, string inputPath, string outputPath, string fileName)
        {
            if (Directory.Exists(outputPath))
            {
               // Console.WriteLine("Folder Already Exists");
                CopyResultsToDirectoryHelper(inputSerial, inputPath, outputPath, fileName);
            } else
            {
                try
                {
                    Directory.CreateDirectory(outputPath);
                    CopyResultsToDirectoryHelper(inputSerial, inputPath, outputPath, fileName);

                } catch (Exception Ex)
                {
                    Console.WriteLine("***** ERROR AT COPY RESULTS TO DIRECTORY *****\n" + Ex.Message);
                }
            }
        }

        private static void CopyResultsToDirectoryHelper(string inputSerial, string inputPath, string outputPath, string fileName)
        {
            string source = inputPath + "\\" + inputSerial;
            string destination = outputPath + "\\" +fileName;
            //Console.WriteLine(source);
            try
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(source);
               
                if(!sourceInfo.Exists)
                {
                    //Console.WriteLine(sourceInfo.FullName);
                    Console.WriteLine("***** Source Info missing COPY HELPER *****\n");
                    return;
                }

                DirectoryInfo destinationInfo = new DirectoryInfo(destination);

                if (!destinationInfo.Exists)
                {
                    destinationInfo.Create();
                }

                FileInfo[] files = sourceInfo.GetFiles();

                foreach(FileInfo file in files)
                {
                    string desFilePath = Path.Combine(destinationInfo.FullName, file.Name);
                    file.CopyTo(desFilePath, true);
                }

            } catch (Exception ex)
            {
                Console.WriteLine("****** CopyResultsToDirectoryHelper ERROR ******\n " + ex.Message);
            }
        }

      
        ///<summary>
        ///Reads the CSV file and displays data to the correct
        /// </summary>
        /// <param name="programDetails">Program Details generated</param>
        /// <param name="side"># corresponding to side</param>
        public static void DisplayData(Sides side, System.Windows.Forms.DataGridView dataGridView)
        {
          
            side.ReadFile();
            string[] regions = side.GetRegions();
            string[] highest = side.GetHighestDefect();
            string[] count = side.GetDefectCount();

            dataGridView.Rows.Clear();
            for (int i = 0; i <regions.Length; i++)
            {
                string[] temp = { regions[i], highest[i], count[i] };
                dataGridView.Rows.Add(temp);
            }
        }

        /// <summary>
        /// Displays the serial number and date at the textboxes 
        /// </summary>
        /// <param name="programs"></param>
        /// <param name="programObjs"></param>
        /// <param name="serialNum"></param>
        /// <param name="lastDate"></param>

        public static void DisplaySerialAndDate(List<ProgramObjs> objs, ProgramObjs programObjs, System.Windows.Forms.TextBox serialNum, System.Windows.Forms.TextBox lastDate, System.Windows.Forms.TextBox comptia, System.Windows.Forms.TextBox regions)
        {
            foreach(ProgramObjs obj in objs)
            {
                if(obj.GetSerialNum() == programObjs.GetSerialNum() && obj.GetLastDate() == programObjs.GetLastDate() && obj.GetLastTime() == programObjs.GetLastTime())
                {
                    serialNum.Text = programObjs.GetSerialNum();
                    lastDate.Text = programObjs.GetLastDate() + " " + programObjs.GetLastTime();
                    string[] splitted = programObjs.GetComptia().Split(',');
                    if (splitted[0] == "PASS")
                    {
                        comptia.Text = "DEVICE GRADE: PASS";
                        regions.Text = "REGIONS : N/A";
                    }
                    else if (splitted[0] == "FAIL")
                    {
                        comptia.Text = "DEVICE GRADE : " + splitted[0] + "-" + splitted[1];
                        regions.Text = "REGIONS : " + splitted[2];
                    }

                }
            }
           
        }

        /// <summary>
        /// Deletes old runs saved images when starting the application.
        /// </summary>
        /// <param name="environment">Current environment the program is running in.</param>

        public static void DeleteOnStart(string environment, int flag)
        {
            string path = environment + "\\Resources\\"+ CurrentFlag(flag) + "\\";
            try
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                   // Console.WriteLine(directory);
                    Directory.Delete(directory, true);
                }
            } catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine("******* ERROR AT DeleteOnStart *******\n" + ex.Message);
            }
           
           
        }


        ///<summary>
        /// Sets the path of the out directory based on the currently running function
        /// </summary>
        /// <param name="flag">number indicating current function</param>
        public static string CurrentFlag(int flag)
        {
            string outDirPath;
            switch (flag)
            {
                case 1:
                    outDirPath = "AllLatestModels";
                    return outDirPath;
                case 2:
                    outDirPath = "ManualSearch";
                    return outDirPath;
                case 3:
                    outDirPath = "ModelSearch";
                    return outDirPath;
            }
            return null;
        }


        //public static void DisplayComptiaAndRegions(ProgramObjs current, System.Windows.Forms.TextBox comptia, System.Windows.Forms.TextBox regions)
        //{
           
        //    string[] splitted = current.GetComptia().Split(',');
        //    foreach(string split in splitted)
        //    {
        //        Console.WriteLine(split);
        //    }
        //    if (splitted[0] == "PASS")
        //    {
        //        comptia.Text = "PASS";
        //        regions.Text = "N/A";
        //    }
        //    else if (splitted[0] == "FAIL")
        //    {
        //        comptia.Text = splitted[0] + ":" + splitted[1];
        //        regions.Text = splitted[2];
        //    }
              
        //}
    }

}
