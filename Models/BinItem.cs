using System;
using System.Collections.Generic;

namespace items_service.Models
{
    public class BinItem
    {
        public string ID { get; set; }
        public string BinID { get; set; }
        public string ItemID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}