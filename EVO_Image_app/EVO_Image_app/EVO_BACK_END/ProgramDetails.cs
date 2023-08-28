using System;
using System.Collections.Generic;
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
        

        //public string FrontImage { get; set; }
        //public string BackImage { get; set; }
        //public string TopImage { get; set; }
        //public string BottomImage { get; set; }
        //public string LeftImage { get; set; }
        //public string RightImage { get; set; }
        //public string BackFile { get; set; }
        //public string FrontFile { get; set; }
        //public string LongFile { get; set; }
        //public string ShortFile { get; set; }

    }
}
