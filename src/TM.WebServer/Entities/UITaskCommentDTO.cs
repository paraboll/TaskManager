using System;

namespace TM.WebServer.Entities
{
    public class UITaskCommentDTO
    {
        public string ExternalId { get; set; }

        public string Text { get; set; }
        public DateTime Time { get; set; }

        public string BuisnessTaskexternalId { get; set; }
        public string AuthorLogin { get; set; }
    }
}
