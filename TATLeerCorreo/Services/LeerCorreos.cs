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
    public class LeerCorreos
    {
        private TAT001Entities db = new TAT001Entities();
        Log log = new Log();
        
        public void correos2()
        {
            ImapClient ic = new ImapClient();
            List<AE.Net.Mail.MailMessage> mx = new List<AE.Net.Mail.MailMessage>();
            List<AE.Net.Mail.MailMessage> emRq17 = new List<AE.Net.Mail.MailMessage>();
            APPSETTING lg = db.APPSETTINGs.Where(x => x.NOMBRE == "logPath" & x.ACTIVO == true).FirstOrDefault();
            log.ruta = lg.VALUE + "LeerCorreos_";
            log.escribeLog("-----------------------------------------------------------------------START");
            try
            {
                List<CONMAIL> cc = db.CONMAILs.ToList();
                CONMAIL conmail = cc.Where(x => x.ID == "LE").FirstOrDefault();
                if (conmail == null) { log.escribeLog("Falta configurar inbox."); return; }
                ic = new ImapClient(conmail.HOST, conmail.MAIL, conmail.PASS,
                                  AuthMethods.Login, (int)conmail.PORT, conmail.SSL);

                // Select a mailbox. Case-insensitive
                ic.SelectMailbox("INBOX");

                //Esto traera los emails recibidos y no leidos
                mx = ic.GetMessages(0, ic.GetMessageCount() - 1, false, false)
                                                .Where(m => !m.Flags.HasFlag(Flags.Seen) && !m.Flags.HasFlag(Flags.Deleted)).ToList();

                //En esta lista ingresaremos a los mails que sean recibidos como cc
                emRq17 = new List<AE.Net.Mail.MailMessage>();
                log.escribeLog("Leer inbox - numReg=(" + mx.Count + ")");
                
            }
            catch (Exception e)
            {
                mx = new List<AE.Net.Mail.MailMessage>();
                emRq17 = new List<AE.Net.Mail.MailMessage>();
                log.escribeLog("Error al leer");
            }
            try
            {
                //ingresamos los correos CORREO
                for (int i = 0; i < mx.Count; i++)
                {
                    AE.Net.Mail.MailMessage mm = mx[i];
                    try
                    {
                        string[] arrAsunto = mm.Subject.Split(']');
                        int a = arrAsunto.Length - 1;
                        //Recupero el asunto y lo separo del numdoc y pos
                        string[] arrAprNum = arrAsunto[a].Split('-');//RSG cambiar 0 a 1
                        string[] arrClaves = arrAprNum[1].Split('.');
                        //Valido que tenga los datos necesarios para el req 17
                        if (arrClaves.Length > 1)
                        {
                            decimal numdoc = Decimal.Parse(arrClaves[1]);
                        }
                        var xy = arrAprNum[0].Trim();
                        if (arrAprNum[0].Trim() == "De Acuerdo" | arrAprNum[0].Trim() == "DeAcuerdo")
                        {
                            emRq17.Add(mm);
                            log.escribeLog("NEGO A - " + arrClaves[1]);
                        }
                        else if (arrAprNum[0].Trim() == "Tengo Observaciones" | arrAprNum[0].Trim() == "TengoObservaciones")
                        {
                            emRq17.Add(mm);
                            log.escribeLog("NEGO O - " + arrClaves[1]);
                        }
                    }
                    catch
                    {
                        ic.AddFlags(Flags.Seen, mm);
                        log.escribeLog("ERROR - " + mm.Subject);
                    }
                }
                //Correos de FLUJO DE APROBACIÓN y RECURRENTE-----------------------------------------------------2 y 3
                if (true)
                {
                    for (int i = 0; i < mx.Count; i++)
                    {
                        AE.Net.Mail.MailMessage mm = mx[i];
                        string[] arrAsunto = mm.Subject.Split(']');
                        int a = arrAsunto.Length - 1;
                        //Recupero el asunto y lo separo del numdoc y pos
                        string[] arrAprNum = arrAsunto[a].Split('-');//RSG cambiar 0 a 1
                        string[] arrClaves = arrAprNum[1].Split('.');
                        decimal numdoc = Decimal.Parse(arrClaves[0]);
                        //Si el Texto es Aprobado, Rechazado o Recurrente
                        string[] arrApr = arrAprNum[0].Split(':');
                        if (arrApr.Length > 1)
                        {
                            ProcesaFlujo pF = new ProcesaFlujo();
                            if (arrApr[1] == "Approved" | arrApr[1] == "Rejected")
                            {
                                log.escribeLog("APPR AR - " + arrClaves[0]);
                                int pos = Convert.ToInt32(arrAprNum[2]);
                                FLUJO fl = db.FLUJOes.Where(x => x.NUM_DOC == numdoc && x.POS == pos).FirstOrDefault();
                                if (fl != null)
                                {
                                    Console.WriteLine(mm.From.Address.Trim()); Console.WriteLine(fl.USUARIO.EMAIL);
                                    log.escribeLog("APPR mails - " + mm.From.Address.Trim() + " == " + fl.USUARIO.EMAIL);
                                    if (mm.From.Address.Trim().ToLower() == fl.USUARIO.EMAIL.Trim().ToLower() | mm.From.Address.Trim().ToLower() == fl.USUARIO1.EMAIL.Trim().ToLower())
                                    {
                                        Console.WriteLine("true");
                                        fl.ESTATUS = arrApr[1].Substring(0, 1);
                                        fl.FECHAM = DateTime.Now;
                                        Comentario com = new Comentario();
                                        fl.COMENTARIO = com.getComment(mm.Body, mm.ContentType);
                                        //fl.COMENTARIO = mm.Body;
                                        //if (fl.COMENTARIO.Length > 255)
                                        //    fl.COMENTARIO = fl.COMENTARIO.Substring(0, 252) + "...";
                                        var res = pF.procesa(fl, "");
                                        log.escribeLog("APPR PROCESA - Res = " + res);
                                        if (res == "1")
                                        {
                                            enviarCorreo(fl.NUM_DOC, 1, pos);
                                        }
                                        else if (res == "3")
                                        {
                                            enviarCorreo(fl.NUM_DOC, 3, pos);
                                        }

                                        using (TAT001Entities db1 = new TAT001Entities())
                                        {
                                            FLUJO ff = db1.FLUJOes.Where(x => x.NUM_DOC == fl.NUM_DOC).OrderByDescending(x => x.POS).FirstOrDefault();
                                            Estatus es = new Estatus();//RSG 18.09.2018
                                            DOCUMENTO ddoc = db1.DOCUMENTOes.Find(fl.NUM_DOC);
                                            ff.STATUS = es.getEstatus(ddoc);
                                            db1.Entry(ff).State = System.Data.Entity.EntityState.Modified;
                                            db1.SaveChanges();
                                        }
                                        Console.WriteLine(res);
                                    }
                                }
                                ic.AddFlags(Flags.Seen, mm);
                            }
                            ////else if (arrApr[1] == "Rejected")
                            ////{
                            ////    int pos = Convert.ToInt32(arrAprNum[2]);
                            ////    FLUJO fl = db.FLUJOes.Where(x => x.NUM_DOC == numdoc && x.POS == pos).FirstOrDefault();
                            ////    fl.ESTATUS = "R";
                            ////    fl.FECHAM = DateTime.Now;
                            ////    fl.COMENTARIO = mm.Body;
                            ////    var res = pF.procesa(fl, "");
                            ////    if (res == "0")
                            ////    {
                            ////        //
                            ////    }
                            ////    else if (res == "1")
                            ////    {
                            ////        enviarCorreo(fl.NUM_DOC, 1);
                            ////    }
                            ////    else if (res == "3")
                            ////    {
                            ////        enviarCorreo(fl.NUM_DOC, 3);
                            ////    }
                            ////    //para marcar el mensaje como leido
                            ////    ic.AddFlags(Flags.Seen, mm);
                            ////}
                            else if (arrApr[1] == "Recurrent")
                            {
                                ////Reversa r = new Reversa();
                                ////string ts = db.DOCUMENTOes.Find(numdoc).TSOL.TSOLR;
                                ////int ret = 0;
                                ////if (ts != null)
                                ////    ret = r.creaReversa(numdoc.ToString(), ts);

                                //////para marcar el mensaje como leido
                                ////if (ret != 0)
                                ////    ic.AddFlags(Flags.Seen, mm);
                            }
                        }

                    }
                }
                //req17
                //FLUJNEGO fn = new FLUJNEGO();
                for (int i = 0; i < emRq17.Count; i++)
                {
                    AE.Net.Mail.MailMessage mm = emRq17[i];
                    if (true)
                    {
                        string[] arrAsunto = mm.Subject.Split(']');
                        int isa = arrAsunto.Length - 1;
                        //Recupero el asunto y lo separo del numdoc y pos
                        string[] arrAprNum = arrAsunto[isa].Split('-');
                        string[] arrPiNN = arrAprNum[1].Split('.');
                        var _id = int.Parse(arrPiNN[1]);
                        var vkorg = arrPiNN[2];
                        var _correo = arrPiNN[4].Replace('*', '.').Replace('+', '-').Replace('/', '@').Replace('#', '-'); ;
                        //recupero las fechas de envio
                        var _xres = db.NEGOCIACIONs.Where(x => x.ID == _id).FirstOrDefault();
                        var pid = arrPiNN[0];
                        // var fs = db.DOCUMENTOes.Where(f => f.PAYER_ID == pid && f.FECHAC.Value.Month == DateTime.Now.Month  && f.FECHAC.Value.Year == DateTime.Now.Year && f.DOCUMENTO_REF == null).ToList();
                        var _xff = _xres.FECHAF.AddDays(1);
                        var fs = db.DOCUMENTOes.Where(f => f.PAYER_ID == pid && f.VKORG == vkorg && f.PAYER_EMAIL == _correo && f.FECHAC >= _xres.FECHAI && f.FECHAC < _xff && f.DOCUMENTO_REF == null).ToList();
                        //LEJ 20.07.2018-----
                        var dOCUMENTOes = fs;
                        var lstD = new List<DOCUMENTO>();//---
                        DOCUMENTOA dz = null;//---
                        for (int y = 0; y < dOCUMENTOes.Count; y++)
                        {
                            //recupero el numdoc
                            var de = fs[i].NUM_DOC;
                            //sino ecuentra una coincidencia con el criterio discriminatorio se agregan o no a la lista
                            dz = db.DOCUMENTOAs.Where(x => x.NUM_DOC == de && x.CLASE != "OTR").FirstOrDefault();
                            if (dz == null || dz != null)
                            {
                                if (dOCUMENTOes[y].TSOL.NEGO == true)//para el ultimo filtro
                                {

                                    string estatus = "";
                                    if (dOCUMENTOes[y].ESTATUS != null) { estatus += dOCUMENTOes[y].ESTATUS; } else { estatus += " "; }
                                    if (dOCUMENTOes[y].ESTATUS_C != null) { estatus += dOCUMENTOes[y].ESTATUS_C; } else { estatus += " "; }
                                    if (dOCUMENTOes[y].ESTATUS_SAP != null) { estatus += dOCUMENTOes[y].ESTATUS_SAP; } else { estatus += " "; }
                                    if (dOCUMENTOes[y].ESTATUS_WF != null) { estatus += dOCUMENTOes[y].ESTATUS_WF; } else { estatus += " "; }
                                    if (dOCUMENTOes[y].FLUJOes.Count > 0)
                                    {
                                        estatus += dOCUMENTOes[y].FLUJOes.OrderByDescending(a => a.POS).FirstOrDefault().WORKFP.ACCION.TIPO;
                                    }
                                    else
                                    {
                                        estatus += " ";
                                    }
                                    if (dOCUMENTOes[y].TSOL.PADRE) { estatus += "P"; } else { estatus += " "; }
                                    if (dOCUMENTOes[y].FLUJOes.Where(x => x.ESTATUS == "R").ToList().Count > 0)
                                    {
                                        estatus += dOCUMENTOes[y].FLUJOes.Where(x => x.ESTATUS == "R").OrderByDescending(a => a.POS).FirstOrDefault().USUARIO.PUESTO_ID;
                                    }
                                    else
                                    {
                                        estatus += " ";
                                    }


                                    if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[P][R].."))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[R]..[8]"))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "[P]..[A]..."))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "..[P][A]..."))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "..[E][A]..."))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[A].[P]."))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[A]..."))
                                        lstD.Add(dOCUMENTOes[y]);
                                    else if (System.Text.RegularExpressions.Regex.IsMatch(estatus, "...[T]..."))
                                        lstD.Add(dOCUMENTOes[y]);

                                    //if (dOCUMENTOes[y].ESTATUS_WF == "P")//LEJ 20.07.2018---------------------------I
                                    //{
                                    //    if (dOCUMENTOes[y].FLUJOes.Count > 0)
                                    //    {
                                    //        if (dOCUMENTOes[y].FLUJOes.OrderByDescending(a => a.POS).FirstOrDefault().USUARIO != null)
                                    //        {
                                    //            //(Pendiente Validación TS)
                                    //            if (dOCUMENTOes[y].FLUJOes.OrderByDescending(a => a.POS).FirstOrDefault().USUARIO.PUESTO_ID == 8)
                                    //            {
                                    //                lstD.Add(dOCUMENTOes[y]);
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //else if (dOCUMENTOes[y].ESTATUS_WF == "R")//(Pendiente Corrección)
                                    //{
                                    //    if (dOCUMENTOes[y].FLUJOes.Count > 0)
                                    //    {
                                    //        lstD.Add(dOCUMENTOes[y]);
                                    //    }
                                    //}
                                    //else if (dOCUMENTOes[y].ESTATUS_WF == "T")//(Pendiente Taxeo)
                                    //{
                                    //    if (dOCUMENTOes[y].TSOL_ID == "NCIA")
                                    //    {
                                    //        if (dOCUMENTOes[y].PAIS_ID == "CO")//(sólo Colombia)
                                    //        {
                                    //            lstD.Add(dOCUMENTOes[y]);
                                    //        }
                                    //    }
                                    //}
                                    //else if (dOCUMENTOes[y].ESTATUS_WF == "A")//(Por Contabilizar)
                                    //{
                                    //    if (dOCUMENTOes[y].ESTATUS == "P")
                                    //    {
                                    //        lstD.Add(dOCUMENTOes[y]);
                                    //    }
                                    //}
                                    //else if (dOCUMENTOes[y].ESTATUS_SAP == "E")//Error en SAP
                                    //{
                                    //    lstD.Add(dOCUMENTOes[y]);
                                    //}
                                    //else if (dOCUMENTOes[y].ESTATUS_SAP == "X")//Succes en SAP
                                    //{
                                    //    lstD.Add(dOCUMENTOes[y]);
                                    //}
                                }
                                //LEJ 20.07.2018----------------------------------------------------------------T
                            }
                        }
                        //----
                        if (arrAprNum[0].Trim() == "DeAcuerdo" | arrAprNum[0].Trim() == "De Acuerdo")
                        {
                            for (int x = 0; x < lstD.Count; x++)
                            {
                                FLUJNEGO fn = new FLUJNEGO();
                                fn.NUM_DOC = lstD[x].NUM_DOC;
                                DateTime fecham = mm.Date;
                                fn.FECHAM = fecham;
                                fn.FECHAC = lstD[x].FECHAC;
                                fn.KUNNR = arrPiNN[0];
                                var cm = arrAprNum[0].ToString();
                                Comentario com = new Comentario();
                                cm += com.getComment(mm.Body, mm.ContentType);
                                //cm += " - " + mm.Body;
                                var cpos = db.FLUJNEGOes.Where(h => h.NUM_DOC.Equals(fn.NUM_DOC)).ToList().Count;
                                fn.POS = cpos + 1;
                                fn.COMENTARIO = cm;
                                db.FLUJNEGOes.Add(fn);
                                db.SaveChanges();
                            }
                        }
                        else if (arrAprNum[0].Trim() == "TengoObservaciones" | arrAprNum[0].Trim() == "Tengo Observaciones")
                        {
                            for (int x = 0; x < lstD.Count; x++)
                            {
                                FLUJNEGO fn = new FLUJNEGO();
                                fn.NUM_DOC = lstD[x].NUM_DOC;
                                fn.FECHAC = lstD[x].FECHAC;
                                DateTime fecham = mm.Date;
                                fn.FECHAM = fecham;
                                fn.KUNNR = arrPiNN[0];
                                var cm = arrAprNum[0] + " ";
                                Comentario com = new Comentario();
                                cm += com.getComment(mm.Body, mm.ContentType);
                                //cm += " - " + mm.Body;
                                var cpos = db.FLUJNEGOes.Where(h => h.NUM_DOC.Equals(fn.NUM_DOC)).ToList().Count;
                                fn.POS = cpos + 1;
                                fn.COMENTARIO = cm;
                                db.FLUJNEGOes.Add(fn);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Hubo algun error
                            break;
                        }
                    }
                    //para marcar el mensaje como leido
                    ic.AddFlags(Flags.Seen, mm);
                }
            }
            catch (Exception ex)
            {
                log.escribeLog("ERROR - " + ex.InnerException.ToString());
                throw new Exception(ex.InnerException.ToString());
            }
            finally
            {
                ic.Dispose();
            }
        }

        public void enviarCorreo(decimal nd, int c, int pos)
        {

            try
            {
                var workflow = db.FLUJOes.Where(a => a.NUM_DOC.Equals(nd) & a.POS == pos).OrderByDescending(a => a.POS).FirstOrDefault();
                APPSETTING mailtC = db.APPSETTINGs.Where(x => x.NOMBRE.Equals("mail") & x.ACTIVO).FirstOrDefault();
                string mailt = mailtC.VALUE;
                APPSETTING mailTestC = db.APPSETTINGs.Where(x => x.NOMBRE.Equals("mailTest") & x.ACTIVO).FirstOrDefault();
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
                    //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                    //mail.From = new MailAddress("lejgg017@gmail.com");

                    //mail.To.Add("rogelio.sanchez@sf-solutionfactory.com");
                    ////mail.To.Add("luisengonzalez25@hotmail.com");


                    //SmtpClient smtp = new SmtpClient();

                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 25; //465; //587
                    //smtp.Credentials = new NetworkCredential("lejgg017@gmail.com", "24abril14");
                    //smtp.EnableSsl = true;

                    APPSETTING urlC = db.APPSETTINGs.Where(x => x.NOMBRE.Equals("url") & x.ACTIVO).FirstOrDefault();
                    string cadUrl = urlC.VALUE;
                    string UrlDirectory = "";
                    if (c == 1)
                    {
                        UrlDirectory = cadUrl + "Correos/Index/" + nd;
                        //mail.Subject = "Aprobado";
                        mail.Subject = "A" + nd + "-" + DateTime.Now.ToShortTimeString();
                    }
                    if (c == 3)
                    {
                        UrlDirectory = cadUrl + "Correos/Details/" + nd;
                        //mail.Subject = "Rechazado";
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
                    mail.IsBodyHtml = true;
                    mail.Body = result;
                    client.Send(mail);
                }
            }
            catch (Exception ex)
            {
                log.escribeLog("ERROR - " + ex.InnerException.ToString());
                throw new Exception("No se ha podido enviar el email", ex.InnerException);
            }
        }

        //public void correo()
        //{
        //    ImapClient ic = new ImapClient("outlook.office365.com", "LA_TAT@kellogg.com", "Wpbcgc9*",
        //          AuthMethods.Login, 993, true);
        //    // Select a mailbox. Case-insensitive
        //    ic.SelectMailbox("INBOX");
        //    List<AE.Net.Mail.MailMessage> mx = ic.GetMessages(0, ic.GetMessageCount() - 1, false, false)
        //                                    .Where(m => !m.Flags.HasFlag(Flags.Seen) && !m.Flags.HasFlag(Flags.Deleted)).ToList();
        //    //En esta lista ingresaremos a los mails que sean recibidos como cc
        //    List<AE.Net.Mail.MailMessage> emRq17 = new List<AE.Net.Mail.MailMessage>();
        //    try
        //    {
        //        //ingresamos los correos 
        //        for (int i = 0; i < mx.Count; i++)
        //        {
        //            AE.Net.Mail.MailMessage mm = mx[i];
        //            string[] arrAsunto = mm.Subject.Split(']');
        //            //Recupero el asunto y lo separo del numdoc y pos
        //            string[] arrAprNum = arrAsunto[1].Split('-');
        //            decimal numdoc = Decimal.Parse(arrAprNum[1]);
        //            var xy = arrAprNum[0].Trim();
        //            if (arrAprNum[0].Trim() == "De Acuerdo")
        //            {
        //                emRq17.Add(mm);
        //            }
        //            else if (arrAprNum[0].Trim() == "Tengo Observaciones")
        //            {
        //                emRq17.Add(mm);
        //            }
        //        }

        //        FLUJNEGO fn = new FLUJNEGO();
        //        for (int i = 0; i < emRq17.Count; i++)
        //        {
        //            AE.Net.Mail.MailMessage mm = emRq17[i];
        //            string[] arrAsunto = mm.Subject.Split(']');
        //            //Recupero el asunto y lo separo del numdoc y pos
        //            string[] arrAprNum = arrAsunto[1].Split('-');
        //            var pid = arrAprNum[1];
        //            var fs = db.DOCUMENTOes.Where(f => f.PAYER_ID == pid && f.FECHAC.Value.Month == DateTime.Now.Month && f.FECHAC.Value.Year == DateTime.Now.Year && f.DOCUMENTO_REF == null).ToList();
        //            if (arrAprNum[0].Trim() == "De Acuerdo")
        //            {                       
        //                for (int x = 0; x < fs.Count; x++)
        //                {
        //                    fn = new FLUJNEGO();
        //                    fn.NUM_DOC = fs[x].NUM_DOC;
        //                    DateTime fecham = mm.Date;
        //                    fn.FECHAM = fecham;
        //                    fn.FECHAC = fs[x].FECHAC;
        //                    fn.KUNNR = arrAprNum[1];
        //                    var cm = arrAprNum[0] + " ";
        //                    cm += mm.Body;
        //                    var cpos = db.FLUJNEGOes.ToList().Count;
        //                    fn.POS = cpos + 1;
        //                    fn.COMENTARIO = cm;
        //                    db.FLUJNEGOes.Add(fn);
        //                    db.SaveChanges();
        //                }
        //            }
        //            else if (arrAprNum[0].Trim() == "Tengo Observaciones")
        //            {
        //                for (int x = 0; x < fs.Count; x++)
        //                {
        //                    fn = new FLUJNEGO();
        //                    fn.NUM_DOC = fs[x].NUM_DOC;
        //                     fn.FECHAC = fs[x].FECHAC;
        //                    DateTime fecham = mm.Date;
        //                    fn.FECHAM = fecham;
        //                    fn.KUNNR = arrAprNum[1];
        //                    var cm = arrAprNum[0] + " ";
        //                    cm += mm.Body;
        //                    var cpos = db.FLUJNEGOes.ToList().Count;
        //                    fn.POS = cpos + 1;
        //                    fn.COMENTARIO = cm;
        //                    db.FLUJNEGOes.Add(fn);
        //                    db.SaveChanges();
        //                }
        //            }
        //            else
        //            {
        //                //Hubo algun error
        //                break;
        //            }
        //            //para marcar el mensaje como leido
        //            ic.AddFlags(Flags.Seen, mm);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.InnerException.ToString());
        //    }
        //    finally
        //    {
        //        ic.Dispose();
        //    }
        //}

    }
}
