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
    
    public partial class DOCTOCLASIF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOCTOCLASIF()
        {
            this.DOCTOAYUDAs = new HashSet<DOCTOAYUDA>();
        }
    
        public int ID_CLASIFICACION { get; set; }
        public string CLASIFICACION_DSC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCTOAYUDA> DOCTOAYUDAs { get; set; }
    }
}
