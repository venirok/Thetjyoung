namespace Thetjyoung.Web.Models
{
    public class MasteryPage
    {
        public long Id { get; set; }
        public bool IsCurrent { get; set; }
        public string Name { get; set; }
        public Mastery[] Masteries { get; set; }
    }
}