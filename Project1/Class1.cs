using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Project1
{
    class Programma
    {
         static void Main() {
            Process p = new Process();
           
            
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "netstat";
            //p.StartInfo.Arguments = "netstat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            System.IO.File.Delete("D:\\test1.txt");
            FileStream fstream = new FileStream("D:\\test1.txt",FileMode.OpenOrCreate);
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(output);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            fstream.Close();

            char[] charSeparators = new char[] { ' ' };
            string[] massiv_slov = output.Split(charSeparators);

            //for (int i=0; i<300;i++) { Console.WriteLine(massiv_slov[i]); }

            int i = 0;
            bool kluch = false;
            string status_connect = "net";
            while (i< massiv_slov.Length)
            {
                if (massiv_slov[i] == "lagom:25432") { kluch = true; }
                if ((kluch == true) & (massiv_slov[i] != "") & (massiv_slov[i] != "lagom:25432") & (massiv_slov[i] != " "))
                {
                    status_connect = massiv_slov[i];
                    //Console.WriteLine(kluch);
                   break;
                }
                i++;
            }
            status_connect = System.Text.RegularExpressions.Regex.Replace(status_connect, @"\s+", " ");

            //Console.WriteLine(status_connect);
            if (status_connect == "net" || String.Equals(status_connect,"CLOSE_WAIT ") || String.Equals(status_connect, "TIME_WAIT ") ) {
                //Console.WriteLine("1");
                System.Diagnostics.Process p1 = new System.Diagnostics.Process();
                p1.StartInfo.FileName = @"D:\start.bat";
                p1.Start();
            }
             
           // Console.ReadKey();


        }

    }
}
