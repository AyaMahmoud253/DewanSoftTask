namespace DewanSoftTask.Models
{
    public class ReceiptItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }  // Navigation property
    }
}
