namespace ITSparkTask.PL.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
        public int InvoiceId { get; set; }
    }
}
