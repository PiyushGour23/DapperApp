namespace DapperApp.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }     
    }
}
