namespace Application.Common
{
    public interface IObserver
    {
        public void Update(Subject subject);
    }
}