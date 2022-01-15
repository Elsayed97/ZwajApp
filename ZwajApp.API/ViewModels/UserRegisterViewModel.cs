using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage ="اسم المستخدم مطلوب")]
        [StringLength(15,MinimumLength =5,ErrorMessage ="يجب ان لا يزيد اسم المستخدم عن 15 حرف ولا تقل عن 5 حروف")]
        public string userName { get; set; }
        
        [Required(ErrorMessage ="البريد الالكترونى مطلوب")]
        [EmailAddress]
        [StringLength(30,MinimumLength =15,ErrorMessage ="يجب ان لا يزيد البريد الالكترونى عن 30 حرف ولا تقل عن 15 حروف")]
        public string Email { get; set; }

        [Required(ErrorMessage ="كلمة المرور مطلوبة")]
        [StringLength(15,MinimumLength =5,ErrorMessage ="يجب ان لا تزيد كلمة المرور عن 15 حرف ولا تقل عن 5 حروف")]
        public string password { get; set; }
        
    }
}