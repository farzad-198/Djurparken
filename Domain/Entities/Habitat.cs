

namespace Domain.Entities
{
    public class Habitat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Climate { get; set; } = string.Empty;
        public string Vegetation { get; set; } = string.Empty;
        //one to many relationship with animal
        public List<Animal> Animals { get; set; } = new List<Animal>();
    }
}
