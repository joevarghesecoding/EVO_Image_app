namespace EVO_Image_app.EVO_BACK_END
{
    internal class ProgramObjs
    {
        private string ModelAndColor;
        private string SerialNum;
        private string LastDate;

        public ProgramObjs(string ModelAndColor)
        {
            this.ModelAndColor = ModelAndColor;
            this.SerialNum = "";
            this.LastDate = "";
        }

        public ProgramObjs(string ModelAndColor, string SerialNum, string LastDate)
        {
            this.ModelAndColor = ModelAndColor;
            this.SerialNum = SerialNum;
            this.LastDate = LastDate;
        }
        public string GetModelAndColor()
        {
            return this.ModelAndColor;
        }

        public void SetSerialNum(string SerialNum)
        {
            this.SerialNum = SerialNum;
        }
        public void SetLastDate(string LastDate)
        {
            this.LastDate = LastDate;
        }
        public string GetSerialNum()
        {
            return this.SerialNum;
        }
        public string GetLastDate()
        {
            return this.LastDate;
        }
    }
}