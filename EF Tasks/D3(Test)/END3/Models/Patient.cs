using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace END3.Models
{
    internal class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
