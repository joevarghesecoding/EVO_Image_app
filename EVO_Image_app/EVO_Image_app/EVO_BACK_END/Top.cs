using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END
{
    class Top : Sides
    {
        private string[,] regions = new string[4, 3];

        public Top(string Image, string File) : base(Image, File) { }

        public override string[] GetDefectCount()
        {
            string[] result = new string[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = regions[i, 2];
            }

            return result;
        }

        public override string[] GetHighestDefect()
        {
            string[] result = new string[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = regions[i, 1];
            }

            return result;
        }

        public override string[] GetRegions()
        {
            string[] result = new string[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = regions[i, 0];
            }

            return result;
        }

        public override void ReadFile()
        {
            throw new NotImplementedException();
        }
    }
}
