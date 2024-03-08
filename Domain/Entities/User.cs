namespace IntegraHub.Domain.Entities
{
    public class User : BaseEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Password { get; set; }
        public string? Color { get; set; }
        public DateTime DateBirth { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public long UserTypeId { get; set; }

        public long CompanyId { get; set; } 

        public Company Company { get; set; }
    }
}
