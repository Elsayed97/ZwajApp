using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZwajApp.Api.ViewModels
{
    public class PhotoForDetailsVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
    }
}
