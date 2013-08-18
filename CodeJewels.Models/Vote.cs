namespace CodeJewels.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public virtual CodeJewel CodeJewel { get; set; }

        public string Author { get; set; }

        public bool IsUpVote { get; set; }
    }
}