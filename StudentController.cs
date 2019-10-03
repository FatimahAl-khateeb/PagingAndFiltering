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

        
        

        
       

       


        // Get 
        //public ActionResult Edit(int? Id)
        //{


        //    Student std = new Student();
        //    if (Id != null)  // edit
        //    {
        //        std = studentList.Where(s => s.StudentId == Id).Single();
        //    }

        //    return View(std); //create

        //}


        //Post 
        //    [HttpPost]
        //    public ActionResult Edit(Student std)
        //    {
        //        //--------------------------------------------
        //        //Validation
        //        if (!ModelState.IsValid)
        //        {
        //            return View(std);
        //        }


        //        bool nameAlreadyExists = studentList.Exists(x =>
        //            x.StudentName == std.StudentName
        //            && (x.StudentId == 0 || x.StudentId != std.StudentId));

        //        if (nameAlreadyExists)
        //        {
        //            ModelState.AddModelError(string.Empty, "Student Name and age already exists.");

        //            return View(std);
        //        }

        //        //--------------------------------------------

        //        if (std.StudentId == 0)  //create
        //        {
        //            var maxId = studentList.Max(x => x.StudentId);
        //            std.StudentId = maxId + 1;
        //            studentList.Add(std);
        //        }
        //        else // edit
        //        {
        //            // bool ageAlreadyExists = studentList.Exists(x => x.Age == std.Age);
        //            var oldStd = studentList.Where(s => s.StudentId == std.StudentId).Single();
        //            oldStd.StudentName = std.StudentName;
        //            oldStd.Age = std.Age;

        //        }

        //        return RedirectToAction("Index");
        //    }

        //    //Post
        //    [HttpPost]
        //    public ActionResult Delete(Student std)
        //    {
        //        studentList.RemoveAt(std.StudentId);
        //        return RedirectToAction("Index");
        //    }

        //    public ActionResult TestLayout()
        //    {
        //        return View("TestLayout", "_myLayoutPage");
        //    }


        //    public ActionResult Paging (string search, int? page)
        //    {


        //        var q = studentList.Where(x =>
        //                String.IsNullOrWhiteSpace(search) || x.StudentName.StartsWith(search)
        //        );
        //        var count = q.Count();
        //        var l = q.ToPagedList(page ?? 1, 3);
        //        return View(l); /*page ?? 1 means if page is null (first time open page) then go to page num 1 otherwise what page parameter holds*/

        //    }

    }
}
// Notes :
// how to declare one std for both condition 
// how View () works ?
// how can i make sure that id will reach post as 0 ? as Edit method written now . f12 tools or like console 
// see how post works . i think i've done 