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
    
    public partial class DOCUMENTOBORRM
    {
        public string USUARIOC_ID { get; set; }
        public decimal POS_ID { get; set; }
        public int POS { get; set; }
        public string MATNR { get; set; }
        public Nullable<decimal> PORC_APOYO { get; set; }
        public Nullable<decimal> APOYO_EST { get; set; }
        public Nullable<decimal> APOYO_REAL { get; set; }
        public Nullable<System.DateTime> VIGENCIA_DE { get; set; }
        public Nullable<System.DateTime> VIGENCIA_AL { get; set; }
        public Nullable<decimal> VALORH { get; set; }
    
        public virtual DOCUMENTOBORRP DOCUMENTOBORRP { get; set; }
    }
}
