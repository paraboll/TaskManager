using System.Collections.Generic;
using TM.Domain.Entities;

namespace TM.WebServer.Entities
{
    public class UIBuisnessTaskDTO
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorLogin { get; set; }
        public string EmployeeLogin { get; set; }
        public string Status { get; set; }
        public List<UITaskCommentDTO> TaskComments { get; set; }
    }
}
