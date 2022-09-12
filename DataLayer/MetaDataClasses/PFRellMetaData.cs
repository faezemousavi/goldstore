using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    class PFRellMetaData
    {
        [Key]
        public int PF_id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Product_id { get; set; }

        [Display(Name = "نام ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Feature_id { get; set; }

        [MaxLength(250)]
        [Display(Name = "مقدار ویژگی")]
        public string value { get; set; }

    }

    [MetadataType(typeof(PFRellMetaData))]
    public partial class PFRell { }
}
