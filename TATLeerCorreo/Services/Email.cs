using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using TATLeerCorreo.Entities;

namespace TATLeerCorreo.Services
{

    class Email
    {
        private TAT001Entities db = new TAT001Entities();
        Log log = new Log();

        public void enviarCorreo(decimal nd, int c, int pos)
        {
            try
            {
                ////var workflow = db.FLUJOes.Where(a => a.NUM_DOC.Equals(nd) && a.POS == pos).OrderByDescending(a => a.POS).FirstOrDefault();
                var workflow = db.FLUJOes.Where(a => a.NUM_DOC.Equals(nd)).OrderByDescending(a => a.POS).FirstOrDefault();
                APPSETTING mailtC = db.APPSETTINGs.Where(x => x.NOMBRE.Equals("mail") && x.ACTIVO).FirstOrDefault();
                string mailt = mailtC.VALUE;
                APPSETTING mailTestC = db.APPSETTINGs.Where(x => x.NOMBRE.Equals("mailTest") && x.ACTIVO).FirstOrDefault();
                string mtest = mailTestC.VALUE;
                string mailTo = "";
                if (mtest == "X")
                    mailTo = "rogelio.sanchez@sf-solutionfactory.com";
                else
                    mailTo = workflow.USUARIO.EMAIL;
                CONMAIL conmail = db.CONMAILs.Find(mailt);
                if (conmail != null)
                {
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(conmail.MAIL, mailTo);
                    SmtpClient client = new SmtpClient();
                    if (conmail.SSL)
                    {
                        client.Port = (int)conmail.PORT;
                        client.EnableSsl = conmail.SSL;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(conmail.MAIL, conmail.PASS);
                    }
                    else
                    {
                        client.UseDefaultCredentials = true;
                        client.Credentials = new NetworkCredential(conmail.MAIL, conmail.PASS);
                    }
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Host = conmail.HOST;

                    APPSETTING urlC = db.APPSETTINGs.Where(x => x.NOMBRE.Equals("url") && x.ACTIVO).FirstOrDefault();
                    string cadUrl = urlC.VALUE;
                    string UrlDirectory = "";
                    if (c == 1)
                    {
                        UrlDirectory = cadUrl + "Correos/Index/" + nd + "?spras=" + workflow.USUARIO.SPRAS_ID;
                        ////mail.Subject = "Aprobado";
                        mail.Subject = "A" + nd + "-" + DateTime.Now.ToShortTimeString();
                    }
                    if (c == 3)
                    {
                        UrlDirectory = cadUrl + "Correos/Details/" + nd + "?spras=" + workflow.USUARIO.SPRAS_ID;
                        ////mail.Subject = "Rechazado";
                        mail.Subject = "R" + nd + "-" + DateTime.Now.ToShortTimeString();
                    }
                    WebRequest myRequest = WebRequest.Create(UrlDirectory);
                    myRequest.Method = "GET";
                    myRequest.ContentType = "application/x-www-form-urlencoded";
                    // Set credentials to use for this request.
                    myRequest.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse myResponse = myRequest.GetResponse();
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                    string result = sr.ReadToEnd();
                    sr.Close();
                    myResponse.Close();
                    mail.AlternateViews.Add(Mail_Body(result, "MX"));//B20180803 MGC Correos
                    mail.IsBodyHtml = true;
                    //mail.Body = result;
                    client.Send(mail);
                }
            }
            catch (Exception ex)
            {
                log.escribeLog("ERROR - " + ex.InnerException.ToString());
                ////throw new Exception("No se ha podido enviar el email", ex.InnerException);
            }
        }

        private AlternateView Mail_Body(string strr, string pais)
        {
            ImageConverter ic = new ImageConverter();
            Bitmap b = new Bitmap(Properties.Resources.logo_kellogg);
            Byte[] ba = (Byte[])ic.ConvertTo(b, typeof(Byte[]));
            MemoryStream logo = new MemoryStream(ba);

            Bitmap b1 = new Bitmap((Bitmap)Properties.Resources.ResourceManager.GetObject(pais.ToLower()));
            Byte[] ba1 = (Byte[])ic.ConvertTo(b1, typeof(Byte[]));
            MemoryStream pai = new MemoryStream(ba1);

            LinkedResource Img = new LinkedResource(logo, MediaTypeNames.Image.Jpeg);
            LinkedResource Img2 = new LinkedResource(pai, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "logo_img";
            Img2.ContentId = "flag_img";

            strr = strr.Replace("\"miimg_id\"", "cid:logo_img");
            strr = strr.Replace("\"miflag_id\"", "cid:flag_img");

            AlternateView AV = AlternateView.CreateAlternateViewFromString(strr, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            AV.LinkedResources.Add(Img2);
            return AV;
        }

    }
}
