namespace Buzzr.API.Models
{
    public class BuzzedPlayerModel
    {
        public Guid PlayerId { get; set; }

        public TimeOnly BuzzTime { get; set; }
    }
}
