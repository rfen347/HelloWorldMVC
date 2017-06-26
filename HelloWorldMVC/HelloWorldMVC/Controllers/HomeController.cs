using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorldMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MyName = "My name is Justin";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HelloWorld()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListUser()
        {
            return View();
        }


        public ActionResult TableUser()
        {
            using (var database = new MytubeEntities1())
            {
                var listOfAllUser = database.Users.Where(x => x.IsActive).ToList();
                ViewBag.ListOfAllUser = listOfAllUser;
                return PartialView("_TableUser");
            }
        }

        public ActionResult EditUser(int id)
        {
            using (var db = new MytubeEntities1())
            {
                var userIWantToEdit = db.Users.Find(id);
                if (userIWantToEdit != null)
                {
                    return PartialView(userIWantToEdit);
                }
                return View();
            }
        }

        public ActionResult SaveEditUser(User model)
        {
            using (var db = new MytubeEntities1())
            {
                var user = db.Users.Find(model.Id);
                user.FirstName = model.FirstName;
                user.EmailAddress = model.EmailAddress;
                user.Password = model.Password;
                db.SaveChanges();
                return Json(new { Success = true });
            }
        }

        public ActionResult DeleteUser(int id)
        {
            using (var db = new MytubeEntities1())
            {
                var user = db.Users.Find(id);
                user.IsActive = false;
                db.SaveChanges();
                return View("SaveChangeSuccess");
            }
        }

        public ActionResult AddNewUser()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddNewUser(User newUser)
        {
            using (var db = new MytubeEntities1())
            {
                var newUserObject = new User
                {
                    EmailAddress = newUser.EmailAddress,
                    Password = newUser.Password,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Username = newUser.Username,
                    RoleId = 1,
                    CreatedOn = DateTime.Now,
                    CreatedBy = newUser.Username,
                    IsActive = true
                };
                db.Users.Add(newUserObject);
                db.SaveChanges();
                return Json(new {Success = true});
            }
        }
    }
}