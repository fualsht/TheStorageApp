using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.API.Models
{
    public enum ErrorTypes
    {
        Information,
        Warning,
        Error
    }

    public class LogEntry : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        [NotMapped]
        public DateTime ModifiedOn { get; set; }

        public string Message { get; set; }
        public ErrorTypes ErrorCode { get; set; }

        public virtual AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public virtual AppUser ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
    }
}
