using System;

namespace API_ToDo.Domain.Dtos
{
    public class TaskEntryUpdateDto
    {
        public string Title { get; set; }
        public string Observation { get; set; }
        public DateTime? Date { get; set; }
        public bool? Done { get; set; }
    }
}
