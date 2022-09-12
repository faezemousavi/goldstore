using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class UserMetaData
    {
        [Key]
        public int User_id { get; set; }

        [Display(Name = "نام نقش")]
        public int Role_id { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string user_name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد!")]
        public string email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        public string password { get; set; }

        [Display(Name = "کد فعالسازی")]
        public string ActiveCode { get; set; }

        [Display(Name = "فعال بودن")]
        public bool isActive { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public System.DateTime RegisterDate { get; set; }
    }

    [MetadataType(typeof(UserMetaData))]
    public partial class User { }
}
