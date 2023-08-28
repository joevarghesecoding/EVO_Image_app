using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END
{
    class Back : Sides
    {
        private string[,] regions = new string[26, 3];

        public Back(string Image, string File) : base(Image, File) { }

        public override string[] GetDefectCount()
        {
            string[] result = new string[26];
            for(int i = 0; i < 26; i++)
            {
                result[i] = regions[i, 2];
            }

            return result;
        }

        public override string[] GetHighestDefect()
        {
            string[] result = new string[26];
            for (int i = 0; i < 26; i++)
            {
                result[i] = regions[i, 1];
            }

            return result;
        }

        public override string[] GetRegions()
        {
            string[] result = new string[26];
            for (int i = 0; i < 26; i++)
            {
                result[i] = regions[i, 0];
            }

            return result;
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
                        if (line != "Back" && line != "data_end" && line != "" && line.Contains("OCR"))
                        {
                            GetHighestValueAndCount(line, i);
                            i++;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("****** ERROR AT FRONT CLASS READ FILE*****\n" + e.StackTrace);
            }
        }


        private void GetHighestValueAndCount(string line, int i)
        {
            string[] splitted = line.Split(',');
            string highestVal = splitted[2].Trim('0');


            if (highestVal.Contains('+') || highestVal.Contains('.'))
            {
                for (int k = 0; k < splitted.Length; k++)
                {
                    if (splitted[k].Contains('+'))
                        splitted[k] = splitted[k].Trim('+');
                    if (splitted[k].Contains('.'))
                        splitted[k] = splitted[k].Trim('.');
                }
            }

            //Console.WriteLine(splitted[1]);
            if (highestVal == "")
            {
                regions[i, 0] = splitted[1];
                regions[i, 1] = "0";
                regions[i, 2] = "0";

            }
            else
            {
                regions[i, 0] = splitted[1];
                regions[i, 1] = highestVal;

                int count = 0;
                for (int j = 2; j < 10; j++)
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
        }
    }
    
}
