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
    
    public partial class WARNINGPT
    {
        public string SPRAS_ID { get; set; }
        public string TAB_ID { get; set; }
        public string WARNING_ID { get; set; }
        public string TXT100 { get; set; }
    
        public virtual SPRA SPRA { get; set; }
    }
}
