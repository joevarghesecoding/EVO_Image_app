using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END
{
    class Front : Sides
    {
        private string[,] regions = new string[16, 3];

        public Front(string Image, string File) : base(Image, File) { }


        public override string[] GetRegions()
        {
            string[] OnlyRegion = new string[16];
            for (int i = 0; i < 16; i++)
            {
                OnlyRegion[i] = regions[i, 0];
            }

            return OnlyRegion;
        }

        public override string[] GetHighestDefect()
        {
            string[] OnlyHighest = new string[16];
           for(int i = 0; i < 16; i++)
            {
                OnlyHighest[i] = regions[i, 1];
            }

            return OnlyHighest;
        }

        public override string[] GetDefectCount()
        {
            string[] OnlyDefect = new string[16];

            for(int i = 0; i < 16; i++)
            {
                OnlyDefect[i] = regions[i, 2];
            }

            return OnlyDefect;
        }
        public override void ReadFile()
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(File))
                {
                    string line;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                       if(line != "Front" && line != "data_end" && line != "")
                       {
                            GetHighestValueAndCount(line, i);
                            i++;
                       }
                    }
                }

            } catch (Exception e)
            {
                Console.WriteLine("****** ERROR AT FRONT CLASS READ FILE*****\n" + e.StackTrace);
            }
        }

        private void GetHighestValueAndCount(string line, int i)
        {

            if(line.Contains("Crack, D1"))
            {
                string[] splitted = line.Split(',');
                int count = 0;
                regions[i, 0] = splitted[0] + ',' + splitted[1];
                for(int j = 13; j < splitted.Length; j++)
                {
                    if(j == 13)
                    {
                        string highestVal = splitted[13].TrimStart('0');
                        regions[i, 1] = (highestVal == "") ? "0" : highestVal;
                    }
                    int res;
                    int.TryParse(splitted[j], out res);
                    if (res > 0)
                    {
                        count++;
                    }
                    if (res == 0)
                    {
                        break;
                    }
                }
                regions[i, 2] = count.ToString();
            }
            else
            {
                string pattern = "[+]";
                string resultLine = Regex.Replace(line, pattern, "");
                string[] splitted = resultLine.Split(',');

                string highestVal = splitted[2].TrimStart('0');

                regions[i, 0] = splitted[1];
                regions[i, 1] = (highestVal == "") ? "0" : highestVal;

                if (splitted.Length > 3)
                {
                    int count = 0;
                    for (int j = 2; j < splitted.Length; j++)
                    {
                        int res;
                        int.TryParse(splitted[j], out res);
                        if (res > 0)
                        {
                            count++;
                        }
                        if (res == 0)
                        {
                            break;
                        }
                    }
                    //Console.WriteLine(count);
                    regions[i, 2] = count.ToString();
                }
                else
                {
                    regions[i, 2] = "0";
                }
            }
            

           
        }
    }

    
}
