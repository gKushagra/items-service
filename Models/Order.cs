using System;
using System.Collections.Generic;

namespace items_service.Models
{
    public class Order
    {
        public string ID { get; set; }
        public string SupplierID { get; set; }
        public string ItemID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}