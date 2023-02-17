namespace Application.Common 
{
    public class Subject
    {
        public List<IObserver> Subscribers { get; } = new List<IObserver>();

        public void Subscribe(IObserver observer) {
            if (observer != null && !Subscribers.Contains(observer)) {
                Subscribers.Add(observer);
            }
        }

        public void Unsubscribe(IObserver observer) {
            if (Subscribers.Contains(observer)) {
                Subscribers.Remove(observer);
            }
        }

        public void UnsubscribeAll() {
            Subscribers.Clear();
        }

        public void Notify() {
            foreach (var observer in Subscribers) {
                observer.Update(Clone());
            }
        }

        public Subject Clone() {
            return MemberwiseClone() as Subject;
        }
    }
}