using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementSystem.Models
{
    public class ClsSupplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ClsSupplier() { }
        public ClsSupplier(int id, string name, string phone, string address)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
        }
    }
}
