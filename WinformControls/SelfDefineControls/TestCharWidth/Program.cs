using StringExtention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCharWidth
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLen("一");
            ConsoleLen("事");
            ConsoleLen("a");
            ConsoleLen("1");
            ConsoleLen(",");
            ConsoleLen("，");

            TestNewlineMethod();

            Console.ReadKey();
        }

        static void ConsoleLen(string content)
        {
            Console.WriteLine(content + " ：" + TextRenderer.MeasureText(content, Control.DefaultFont));
        }

        static void TestNewlineMethod()
        {
            string testContent = "sdasad爱神的箭阿三客户等级啊可是大家开始搭建啊手机打开激动撒看见很多撒撒娇电话卡票iOS大批";
            Console.WriteLine("newLine --> \r\n" + testContent.ConvertToStringCanFillInWidth(Control.DefaultFont,20));
        }
    }
}
