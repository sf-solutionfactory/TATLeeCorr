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
    
    public partial class TSOLT
    {
        public string SPRAS_ID { get; set; }
        public string TSOL_ID { get; set; }
        public string TXT020 { get; set; }
        public string TXT50 { get; set; }
        public string TXT010 { get; set; }
    
        public virtual SPRA SPRA { get; set; }
        public virtual TSOL TSOL { get; set; }
    }
}
