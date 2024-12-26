namespace DewanSoftTask.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public List<ReceiptItem> ReceiptItems { get; set; } = new List<ReceiptItem>();
    }
}
