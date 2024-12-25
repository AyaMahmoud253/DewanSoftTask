namespace DewanSoftTask.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Balance { get; set; }
        public int AmountSold { get; set; } = 0;
    }

}
