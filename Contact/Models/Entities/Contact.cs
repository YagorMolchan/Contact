using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Contact.Models.Entities
{
    public class UserContact
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Место работы")]
        public string JobTitle { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }


    }
}
