//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YWPatient.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class concentrationUnit
    {
        public concentrationUnit()
        {
            this.medications = new HashSet<medication>();
        }
    
        public string concentrationCode { get; set; }
    
        public virtual ICollection<medication> medications { get; set; }
    }
}
