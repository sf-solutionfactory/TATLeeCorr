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
    
    public partial class DET_APROBP
    {
        public string SOCIEDAD_ID { get; set; }
        public int PUESTOC_ID { get; set; }
        public int VERSION { get; set; }
        public int POS { get; set; }
        public Nullable<int> PUESTOA_ID { get; set; }
        public Nullable<decimal> MONTO { get; set; }
        public Nullable<bool> PRESUPUESTO { get; set; }
        public bool ACTIVO { get; set; }
    
        public virtual DET_APROBH DET_APROBH { get; set; }
    }
}
