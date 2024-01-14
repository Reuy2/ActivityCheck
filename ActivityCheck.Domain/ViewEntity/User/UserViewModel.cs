using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Domain.ViewEntity.User
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="Введите имя пользователя")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        public string Password {  get; set; }

        [Required(ErrorMessage ="Введите подтверждение пароля")]
        [Compare("Password",ErrorMessage ="Пароли не совпадают")]
        public string PasswordConfirm {  get; set; }

    }
}
