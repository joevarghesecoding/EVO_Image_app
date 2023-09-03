using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    class ModelSearch : Functions
    {
        public ModelSearch() : base() 
        {
            programObjs = Common.GetCurrentPrograms();
        }


        public override void GetModelImages(ProgramObjs program, string date)
        {
            string dailyRunData = Common.currentDirectory + "\\Resources";
            string today = Common.GetDate();
            try
            {
                FileInfo file = Common.GetFatSatFile(date);
                if(file != null)
                {
                    FindSerialCount(program, file);
                    foreach (ProgramObjs obj in foundPrograms)
                    {
                        string serial = obj.GetSerialNum();
                        string lastDate = obj.GetLastDate();
                        string inDirPath = dailyRunData + "\\" + date;
                        string outDirPath = Common.currentDirectory + "\\Resources\\ModelSearch\\" + today;

                        string fileName = obj.GetSerialNum() + "," + obj.GetModelAndColor();
                        if (date != "" && date != "iPhone")
                        {
                            Common.CopyResultsToDirectory(serial, inDirPath, outDirPath, fileName);
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No Data found for Date");
                }
            } catch(NullReferenceException ex)
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Date");
                Console.WriteLine("******* ERROR AT GetModelImages ******\n" + ex.Message);
            }
            
        }

        private void FindSerialCount(ProgramObjs program, FileInfo file)
        {
            if(file != null)
            {
                string fullPath = Environment.CurrentDirectory + "\\Resources\\FAT-SAT\\" + file.Name;
                try
                {
                    using(StreamReader reader = new StreamReader(fullPath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(program.GetModelAndColor()))
                            {
                                string[] splitted = line.Split(',');
                                string serial = splitted[3];
                                string[] dateSplit = splitted[0].Split(' ');
                                string date = dateSplit[0].Replace("/", "-");
                                string comptia = splitted[4] + "," + splitted[5] + "," + splitted[6];
                                ProgramObjs temp = new ProgramObjs(program.GetModelAndColor(), serial, date, comptia);
                                foundPrograms.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***** ERROR At FindSerialCount ******\n" + ex.Message);
                }
            }
        }

        public override void GetImagesForSerial(string serial)
        {
            throw new NotImplementedException();
        }

        public override void GetLatestImages()
        {
            throw new NotImplementedException();
        }
    }
}
