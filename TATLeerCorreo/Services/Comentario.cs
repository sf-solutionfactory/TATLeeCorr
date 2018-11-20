using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TATLeerCorreo.Services
{
    class Comentario
    {
        public string getComment(string c, string contType)
        {

            string bodyHtml = "";
            //AE.Net.Mail.MailMessage mm = mx[i];
            if (contType != "text /plain" & contType != "text/plain")
            {
                string[] bodyH = c.Split(new string[] { "<div class=\"WordSection1\">" }, StringSplitOptions.None);
                if (bodyH.Length > 1)
                {
                    string[] bb = bodyH[1].Split(new string[] { "<img" }, StringSplitOptions.None);
                    if (bb.Length > 1)
                    {
                        bodyHtml = bb[0] + "</div>";
                        bodyHtml = bodyHtml.Replace("\r", "").Replace("\n", "").Replace("&nbsp;", "");
                    }
                    else
                    {
                        bb = bodyH[1].Split(new string[] { "</div" }, StringSplitOptions.None);
                        if (bb.Length > 1)
                        {
                            bodyHtml = bb[0] + "</div>";
                            bodyHtml = bodyHtml.Replace("\r", "").Replace("\n", "").Replace("&nbsp;", ""); ;
                        }
                    }
                }
                else
                {
                    bodyH = c.Split(new string[] { "<body>" }, StringSplitOptions.None);
                    if (bodyH.Length > 1)
                    {
                        string[] bb = bodyH[1].Split(new string[] { "<div class=\"acompli_signature\">" }, StringSplitOptions.None);
                        if (bb.Length > 1)
                        {
                            bodyHtml = bb[0] + "</div></div>";
                            bodyHtml = bodyHtml.Replace("\r", "").Replace("\n", "").Replace("&nbsp;", "");
                        }
                    }
                }
            }
            else
            {

                bodyHtml = c;
            }
            return bodyHtml;
        }
    }
}
