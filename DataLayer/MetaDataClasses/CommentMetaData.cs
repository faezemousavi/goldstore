using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.MetaDataClasses
{
    class CommentMetaData
    {
        [Key]
        public int Comment_id { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Product_id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Email { get; set; }

        [Display(Name = "وبسایت")]
        public string WebSite { get; set; }

        [MaxLength(800)]
        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Cm { get; set; }

        [Display(Name = "تاریخ")]
        public System.DateTime Create_Date { get; set; }

        [Display(Name = "نظر مرجع")]
        public Nullable<int> Parent_id { get; set; }
    }

    [MetadataType(typeof(CommentMetaData))]
    public partial class Comment{ }

}
