namespace TRUSIRENT.Models.Entities
{
    public class Vote
    {
        public int VoteId { get; set; }

        public int OptionId { get; set; }
        public Option? Option { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
