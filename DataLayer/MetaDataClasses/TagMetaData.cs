using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class TagMetaData
    {
        [Key]
        public int Tag_id { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Product_id { get; set; }

        [Display(Name = "برچسب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Tag1 { get; set; }
    }

    [MetadataType(typeof(TagMetaData))]
    public partial class Tag { }
}
