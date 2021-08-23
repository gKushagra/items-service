using System;
using System.Collections.Generic;

namespace items_service.Models
{
    public class Item
    {
        public string ID { get; set; }
        public int Quantity { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime DateAdded { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string Code { get; set; }
        public string SupplierID { get; set; }
    }
}