//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TATLeerCorreo.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONPOSAPH
    {
        public decimal CONSECUTIVO { get; set; }
        public string TIPO_SOL { get; set; }
        public string TIPO_DOC { get; set; }
        public string SOCIEDAD { get; set; }
        public string FECHA_CONTAB { get; set; }
        public string FECHA_DOCU { get; set; }
        public string MONEDA { get; set; }
        public string HEADER_TEXT { get; set; }
        public Nullable<System.DateTime> FECHA_INIVIG { get; set; }
        public Nullable<System.DateTime> FECHA_FINVIG { get; set; }
        public string REFERENCIA { get; set; }
        public string PAIS { get; set; }
        public string NOTA { get; set; }
        public string CORRESPONDENCIA { get; set; }
        public Nullable<bool> CALC_TAXT { get; set; }
        public Nullable<int> RELACION { get; set; }
        public string RETENCION { get; set; }
        public string DESCRI_CONFIG { get; set; }
    
        public virtual SOCIEDAD SOCIEDAD1 { get; set; }
    }
}
