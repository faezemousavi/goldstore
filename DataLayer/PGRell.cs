//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class PGRell
    {
        public int PG_id { get; set; }
        public int Product_id { get; set; }
        public int Group_id { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Products_group Products_group { get; set; }
    }
}