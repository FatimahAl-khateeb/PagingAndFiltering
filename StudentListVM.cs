using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobaDev.Models
{
    public class StudentListVM
    {
        public IEnumerable<DAL.Student> Students { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
    }
}