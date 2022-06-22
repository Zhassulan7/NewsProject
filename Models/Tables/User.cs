using System.ComponentModel.DataAnnotations;


namespace Models.Tables
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
