using Program.Common;

namespace Program.Entities
{
    public class CheatingScreen : Observer
    {
        public Dictionary<int, Racer> Observing = new();

        public bool refresh;

        public override void Update(Subject subject)
        {
            if (subject is not Racer racer) return;

            if (!Observing.ContainsKey(racer.Bib))
            {
                Observing.Add(racer.Bib, racer);
            }
            else
            {
                Observing[racer.Bib] = racer;
            }

            refresh = true;
        }

        public override void UnsubscribeAll()
        {
            Dictionary<int, Racer>.Enumerator iter = Observing.GetEnumerator();
            while (iter.MoveNext())
            {
                iter.Current.Value.Unsubscribe(this);
            }
        }

        public List<Racer> Cheaters()
        {
            return Observing.Values.Where(racer => racer.Cheating).ToList();
        }
    }
}
