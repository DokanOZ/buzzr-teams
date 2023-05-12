using Buzzr.API.models;

namespace Buzzr.API.Models
{
    public class BuzzedPlayerModel
    {
        public Guid PlayerId { get; set; }

        public BuzzTimeModel BuzzTime { get; set; }
    }
}
