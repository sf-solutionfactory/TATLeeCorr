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
    
    public partial class MATERIAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MATERIAL()
        {
            this.MATERIALTs = new HashSet<MATERIALT>();
            this.MATERIALVKEs = new HashSet<MATERIALVKE>();
        }
    
        public string ID { get; set; }
        public string MTART { get; set; }
        public string MATKL_ID { get; set; }
        public string MAKTX { get; set; }
        public string MAKTG { get; set; }
        public string MEINS { get; set; }
        public Nullable<decimal> PUNIT { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
        public string CTGR { get; set; }
        public string BRAND { get; set; }
        public string MATERIALGP_ID { get; set; }
    
        public virtual ZCTGR ZCTGR { get; set; }
        public virtual ZBRAND ZBRAND { get; set; }
        public virtual MATERIALGP MATERIALGP { get; set; }
        public virtual UMEDIDA UMEDIDA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATERIALT> MATERIALTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MATERIALVKE> MATERIALVKEs { get; set; }
    }
}
