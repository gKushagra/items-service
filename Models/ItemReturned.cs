using System;

namespace items_service.Models
{
    public class ItemReturned
    {
        public string ID { get; set; }
        public string BinItemID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateReturned { get; set; }
    }
}