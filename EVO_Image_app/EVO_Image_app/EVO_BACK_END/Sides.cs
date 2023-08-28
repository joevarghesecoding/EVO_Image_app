using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END
{
    class Sides
    {
        public string Image { get; set; }
        public string File { get; set; }
        public Sides (string Image, string File)
        {
            this.Image = Image;
            this.File = File;
        }
      

    }
}
