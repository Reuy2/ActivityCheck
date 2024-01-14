using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Domain.ViewEntity.Activity
{
    public class ActivityViewModel
    {
        public int id { get; set; }
        [DisplayName("Название активности")]
        [Required(ErrorMessage = "Укажите название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        [Required(ErrorMessage = "Укажите Описание")]
        public string Description { get; set; }
        [DisplayName("Дата активности")]
        public DateTime Created { get; set; }
        [DisplayName("Длительность активности")]
        [Required(ErrorMessage ="Укажите продолжительность")]
        public int DurationInSec { get; set; }
        public byte[]? Icon { get; set; }
    }
}
