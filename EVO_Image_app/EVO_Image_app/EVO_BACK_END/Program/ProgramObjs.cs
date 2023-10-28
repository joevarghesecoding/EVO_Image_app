namespace EVO_Image_app.EVO_BACK_END
{
    internal class ProgramObjs
    {
        private string ModelAndColor;
        private string SerialNum;
        private string LastDate;
        private string Comptia;
        private string LastTime;
        private string Result;

        public ProgramObjs()
        {
            this.ModelAndColor = "";
            this.SerialNum = "";
            this.LastDate = "";
            this.LastTime = "";
            this.Comptia = "";
            this.Result = ""; 
        }
        public ProgramObjs(string ModelAndColor)
        {
            this.ModelAndColor = ModelAndColor;
            this.SerialNum = "";
            this.LastDate = "";
            this.LastTime = "";
            this.Comptia = "";
            this.Result = "";
        }

        public ProgramObjs(string ModelAndColor, string SerialNum, string LastDate, string Result, string Comptia, string LastTime)
        {
            this.ModelAndColor = ModelAndColor;
            this.SerialNum = SerialNum;
            this.LastDate = LastDate;
            this.LastTime = LastTime;
            this.Result = Result;
            this.Comptia = Comptia;
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
        public string GetComptia()
        {
            return this.Comptia;
        }
        public void SetComptia(string comptia)
        {
            this.Comptia = comptia;
        }
        public string GetResult()
        {
            return this.Result;
        }
        public void SetResult(string Result)
        {
            this.Result = Result;
        }
        public void SetLastTime(string LastTime)
        {
            this.LastTime = LastTime;
        }
        public string GetLastTime()
        {
            return this.LastTime;
        }
    }
}