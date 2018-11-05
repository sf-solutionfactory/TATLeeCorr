using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TATLeerCorreo.Services;

namespace TATLeerCorreo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(100);
            //LeerCorreos lc = new LeerCorreos();
            var timer = new System.Threading.Timer((e) =>
            {
                LeerCorreos lc = new LeerCorreos();
                lc.correos2();
            }, null, startTimeSpan, periodTimeSpan);
            //lc.correos2();
            Console.Write("Terminar?");
            Console.ReadKey();
        }
    }
}
