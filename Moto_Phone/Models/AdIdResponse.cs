using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moto_Phone.Models
{
    public class AdIdResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int entityId { get; set; }
        public int vehicleId { get; set; }
    }
}
