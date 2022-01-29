using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.ViewModels
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage ="اسم المستخدم مطلوب")]
        [StringLength(15,MinimumLength =3,ErrorMessage ="يجب ان لا يزيد اسم المستخدم عن 15 حرف ولا تقل عن 3 حروف")]
        public string userName { get; set; }

        [Required(ErrorMessage ="كلمة المرور مطلوبة")]
        [StringLength(15,MinimumLength =5,ErrorMessage ="يجب ان لا تزيد كلمة المرور عن 15 حرف ولا تقل عن 5 حروف")]
        public string password { get; set; }
        
    }
}