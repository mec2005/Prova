using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ConsumerQueue.Domain.Models
{
    [Serializable()]
    public class QueueEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string IP { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string QueryString { get; set; }
        public string Dispositivo { get; set; }
        public DateTime Data { get; set; }
    }
}
