using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class RolesMetaData
    {
        [Key]
        public int Role_id { get; set; }

        [MaxLength(250)]
        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Role_title { get; set; }


        [MaxLength(150)]
        [Display(Name = "نام سیستمی نفش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string Role_name { get; set; }
    }


    [MetadataType(typeof(RolesMetaData))]
    public partial class Role { }
}
