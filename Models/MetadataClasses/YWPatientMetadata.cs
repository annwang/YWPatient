using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Create a metadata class call YWPatientMetadata for modify forms and will avoid contaminating the generated models

namespace YWPatient.Models
{
    [MetadataType(typeof(YWPatientMetadata))]
    public partial class patient
    {

    }
    public class YWPatientMetadata
    {
        //use [Display(name= "value")] formate each field's display name
        //use [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:XXX}")] formate some date's formatting
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:00000}")]
        [Display(Name = "Patient #")]
        public int patientId { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }


        [Display(Name = "Stree Address")]
        public string address { get; set; }


        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "Province")]
        public string provinceCode { get; set; }

        [Display(Name = "Postal Code")]
        public string postalCode { get; set; }

        [Display(Name = "OHIP #")]
        public string OHIP { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd MMM yyyy}" )]
        public Nullable<System.DateTime> dateOfBirth { get; set; }

        [Display(Name = "Deceased?")]
        public bool deceased { get; set; }


        [Display(Name = "Date of Death")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> dateOfDeath { get; set; }


        [Display(Name = "Home Phone")]
        public string homePhone { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }
    }

}