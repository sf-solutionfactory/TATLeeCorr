//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TATLeerCorreo.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class DOCUMENTOBORRREC
    {
        public string USUARIOC_ID { get; set; }
        public int POS { get; set; }
        public Nullable<System.DateTime> FECHAF { get; set; }
        public Nullable<int> PERIODO { get; set; }
        public Nullable<int> EJERCICIO { get; set; }
        public Nullable<decimal> MONTO_BASE { get; set; }
        public Nullable<decimal> MONTO_FIJO { get; set; }
        public Nullable<decimal> MONTO_GRS { get; set; }
        public Nullable<decimal> MONTO_NET { get; set; }
        public string ESTATUS { get; set; }
        public Nullable<decimal> PORC { get; set; }
        public Nullable<decimal> DOC_REF { get; set; }
        public Nullable<System.DateTime> FECHAV { get; set; }
    
        public virtual DOCUMENTBORR DOCUMENTBORR { get; set; }
    }
}
