using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class FeatureMetaData
    {
        [Key]
        public int Feature_id { get; set; }

        [MaxLength(150)]
        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Feature_title { get; set; }
    }

    [MetadataType(typeof(FeatureMetaData))]
    public partial class Feature { }
}
