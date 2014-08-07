namespace Thetjyoung.Web.Models
{
    public class RunePage
    {
        public long Id { get; set; }
        public bool IsCurrent { get; set; }
        public string Name { get; set; }
        public Rune[] Runes { get; set; }
    }
}