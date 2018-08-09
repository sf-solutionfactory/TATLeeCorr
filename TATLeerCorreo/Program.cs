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
            LeerCorreos lc = new LeerCorreos();
            lc.correos2();
            //lc.correo();
            //Console.Write("Terminar?");
            //Console.ReadKey();
        }
    }
}
