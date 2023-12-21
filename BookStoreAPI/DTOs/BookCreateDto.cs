namespace BookStoreAPI.DTOs
{
    public class BookCreateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double CostPrice { get; set; }
        public int CatagoryId {  get; set; }
    }
}
