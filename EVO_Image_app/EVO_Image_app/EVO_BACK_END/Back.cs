using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END
{
    class Back : Sides
    {
        private string[,] regions = new string[16, 3];

        public Back(string Image, string File) : base(Image, File) { }
    }
}
