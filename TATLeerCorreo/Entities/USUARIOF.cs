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
    
    public partial class USUARIOF
    {
        public string USUARIO_ID { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string KUNNR { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
        public string USUARIOC_ID { get; set; }
        public Nullable<System.DateTime> FECHAC { get; set; }
        public string USUARIOM_ID { get; set; }
        public Nullable<System.DateTime> FECHAM { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
