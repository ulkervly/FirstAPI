namespace BookStoreAPI.Entities
{
    public class Catagory:BaseEntity
    {
        public string Name {  get; set; }
        public List<Book>? Books { get; set; }
    }
}
