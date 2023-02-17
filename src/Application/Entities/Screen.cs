using Application.Common;

namespace Application.Entities
{
    public class Screen : IObserver
    {
        private readonly Dictionary<int, Racer>  _observing = new();
        public bool refresh;
        public void Update(Subject subject)
        {
            var racer = subject as Racer;
            if (racer == null) return;

            if (!_observing.ContainsKey(racer.Bib)) {
                _observing.Add(racer.Bib, racer);
            }
            else {
                _observing[racer.Bib] = racer;
            }
        }

        public void UnsubscribeAll() {
            Dictionary<int, Racer>.Enumerator iter = _observing.GetEnumerator();
            while (iter.MoveNext()) {
                iter.Current.Value.Unsubscribe(this);
            }
        }
    }
}