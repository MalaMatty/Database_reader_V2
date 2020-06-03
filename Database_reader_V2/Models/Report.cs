using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Database_reader_V2.Models
{
    public class Report
    {
        

        [Required(ErrorMessage = "Rep_Id is required.")]
        public string Rep_Id { get; set; }

        [Required(ErrorMessage = "Rep_Lot_Id is required.")]
        public string Rep_Lot_Id { get; set; }

        [Required(ErrorMessage = "Rep_PDFStatus is required.")]
        public int Rep_PDFStatus { get; set; }

        

        [Required(ErrorMessage = "Rep_Status is required.")]
        public int Rep_Status { get; set; }

        [Required(ErrorMessage = "Rep_Type is required.")]
        public int Rep_Type { get; set; }
    }
}