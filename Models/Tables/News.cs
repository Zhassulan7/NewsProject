using System.ComponentModel.DataAnnotations;

namespace Models.Tables
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
