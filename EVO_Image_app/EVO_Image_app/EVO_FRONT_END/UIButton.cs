using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVO_Image_app.EVO_FRONT_END
{
    public class UIButton : Button
    {
        public UIButton(System.Drawing.Image buttonImage)
        {
            FlatStyle = FlatStyle.Flat;
            Image = buttonImage;
            Text = "";

             
        }
    }
}
