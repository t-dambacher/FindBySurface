namespace FindBySurface.Dtos
{
    public struct Coords
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Coords(double latitude, double longitude)
            : this()
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public override string ToString()
        {
            return $"{Latitude},{Latitude}";
        }
    }
}
