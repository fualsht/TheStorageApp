﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.API.Models
{
    public class Shop : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string GPSLocation { get; set; }
        public string Website { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual AppUser CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public virtual AppUser ModifiedBy { get; set; }
        public Guid ModifiedById { get; set; }
    }
}
