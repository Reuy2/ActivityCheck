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

        public string Email { get; set; }

        [Required(ErrorMessage ="Введите пароль")]
        public string Password {  get; set; }

    }
}
