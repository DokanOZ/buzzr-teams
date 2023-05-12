namespace Buzzr.Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeOnly BuzzedTime { get; set; }

        public bool BuzzActive { get; set; }
        public Guid TeamId { get; set; }

        public Player(string name, Guid teamId)
        {
            Id = Guid.NewGuid();
            Name = name;
            TeamId = teamId;
            BuzzedTime = new TimeOnly(0, 0, 0, 0);
            BuzzActive = true;
        }

        public void ResetBuzzer()
        {
            BuzzedTime = new TimeOnly(0, 0, 0, 0);
        }

        public void Buzzed(int hour, int minute, int second, int millisecond)
        {
            BuzzedTime = new TimeOnly(hour, minute, second, millisecond);
        }

        public void BuzzBan(bool active)
        {
            BuzzActive = active;
        }
    }
}
