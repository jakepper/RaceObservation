namespace Program.Common
{
    public abstract class Observer
    {
        public string Name { get; set; }
        public abstract void Update(Subject subject);
        public abstract void UnsubscribeAll();
    }
}