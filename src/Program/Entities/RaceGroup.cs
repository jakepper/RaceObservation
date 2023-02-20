using System.Collections;

namespace Program.Entities
{
    public class RaceGroup
    {
        public int Id;
        public string Label;
        public int MinBib;
        public int MaxBib;
        public DateTime StartTime;
        public List<Racer> Racers = new();

        public RaceGroup(int id, string label, int startTime, int min, int max) {
            Id = id;
            Label = label;
            MinBib = min;
            MaxBib = max;
        }

        public void AddRacer(Racer racer)
        {
            if (!Racers.Contains(racer))
            {
                Racers.Add(racer);
            }
        }

        public Racer? GetRacer(int bib)
        {
            return Racers.Find(racer => racer.Bib == bib);
        }
    }
}
