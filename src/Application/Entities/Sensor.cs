namespace Application.Entities
{
    public class Sensor
    {
        private int _id;
        public float Location;

        public Sensor(int id, float location) {
            _id = id;
            Location = location;
        }
    }
}