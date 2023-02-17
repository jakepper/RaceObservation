namespace Application.Entities {
    public class RaceGroup
    {
        private readonly List<Racer> _racers = new();
        public string Label;
        public int MinBib;
        public int MaxBib;
        public DateTime StartTime;
        public RaceGroup(string label, int min, int max) {
            Label = label;
            MinBib = min;
            MaxBib = max;
        }

        public void AddRacer(Racer racer) {
            if (!_racers.Contains(racer)) {
                _racers.Add(racer);
            }
        }
    }
}
