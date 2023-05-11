namespace Buzzr.API.models
{
    public class TeamModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public IList<PlayerModel> Players { get; set; }
    }
}
