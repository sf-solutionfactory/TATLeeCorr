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
    
    public partial class DET_AGENTEH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DET_AGENTEH()
        {
            this.DET_AGENTEP = new HashSet<DET_AGENTEP>();
        }
    
        public string SOCIEDAD_ID { get; set; }
        public int PUESTOC_ID { get; set; }
        public int VERSION { get; set; }
        public long AGROUP_ID { get; set; }
        public string USUARIOC_ID { get; set; }
        public bool ACTIVO { get; set; }
    
        public virtual GAUTORIZACION GAUTORIZACION { get; set; }
        public virtual PUESTO PUESTO { get; set; }
        public virtual SOCIEDAD SOCIEDAD { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DET_AGENTEP> DET_AGENTEP { get; set; }
    }
}
