using System;
using System.IO;

namespace Transversal
{
    public static class Logger
    {
        public static void EscribirLogger(string lines)
        {
            string path = @"D:\LOG\AdminCH\";
            string fileName = string.Format("LOG_APP_ADMIN_CH_{0:yyyyMMdd}.txt", DateTime.Now);

            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
