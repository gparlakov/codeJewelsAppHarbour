namespace CodeJewels.Models
{
    public class CodeJewel
    {
        public int Id { get; set; }

        public string AuthorEMail { get; set; }

        public Category Category { get; set; }

        public string Code { get; set; }

        //public ICollection<Vote> Votes { get; set; }
    }
}
