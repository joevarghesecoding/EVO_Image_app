using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    class Functions
    {
        protected List<ProgramObjs> programObjs;

        public Functions()
        {
            programObjs = new List<ProgramObjs>();
        }
        public List<ProgramObjs> GetProgramObjs()
        {
            return programObjs;
        }


    }
}
