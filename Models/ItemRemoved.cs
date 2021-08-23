using System;

namespace items_service.Models
{
    public class ItemRemoved
    {
        public string ID { get; set; }
        public string PatientID { get; set; }
        public string BinItemID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateRemoved { get; set; }
    }
}