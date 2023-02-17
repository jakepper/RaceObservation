using Application.Common;

namespace Application.Entities {
    public class Racer : Subject
    {
        private RaceGroup _group;
        public string Name;
        public int Bib;
        public DateTime FinishTime;
        public bool DNF;

        public Racer(RaceGroup group, string name, int bib) {
            _group = group;
            Name = name;
            Bib = bib;
        }
    }
}