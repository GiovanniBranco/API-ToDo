using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_ToDo.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Observation { get; set; }
        [Required]
        [ForeignKey("user_id")]
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
