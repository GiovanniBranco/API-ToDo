using System;

namespace API_ToDo.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Observation { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
