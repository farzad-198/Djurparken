

namespace Domain.Entities
{
    public class Visitor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public int Age { get; set; }
        //one to many relationship with visit
        public List<Visit> Visits { get; set; } = new List<Visit>();

    }
}
