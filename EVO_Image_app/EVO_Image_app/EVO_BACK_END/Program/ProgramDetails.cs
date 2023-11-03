using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END
{
    class ProgramDetails
    {
        public ProgramObjs ProgramObject { get; set; }
        
        public Sides[] sides { get; set; }

        public ProgramDetails()
        {
            this.sides = new Sides[6];
   
        }

        ///<summary>
        ///Gets Images and file names for the specific file.
        /// </summary>
        /// <param name="path">The path to the directory where the results are</param>
        /// 
        public void GetProgramDetails(string path)
        {
            Console.WriteLine("PATH: " + path);
           try
            {
                DirectoryInfo sourceInfo = new DirectoryInfo(path);
                //Console.WriteLine(sourceInfo.FullName);
                if (!sourceInfo.Exists)
                {
                    //Console.WriteLine(sourceInfo.FullName);
                    Console.WriteLine("***** GetProgramDetails missing File *****\n");

                }
                else
                {
                    FileInfo[] files = sourceInfo.GetFiles();
                    this.ProgramObject = new ProgramObjs(sourceInfo.Name);

                    string frontImage, frontFile, backImage, backFile, leftImage, leftFile,
                    rightImage, rightFile, bottomImage, bottomFile, topImage, topFile;
                    frontImage = frontFile = backImage = backFile = leftImage = leftFile =
                    rightImage = rightFile = bottomImage = bottomFile = topImage = topFile = "";
                    foreach (FileInfo file in files)
                    {
                        if (file.Name.Contains("DSP") || file.Name == "Display.jpg")
                            frontImage = file.FullName;
                        else if (file.Name.Contains("HSG") || file.Name == "Housing.jpg")
                            backImage = file.FullName;
                        else if (file.Name.Contains("LFT") || file.Name == "Left.jpg")
                            leftImage = file.FullName;
                        else if (file.Name.Contains("RHT") || file.Name == "Right.jpg")
                            rightImage = file.FullName;
                        else if (file.Name.Contains("TOP") || file.Name == "Top.jpg")
                            topImage = file.FullName;
                        else if (file.Name.Contains("BTM") || file.Name == "Bottom.jpg")
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

                    this.sides[0] = front;
                    this.sides[1] = back;
                    this.sides[2] = top;
                    this.sides[3] = bottom;
                    this.sides[4] = left;
                    this.sides[5] = right;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("****** CopyResultsToDirectoryHelper ERROR ******\n " + ex.Message);
            }

        }


    }
}
