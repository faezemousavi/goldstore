using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataLayer
{
    public class ProductMetaData
    {
        [Key]
        public int Product_id { get; set; }

        [MaxLength(200)]
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Product_title { get; set; }

        [MaxLength(500)]
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.MultilineText)]
        public string Product_ShortDescription { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Product_text { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public int Product_price { get; set; }

        [Display(Name = "تاریخ تولید")]
        public System.DateTime Product_CreateDate { get; set; }

        [Display(Name = "عکس محصول")]
        public string Product_image { get; set; }

    }

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product { }
}
