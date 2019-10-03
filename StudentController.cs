using GlobaDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using GlobaDev.Common;
using PagedList;
using PagedList.Mvc;
using BLL;
using DAL;


namespace GlobaDev.Controllers
{
    ///[HandleError]
    ///[TrackExecutionTrack]
    public class StudentController : Controller
    {
        ClsBussinessLogic _BLL = new ClsBussinessLogic();
            
        public ActionResult Index(string search,int page=1)
        {
            var vm = new StudentListVM();
            var studentList = _BLL.Get();
            vm.Students = studentList;
            const int pageSize = 3;
            

            if (search != null )
            {
                var  result = studentList.Where(s => s.StudentName.Contains(search))
                    .OrderBy(s => s.StudentId)
                    .Skip((page - 1) * pageSize) 
                    .Take(pageSize)
                    .ToList();

               var countAfterFilter = studentList.Where(s => s.StudentName.Contains(search)).Count();

                vm.Search = search;
                vm.CurrentPage = page;
                vm.PageSize = pageSize;
                vm.TotalPages = Math.Ceiling((double)countAfterFilter / pageSize);

                
                vm.Students = result;
            }          
            return View(vm);
        }

        //Get
        public ActionResult Add()
        {          
            return View();
        }
        //Get
        public ActionResult Edit(int Id)
        {
            Student std = new Student();
            var studentList = _BLL.Get();
            std = studentList.Where(s => s.StudentId == Id).Single();
            
            return View(std);
        }
        //Get
        public ActionResult Delete(int Id)
        {
            var Result = _BLL.Delete(Id);
            return RedirectToAction("Index");
        }

        //Get
        public ActionResult GetById (int Id)
        {
            var Result = _BLL.GetById(Id);
            return View(Result);
        }

        //-----------------------------------POST---------------------------//
        [HttpPost]
        public ActionResult Add(Student std)
        {
            var Result = _BLL.Add(std);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult Edit(Student std)
        {
            var Result = _BLL.Edit(std);
            return RedirectToAction("Index");

        }

        
  

    }
}

