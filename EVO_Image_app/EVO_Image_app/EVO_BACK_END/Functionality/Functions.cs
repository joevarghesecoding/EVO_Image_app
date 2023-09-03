using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO_Image_app.EVO_BACK_END.Functionality
{
    abstract class Functions
    {
        protected List<ProgramObjs> programObjs;

        protected List<ProgramObjs> foundPrograms;

        public Functions()
        {
            programObjs = new List<ProgramObjs>();
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
        public abstract void GetModelImages(ProgramObjs program, string date);


    }
}
