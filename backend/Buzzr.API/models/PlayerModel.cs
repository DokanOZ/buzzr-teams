namespace Buzzr.API.models
{
    public class PlayerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid teamId { get; set; }
    }
}
