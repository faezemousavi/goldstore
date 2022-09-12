using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class PGRellMetaData
    {
        [Key]
        public int PG_id { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Product_id { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Group_id { get; set; }

    }

    [MetadataType(typeof(PGRellMetaData))]
    public partial class PGRell { }
}
