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
    
    public partial class CITy
    {
        public long ID { get; set; }
        public string NAME { get; set; }
        public int STATE_ID { get; set; }
    
        public virtual STATE STATE { get; set; }
    }
}
