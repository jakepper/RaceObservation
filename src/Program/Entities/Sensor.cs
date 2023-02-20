namespace Program.Entities
{
    public struct Sensor
    {
        public readonly int Id;
        public readonly float Location;

        public Sensor(int id, float location)
        {
            Id = id;
            Location = location;
        }
    }
}