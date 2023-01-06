using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementSystem.Models
{
    public class ClsEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public int Password { get; set; }
        public ClsEmployee() { }
        public ClsEmployee(int id, string name, int age, string phone, double salary, string address, string username, int password)
        {
            Id = id;
            Name = name;
            Age = age;
            Phone = phone;
            Salary = salary;
            Address = address;
            Username = username;
            Password = password;
        }

    }
}
