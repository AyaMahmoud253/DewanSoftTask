namespace DewanSoftTask.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public List<ReceiptItem> ReceiptItems { get; set; }
    }
}
