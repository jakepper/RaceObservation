namespace Program.Common 
{
    public class Subject
    {
        public List<Observer> Subscribers { get; } = new List<Observer>();

        public void Subscribe(Observer observer) {
            if (observer != null && !Subscribers.Contains(observer)) {
                Subscribers.Add(observer);
            }
        }

        public void Unsubscribe(Observer observer) {
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