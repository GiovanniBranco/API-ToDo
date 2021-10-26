using System;

namespace API_ToDo.Domain.Dtos
{
    public class TaskReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Observation { get; set; }
        public string User { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; }
    }
}
