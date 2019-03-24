using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringExtention.NewlineString
{
    /* constract a class invoid of pass to many params */
    internal class NewlineStringConverter
    {
        Font font;
        int limitWidth;
        string content;
        StringBuilder returnStringBuilder;

        public NewlineStringConverter(string content,Font font,int limitWidth)
        {
            this.content = RemoveNewlineCharacter(content);
            
            this.font = font;
            this.limitWidth = limitWidth;
            returnStringBuilder = new StringBuilder();
        }

        public string Convert()
        {
            if (IsValid())
            {
                ProcessContent();
            }

            return returnStringBuilder.ToString();
        }

        void ProcessContent()
        {
            if (IsNeedNewline())
            {
                for (int i = 1; i < content.Length; i++)
                {
                    if (CaculateStringLength(content.Substring(0, i + 1)) > limitWidth)
                    {
                        returnStringBuilder.Append(content.Substring(0, i));
                        returnStringBuilder.AppendLine();

                        content = content.Substring(i);
                        ProcessContent();
                    }
                }
            }
            else
                returnStringBuilder.Append(content);
        }

        private static string RemoveNewlineCharacter(string content)
        {
            return content.Replace("\r", "").Replace("\n", "");
        }

        bool IsValid()
        {
            if (font == null
                || string.IsNullOrEmpty(content)
                || CaculateStringLength(content[0].ToString()) > limitWidth)
                return false;

            return true;
        }

        bool IsNeedNewline()
        {
            if (CaculateStringLength(content) > limitWidth)
                return true;
            return false;
        }

        int CaculateStringLength(string caculateString)
        {
            return TextRenderer.MeasureText(caculateString,font).Width;
        }
    }
}
