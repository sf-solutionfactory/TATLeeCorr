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
    
    public partial class PRESUPSAPP
    {
        public int ID { get; set; }
        public int ANIO { get; set; }
        public int POS { get; set; }
        public Nullable<int> PERIOD { get; set; }
        public string TYPE { get; set; }
        public string BUKRS { get; set; }
        public string VKORG { get; set; }
        public string VTWEG { get; set; }
        public string SPART { get; set; }
        public string VKBUR { get; set; }
        public string VKGRP { get; set; }
        public string BZIRK { get; set; }
        public string MATNR { get; set; }
        public string PRDHA { get; set; }
        public string KUNNR { get; set; }
        public string KUNNR_P { get; set; }
        public string BANNER { get; set; }
        public string BANNER_CALC { get; set; }
        public string KUNNR_PAY { get; set; }
        public string FECHAP { get; set; }
        public string UNAME { get; set; }
        public string XBLNR { get; set; }
        public Nullable<decimal> VVX17 { get; set; }
        public Nullable<decimal> CSHDC { get; set; }
        public Nullable<decimal> RECUN { get; set; }
        public Nullable<decimal> DSTRB { get; set; }
        public Nullable<decimal> OTHTA { get; set; }
        public Nullable<decimal> ADVER { get; set; }
        public Nullable<decimal> CORPM { get; set; }
        public Nullable<decimal> POP { get; set; }
        public Nullable<decimal> OTHER { get; set; }
        public Nullable<decimal> CONPR { get; set; }
        public Nullable<decimal> OHV { get; set; }
        public Nullable<decimal> FREEG { get; set; }
        public Nullable<decimal> RSRDV { get; set; }
        public Nullable<decimal> SPA { get; set; }
        public Nullable<decimal> PMVAR { get; set; }
        public Nullable<decimal> GRSLS { get; set; }
        public Nullable<decimal> NETLB { get; set; }
    
        public virtual PRESUPSAPH PRESUPSAPH { get; set; }
    }
}
