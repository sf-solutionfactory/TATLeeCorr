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
    
    public partial class MATERIALT
    {
        public string SPRAS { get; set; }
        public string MATERIAL_ID { get; set; }
        public string MAKTX { get; set; }
        public string MAKTG { get; set; }
    
        public virtual MATERIAL MATERIAL { get; set; }
        public virtual SPRA SPRA { get; set; }
    }
}