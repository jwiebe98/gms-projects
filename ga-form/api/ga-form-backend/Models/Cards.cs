namespace Gmsca.Group.GA.Backend.Models
{
    public class Cards
    {
        public CardTiers dental { get; set; } = new();
        public CardTiers health { get; set; } = new();
    }

    public class CardTypes
    {
        public float couple { get; set; }
        public float family { get; set; }
        public float single { get; set; }
    }

    public class CardTiers
    {
        public CardTypes gold { get; set; } = new();
        public CardTypes platinum { get; set; } = new();
        public CardTypes silver { get; set; } = new();
    }
}
