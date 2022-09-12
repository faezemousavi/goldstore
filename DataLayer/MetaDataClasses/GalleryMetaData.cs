using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class GalleryMetaData
    {
        [Key]
        public int Gallery_id { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int product_id { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Gallery_image { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string title { get; set; }
    }

    [MetadataType(typeof(GalleryMetaData))]
    public partial class Gallery { }

}
