using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Log //add time
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }

        public Log(string content, string fileName)
        {
            this.Content = content;
            this.FileName = fileName;
            this.Date = DateTime.Now;

        }
    }
}