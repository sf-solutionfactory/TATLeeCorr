using AE.Net.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TATLeerCorreo.Entities;

namespace TATLeerCorreo.Services
{
    public class Log
    {
        public void escribeLog(string text)
        {
            //File.OpenWrite(DateTime.Now.ToShortDateString() + ".txt");
            using (StreamWriter w = File.AppendText("LOG/" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + ".txt"))
            {
                w.WriteLine(DateTime.Now.ToString() + "-" + text);
            }
        }

    }
}
