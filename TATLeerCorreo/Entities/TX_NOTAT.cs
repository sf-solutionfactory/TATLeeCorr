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
    
    public partial class TX_NOTAT
    {
        public string SPRAS_ID { get; set; }
        public int TNOTA_ID { get; set; }
        public string TXT50 { get; set; }
    
        public virtual SPRA SPRA { get; set; }
        public virtual TX_TNOTA TX_TNOTA { get; set; }
    }
}
