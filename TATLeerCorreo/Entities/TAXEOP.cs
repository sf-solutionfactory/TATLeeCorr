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
    
    public partial class TAXEOP
    {
        public string SOCIEDAD_ID { get; set; }
        public string PAIS_ID { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string KUNNR { get; set; }
        public int CONCEPTO_ID { get; set; }
        public int POS { get; set; }
        public Nullable<int> RETENCION_ID { get; set; }
        public Nullable<decimal> PORC { get; set; }
        public bool ACTIVO { get; set; }
        public string TRETENCION_ID { get; set; }
    
        public virtual RETENCION RETENCION { get; set; }
        public virtual TAXEOH TAXEOH { get; set; }
        public virtual TRETENCION TRETENCION { get; set; }
    }
}
