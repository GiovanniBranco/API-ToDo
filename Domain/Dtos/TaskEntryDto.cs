using System;
using System.ComponentModel.DataAnnotations;

namespace API_ToDo.Domain.Dtos
{
    public class TaskEntryDto
    {
        [Required (ErrorMessage = "Field Title is required")]
        public string Title { get; set; }

        public string Observation { get; set; }

        [Required(ErrorMessage = "Field User is required")]
        public string User { get; set; }

        [Required(ErrorMessage = "Field Date is required")]
        public DateTime Date { get; set; }
    }
}
