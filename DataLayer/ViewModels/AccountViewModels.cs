using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید!")]
        public String Username { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نمی باشد!")]
        public String Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
      
        
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="تکرار رمز عبور با رمز عبور یکسان نیست!")]
        public String rePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]        
        [Required(ErrorMessage ="لطفا {0} را وارد کنید!")] 
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد!")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]    
        [Required(ErrorMessage ="لطفا {0} را وارد کنید!")] 
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool remembreMe { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد!")]
        public String Email { get; set; }
    }

    public class RecoveryPasswordViewModel
    {
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        public String Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز عبور یکسان نیست!")]
        public String rePassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        public String Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز عبور یکسان نیست!")]
        public String rePassword { get; set; }
    }
}
