using StringExtention.NewlineString;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringExtention
{
    /* class name doesn't work,just be easy to understand */
    public static class Extender
    {
        public static string ConvertToStringCanFillInWidth(this string  content,Font font,int limitWidth)
        {
            return new NewlineStringConverter(content, font, limitWidth).Convert();
        }

        private static void RemoveNewlineCharacter(ref string content)
        {
            content = content.Replace("\r","").Replace("\n","");
        }

        private static void FindNewLinePos(string content,Font font,int limitWidth)
        {

        }
        
    }
}
