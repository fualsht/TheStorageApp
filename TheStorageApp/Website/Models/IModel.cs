using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Models
{
    public interface IModel
    {
        bool IsSelected { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        //DateTime CreatedOn { get; set; }
        //DateTime ModifiedOn { get; set; }
    }
}