namespace Buzzr.Domain
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public List<Player> players { get; set; }

        public Team(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            players = new List<Player>();
            Points = 0;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(Player player) 
        {  
            players.Remove(player); 
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        public void RemovePoints(int points)
        {
            Points -= points;
        }
    }
}
