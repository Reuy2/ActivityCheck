using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Domain.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password {  get; set; }

        public void Form(string name, string password)
        {
            Name = name;
            Password = password;
        }

    }
}
