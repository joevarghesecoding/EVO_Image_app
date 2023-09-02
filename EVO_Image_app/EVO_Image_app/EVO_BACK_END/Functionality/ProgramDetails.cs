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
        

    }
}
