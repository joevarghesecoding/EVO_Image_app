using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EVO_Image_app.EVO_BACK_END
{
    class Common
    {

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
        ///Gets Images and file names for the specific file.
        /// </summary>
        /// <param name="path">The path to the directory where the results are</param>
        /// 
        public static ProgramDetails GetProgramDetails(string path)
        {
            ProgramDetails programDetails = new ProgramDetails();
            try
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(path);
                Console.WriteLine(sourceInfo.FullName);
                if (!sourceInfo.Exists)
                {
                    //Console.WriteLine(sourceInfo.FullName);
                    Console.WriteLine("***** GetProgramDetails missing File *****\n");
                    
                }
                else
                {
                        FileInfo[] files = sourceInfo.GetFiles();
                        ProgramObjs temp2 = new ProgramObjs(sourceInfo.Name);
                        programDetails.ProgramObject = temp2;
                        string frontImage, frontFile, backImage, backFile, leftImage, leftFile, 
                        rightImage, rightFile, bottomImage, bottomFile, topImage, topFile;
                        frontImage = frontFile = backImage = backFile = leftImage = leftFile =
                        rightImage = rightFile = bottomImage = bottomFile = topImage = topFile = "";
                    foreach (FileInfo file in files)
                        {
                            if (file.Name == "Display.jpg")
                                frontImage = file.FullName;
                            else if (file.Name == "Housing.jpg")
                                backImage = file.FullName;
                            else if (file.Name == "Left.jpg")
                                leftImage = file.FullName;
                            else if (file.Name == "Right.jpg")
                                rightImage = file.FullName;
                            else if (file.Name == "Top.jpg")
                                topImage = file.FullName;
                            else if (file.Name == "Bottom.jpg")
                                bottomImage = file.FullName;
                            else if (file.Name == "Back.csv")
                                backFile = file.FullName;
                            else if (file.Name == "Front.csv")
                                frontFile = file.FullName;
                            else if (file.Name == "Long.csv")
                            {
                                leftFile = file.FullName;
                                rightFile = file.FullName;
                            }
                            else if (file.Name == "Short.csv")
                            {
                                topFile = file.FullName;
                                bottomFile = file.FullName;
                            }
                        }

                        Front front = new Front(frontImage, frontFile);
                        Back back = new Back(backImage, backFile);
                        Top top = new Top(topImage, topFile);
                        Bottom bottom = new Bottom(bottomImage, bottomFile);
                        Left left = new Left(leftImage, leftFile);
                        Right right = new Right(rightImage, rightFile);

                    programDetails.sides[0] = front;
                    programDetails.sides[1] = back;
                    programDetails.sides[2] = top;
                    programDetails.sides[3] = bottom;
                    programDetails.sides[4] = left;
                    programDetails.sides[5] = right;
                    
                   
                        //Console.WriteLine(programDetails.BackFile);
                        //Console.WriteLine(programDetails.FrontFile);
                        //Console.WriteLine(programDetails.LeftImage);
                        //Console.WriteLine(programDetails.BottomImage);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("****** CopyResultsToDirectoryHelper ERROR ******\n " + ex.Message);
            }

            return programDetails;
        }

        ///<summary>
        ///Reads the CSV file and displays data to the correct
        /// </summary>
        /// <param name="programDetails">Program Details generated</param>
        /// <param name="side"># corresponding to side</param>
        public static void DisplayData(ProgramDetails programDetails, Sides side, System.Windows.Forms.DataGridView dataGridView)
        {
          
            side.ReadFile();
            string[] regions = side.GetRegions();
            string[] highest = side.GetHighestDefect();
            string[] count = side.GetDefectCount();

            dataGridView.Rows.Clear();
            for (int i = 0; i < 16; i++)
            {
                string[] temp = { regions[i], highest[i], count[i] };
                dataGridView.Rows.Add(temp);
            }
        }
    }

}
