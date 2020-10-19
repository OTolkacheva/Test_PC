using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Test_PC.Db;
using Test_PC.EF;
using Test_PC.Models;
using Test_PC.Service;

namespace Test_PC.Controllers
{
    public partial class HomeController : Controller
    {
        IRepository db;

        public HomeController()
        {
            db = new DbReposytory();
        }

        public ActionResult Index()
        {
            var r = new Request();
            return View(r);
        }

        public ActionResult GetData(Request r)
        {
            if (ModelState.IsValid)
            {
                r.BaseAddress = db.GetBaseAddressById(r.BaseAddressId);
                if (db.SaveRequst(r.AllRequest))
                    return PartialView("Success", new Message { TextMessage = "Запрос был сохренен в БД" });
                else
                    return PartialView("Warning", new Message { TextMessage = "Такой запрос уже существует в БД" });
            }
            else
            {
                var errors = ModelState.Values.Where(e => e.Errors.Count > 0).SelectMany(e => e.Errors).Select(e => e.ErrorMessage).Distinct().Select(x => new Message { TextMessage = x }).ToList();
                return PartialView("Error", errors);
            }
        }

        public ActionResult SendRequest()
        {
            var notdonereq = db.GetNotDoneRequst();
            if (notdonereq.Count > 0)
            {
                var apiService = new ApiService();
                var LRR = new List<ViewRequestResponse>();
                foreach (var req in notdonereq)
                {
                    var users = apiService.Send<User>(req.Request);
                    db.SaveRequstData(req, DateTime.Now);
                    db.SaveUsers(users, req.Request);
                    LRR.Add(new ViewRequestResponse { Request = req.Request, LUs = users });
                }
                apiService.Dispose();
                return PartialView("RequestResponse", LRR);
            }
            else
                return PartialView("Warning", new Message { TextMessage = "Нет неотправленных запросов в БД" });
        }

        public ActionResult ViewDoneRequest()
        {
            return View(new ViewDate());
        }

        public ActionResult GetDoneRequest(ViewDate viewDate = null)
        {
            if (viewDate.NeedDate != false)
            {
                if (ModelState.IsValid)
                {
                    var donereq = db.GetDoneRequst(Convert.ToDateTime(viewDate.StartDate), Convert.ToDateTime(viewDate.FinalDate));
                    if (donereq.Count > 0)
                        return PartialView("GetRequest", donereq);
                    else
                        return PartialView("Warning", new Message { TextMessage = "Нет данных в БД" });
                }
                else
                {
                    var errors = ModelState.Values.Where(e => e.Errors.Count > 0).SelectMany(e => e.Errors).Select(e => e.ErrorMessage).Distinct().Select(x => new Message { TextMessage = x }).ToList();
                    return PartialView("Error", errors);
                }
            }
            else
            {
                var donereq = db.GetDoneRequst();
                if (donereq.Count > 0)
                    return PartialView("GetRequest", donereq);
                else
                    return PartialView("Warning", new Message { TextMessage = "Нет данных в БД" });
            }
        }

        public ActionResult ViewNotDoneRequest()
        {
            return View();
        }

        public ActionResult GetNotDoneRequest()
        {
            var notdonereq = db.GetNotDoneRequst();
            if (notdonereq.Count > 0)
                return PartialView("GetRequest", notdonereq);
            else
                return PartialView("Warning", new Message { TextMessage = "Нет данных в БД" });
        }

        public ActionResult ViewUserOnRequest()
        {
            return View(new SearchByRequest());
        }

        public ActionResult GetUserOnRequest(SearchByRequest r)
        {
            if (ModelState.IsValid)
            {
                if (r.NeedName)
                {
                    var ListUs = db.GetUserByRequest(r.RequestId, r.Name);
                    if(ListUs.Count > 0)
                        return PartialView("GetUser", ListUs);
                    else
                        return PartialView("Warning", new Message { TextMessage = "Нет данных в БД" });
                }
                else
                    return PartialView("GetUser", db.GetUserByRequest(r.RequestId));
            }
            else
            {
                var errors = ModelState.Values.Where(e => e.Errors.Count > 0).SelectMany(e => e.Errors).Select(e => e.ErrorMessage).Distinct().Select(x => new Message { TextMessage = x }).ToList();
                return PartialView("Error", errors);
            }
        }

        public ActionResult ViewAllUsers()
        {
            return View(new ViewSearchUser());
        }

        public ActionResult GetAllUsers(ViewSearchUser viewSearchUser = null)
        {
            if (viewSearchUser.NeedName)
            {
                if (ModelState.IsValid)
                {
                    var users = db.GetAllUsers(viewSearchUser.Name);
                    if (users.Count > 0)
                        return PartialView("GetUser", users);
                    else
                        return PartialView("Warning", new Message { TextMessage = "Нет данных в БД" });
                }
                else
                {
                    var errors = ModelState.Values.Where(e => e.Errors.Count > 0).SelectMany(e => e.Errors).Select(e => new Message { TextMessage = e.ErrorMessage }).Distinct().ToList();
                    return PartialView("Error", errors);
                }
            }
            else
            {
                var users = db.GetAllUsers();
                if (users.Count > 0)
                    return PartialView("GetUser", users);
                else
                    return PartialView("Warning", new Message { TextMessage = "Нет данных БД" });
            }
        }

        public ActionResult ViewRequestOnUser()
        {
            return View(new SearchByUser());
        }

        public ActionResult GetRequestOnUser(SearchByUser user)
        {
            if (ModelState.IsValid)
            {
                if (user.NeedDate)
                {
                    var ListReq = db.GetRequestByUser(user.UserId, Convert.ToDateTime(user.StartDate), Convert.ToDateTime(user.FinalDate));
                    if (ListReq.Count > 0)
                        return PartialView("GetRequest", ListReq);
                    else
                        return PartialView("Warning", new Message { TextMessage = "Нет данных в БД" });
                }
                else
                {
                    return PartialView("GetRequest", db.GetRequestByUser(user.UserId));
                }
            }
            else
            {
                var errors = ModelState.Values.Where(e => e.Errors.Count > 0).SelectMany(e => e.Errors).Select(e => e.ErrorMessage).Distinct().Select(x=> new Message { TextMessage = x }).ToList();
                return PartialView("Error", errors);
            }
        }

        public ActionResult AllServices()
        {
            return View(db.GetServiceSettings());
        }

        public ActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateService(ServiceSetting service)
        {
            if (ModelState.IsValid)
            {
                db.CreateService(service.BaseAddress);
                return RedirectToAction("AllServices");
            }

            return View(service);
        }

        public ActionResult EditService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceSetting service = db.GetServiceSettingById(Convert.ToInt32(id));
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost]
        public ActionResult EditService(ServiceSetting service)
        {
            if (ModelState.IsValid)
            {
                db.UpdateServiceSetting(service);
                return RedirectToAction("AllServices");
            }
            return View(service);
        }

        public ActionResult DeleteService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceSetting service = db.GetServiceSettingById(Convert.ToInt32(id));
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost, ActionName("DeleteService")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteServiceSetting(id);
            return RedirectToAction("AllServices");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}