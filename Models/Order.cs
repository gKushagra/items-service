using System;
using System.Collections.Generic;

namespace items_service.Models
{
    public class Order
    {
        public string ID { get; set; }
        public string SupplierID { get; set; }
        // public Supplier Supplier { get; set; }
        public string ItemID { get; set; }
        // public Item Item { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}