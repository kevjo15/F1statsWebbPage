namespace Skolinlämning.Models
{
    public class Author
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public BloggPost BloggPost { get; set; }

    }
}
