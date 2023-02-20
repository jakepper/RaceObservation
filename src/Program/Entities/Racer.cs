using Program.Common;

namespace Program.Entities
{
    public class Racer : Subject
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Bib { get; set; }
        public DateTime FinishTime { get; set; }
        public bool DNF { get; set; }

        public float Location { get; set; }
        public bool Cheating { get; set; }

        public Racer(int groupId, string name, int bib) {
            GroupId = groupId;
            Name = name;
            Bib = bib;
        }
    }
}