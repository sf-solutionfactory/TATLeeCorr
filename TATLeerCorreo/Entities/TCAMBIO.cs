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
    
    public partial class TCAMBIO
    {
        public string KURST { get; set; }
        public string FCURR { get; set; }
        public string TCURR { get; set; }
        public System.DateTime GDATU { get; set; }
        public Nullable<decimal> UKURS { get; set; }
    
        public virtual MONEDA MONEDA { get; set; }
        public virtual MONEDA MONEDA1 { get; set; }
    }
}