namespace DewanSoftTask.Models
{
    public class ReceiptModel
    {
        public List<ReceiptItemModel> Items { get; set; }
        public decimal PaidAmount { get; set; }
    }

    public class ReceiptItemModel
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
