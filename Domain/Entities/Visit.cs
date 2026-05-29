

namespace Domain.Entities
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime VisitDate { get; set; }
        public bool HasPaidTicket { get; set; }
        public Guid VisitorId { get; set; }
        //many to one relationship with visitor
        public Visitor? Visitor { get; set; }



    }
}
