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
    
    public partial class DELEGAR
    {
        public string USUARIO_ID { get; set; }
        public string USUARIOD_ID { get; set; }
        public System.DateTime FECHAI { get; set; }
        public System.DateTime FECHAF { get; set; }
        public bool ACTIVO { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
        public virtual USUARIO USUARIO1 { get; set; }
    }
}
