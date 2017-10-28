using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 500000; i++)
            {
                teller = 0;
                teller += i;
                printscreen();
            }
        }

        private static int teller;
        private static void printscreen()
        {
            Thread.Sleep(2500);
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save("D:/keylogger/screenshot/cap" + teller + ".jpg", ImageFormat.Jpeg);


            }
        }
    }
}
