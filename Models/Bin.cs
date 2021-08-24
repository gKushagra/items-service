using System;
using System.Collections.Generic;

namespace items_service.Models
{
    public class Bin
    {
        public string ID { get; set; }
        public string CabinetID { get; set; }
        public string Tag { get; set; }
        public int Capacity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}