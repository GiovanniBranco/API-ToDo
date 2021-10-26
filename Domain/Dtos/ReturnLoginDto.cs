using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ToDo.Domain.Dtos
{
    public class ReturnLoginDto
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}
