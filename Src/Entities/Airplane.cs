namespace dotnet_airplanes_api.Src.Entities
{
    public class Airplane
    {
        public int Id { get; set; }
        public required string Model { get; set; }
        public required int Capacity { get; set; }
    }
}
