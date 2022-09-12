using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Products_groupMetaData
    {
        [Key]
        public int Group_id { get; set; }


        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [MaxLength(200)]
        public string Group_title { get; set; }

        public Nullable<int> Parent_id { get; set; }

    }

    [MetadataType(typeof(Products_groupMetaData))]
    public partial class Products_group { }
}
