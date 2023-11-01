using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    abstract class Functions
    {
        protected List<ProgramObjs> programObjs;

        protected List<ProgramObjs> foundPrograms;

        public Functions()
        {
            foundPrograms = new List<ProgramObjs>();
        }
        public List<ProgramObjs> GetProgramObjs()
        {
            return programObjs;
        }

        public List<ProgramObjs> GetFoundPrograms()
        {
            return foundPrograms;
        }
        public void SetProgramObjs(ProgramObjs programObj)
        {
            foundPrograms.Add(programObj);
        }

        public abstract void GetLatestImages();
        public abstract void GetImagesForSerial(string serial);
        public abstract void GetModelImages(ProgramObjs program, string date, int type);
        public abstract void GetAllModelImages(string date);

        /// <summary>
        /// Finds all file names of FAT-SAT files
        /// </summary>
        /// <returns>List of FAT SAT files</returns>
        public List<FileInfo> GetAllFatSatFiles()
        {

            string path = "C:\\EVO-3\\Save Data\\Logs\\FAT-SAT\\";
            //string path = currentDirectory + "\\Resources\\FAT-SAT\\";

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToArray();
            List<FileInfo> result = files.ToList<FileInfo>();

            string auditPath = "C:\\EVO-3\\Save Data\\Logs\\Audit_FAT-SAT\\";
            DirectoryInfo auditDir = new DirectoryInfo(auditPath);
            List<FileInfo> auditFiles = dir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();
            foreach (FileInfo fileInfo in auditFiles)
            {
                result.Add(fileInfo);
            }
            return result;
        }

        /// <summary>
        /// Gets a list of all current programs from the colors_program.xml file
        /// </summary>
        /// <returns></returns>
        public List<ProgramObjs> GetCurrentPrograms()
        {
            List<ProgramObjs> objs = new List<ProgramObjs>();
            string cpPath = "c:\\EVO-3\\Parameters\\color_programs.xml";
            //string cpPath = currentDirectory + "\\Resources\\color_programs.xml";
            try
            {
                using (XmlReader reader = XmlReader.Create(cpPath))
                {
                    reader.ReadToFollowing("ModelPrograms");
                    string currentModel = "";
                    while (reader.Read())
                    {

                        string currentModelAndColor = "";

                        if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Text)
                        {
                            if (reader.Value.Contains("iPhone"))
                            {
                                currentModel = reader.Value;
                            }
                            if (!reader.Name.Contains("ModelPrograms") && !reader.Name.Contains("audit") && !reader.Name.Contains("model") && !string.IsNullOrWhiteSpace(reader.Name)
                                    && !reader.Name.Contains("large") && !reader.Name.Contains("small"))
                            {
                                char[] color = reader.Name.ToCharArray();
                                color[0] = char.ToUpper(color[0]);
                                string colorUpper = new string(color);
                                if (colorUpper.Equals("Gray"))
                                {
                                    colorUpper = "Space Gray";
                                }
                                currentModelAndColor = currentModel + "," + colorUpper;
                                objs.Add(new ProgramObjs(currentModelAndColor));
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" ***** ERROR At GetCurrentProgram *****\n " + ex.Message);
            }

            return objs;
        }
    }
}
