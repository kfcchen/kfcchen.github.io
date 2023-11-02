using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _1101664.Models;
using System.Net;
using System.Data.Entity;
using System.Web.Security;
using PagedList;

namespace _1101664.Controllers
{
    [Authorize]
    public class FinalController : Controller
    {
        SongEntities song = new SongEntities();
        loginEntities log = new loginEntities();
        AllroleEntities allrole = new AllroleEntities();
        int pagesize = 5;
        
        public ActionResult Index()
        {
            ViewBag.name = User.Identity.Name;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register([Bind(Include = "account,password,name,email,phone,address")] Tablelogins1101664 user)
        {
            if (ModelState.IsValid)
            {
                log.Tablelogins1101664.Add(user);
                log.SaveChanges();
                return RedirectToAction("Member");
            }
            return View(user);
        }
        public ActionResult Song(int page = 1)
        {
            //var member = log.Tablelogins1101664.ToList();
            //return View(member);
            int currentPage = pagesize < 1 ? 1 : page;
            var lists = song.Tablesongs1101664.OrderBy(m => m.Id).ToList();
            var result = lists.ToPagedList(currentPage, pagesize);
            return View(result);
        }
        public ActionResult CreateS()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateS([Bind(Include = "Id,songname,singer,author,time")] Tablesongs1101664 emp2)
        {
            if (ModelState.IsValid)
            {
                song.Tablesongs1101664.Add(emp2);
                song.SaveChanges();
                return RedirectToAction("Song");
            }
            return View(emp2);
        }

        public ActionResult DeleteS(int? Id)
        {
            if (Id == null)
            {
                return Content("查無此資料，請提供員工編號!");
            }
            Tablesongs1101664 emp2 = song.Tablesongs1101664.Find(Id);
            if (emp2 == null)
            {
                return HttpNotFound();
            }
            return View(emp2);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteS(int Id)
        {
            Tablesongs1101664 emp2 = song.Tablesongs1101664.Find(Id);
            song.Tablesongs1101664.Remove(emp2);
            song.SaveChanges();
            return RedirectToAction("Song");
        }
        public ActionResult EditS(int? Id)
        {
            if (Id == null)
            {
                return Content("查無此資料，請提供員工編號!");
            }
            Tablesongs1101664 emp2 = song.Tablesongs1101664.Find(Id);
            if (emp2 == null)
            {
                return HttpNotFound();
            }
            return View(emp2);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditS([Bind(Include = "Id,songname,singer,author,time")] Tablesongs1101664 emp2)
        {
            if (ModelState.IsValid)//判斷資料是否通過驗證
            {
                //emp這個entity狀態設為modified
                //SaveChanges執行時，會向sql server發出update命令
                song.Entry(emp2).State = EntityState.Modified;
                song.SaveChanges();
                return RedirectToAction("Song");
            }
            return View(emp2);
        }

        public ActionResult Member(int page = 1)
        {
            //var member = log.Tablelogins1101664.ToList();
            //return View(member);
            int currentPage = pagesize < 1 ? 1 : page;
            var lists = log.Tablelogins1101664.OrderBy(m => m.account).ToList();
            var result = lists.ToPagedList(currentPage, pagesize);
            return View(result);
        }
        public ActionResult CreateM()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateM([Bind(Include = "account,password,name,email,phonenumber,address")] Tablelogins1101664 emp1)
        {
            if (ModelState.IsValid)
            {
                log.Tablelogins1101664.Add(emp1);
                log.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(emp1);
        }

        public ActionResult DeleteM(int? account)
        {
            if (account == 0)
            {
                return Content("查無此資料，請提供員工編號!");
            }
            Tablelogins1101664 emp1 = log.Tablelogins1101664.Find(account);
            if (emp1 == null)
            {
                return HttpNotFound();
            }
            return View(emp1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteM(int account)
        {
            Tablelogins1101664 emp1 = log.Tablelogins1101664.Find(account);
            log.Tablelogins1101664.Remove(emp1);
            log.SaveChanges();
            return RedirectToAction("Member");
        }

        public ActionResult EditM(int? account)
        {
            if (account == null)
            {
                return Content("查無此資料，請提供員工編號!");
            }
            Tablelogins1101664 emp1 = log.Tablelogins1101664.Find(account);
            if (emp1 == null)
            {
                return HttpNotFound();
            }
            return View(emp1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditM([Bind(Include = "account,password,name,email,phonenumber,address")] Tablelogins1101664 emp1)
        {
            if (ModelState.IsValid)//判斷資料是否通過驗證
            {
                //emp這個entity狀態設為modified
                //SaveChanges執行時，會向sql server發出update命令
                log.Entry(emp1).State = EntityState.Modified;
                log.SaveChanges();
                return RedirectToAction("Member");
            }
            return View(emp1);
        }

        public ActionResult DetailsM(int? account)
        {
            //ViewBag.id = Id;
            if (account == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tablelogins1101664 emp1 = log.Tablelogins1101664.Find(account);
            if (emp1 == null)
            {
                return HttpNotFound();
            }
            return View(emp1);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            
            FormsAuthentication.SignOut();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(int? account, String password)
        {
            FormsAuthentication.SignOut();
            if (account == null && password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Qresult = log.Tablelogins1101664.Where(m => m.account == account && m.password == password);

            if (Qresult.Count() == 0)
            {
                ViewBag.Err = "帳號或密碼錯誤!";
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(password, true);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); //登出
            return RedirectToAction("Login");
        }
        public ActionResult Twelve()
        {
            return View();
        }
        public ActionResult Twelve2()
        {
            return View();
        }
        public ActionResult Ninecard()
        {
            List<Card> card = new List<Card>
            {
                new Card{Name="岩柱-悲鳴嶼行冥",Brief="",Photo="A.jpg"},
                new Card{Name="炎柱-煉獄杏壽郎",Brief="",Photo="B.jpg"},
                new Card{Name="水柱-富岡義勇",Brief="",Photo="C.jpg"},
                new Card{Name="音柱-宇髓天元",Brief="",Photo="D.jpg"},
                new Card{Name="蟲柱-蝴蝶忍",Brief="",Photo="E.jpg"},
                new Card{Name="風柱-不死川實彌",Brief="",Photo="F.jpg"},
                new Card{Name="霞柱-時透無一郎",Brief="",Photo="G.jpg"},
                new Card{Name="戀柱-甘露寺蜜璃",Brief="",Photo="H.jpg"},
                new Card{Name="蛇住-伊黑小巴內",Brief="",Photo="I.jpg"},
            };
            return View(card);
        }
        public ActionResult AllRole(int page = 1)
        {
            int currentPage = pagesize < 1 ? 1 : page;
            var lists = allrole.TableAllroles1101664.OrderBy(m => m.Id).ToList();
            var result = lists.ToPagedList(currentPage, pagesize);
            return View(result);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,category,age,height,ability,attribute")] TableAllroles1101664 emp)
        {
            if (ModelState.IsValid)
            {
                allrole.TableAllroles1101664.Add(emp);
                allrole.SaveChanges();
                return RedirectToAction("AllRole");
            }
            return View(emp);
        }
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableAllroles1101664 emp = allrole.TableAllroles1101664.Find(Id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == 0)
            {
                return Content("查無此資料，請提供員工編號!");
            }
            TableAllroles1101664 emp = allrole.TableAllroles1101664.Find(Id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,category,age,height,ability,attribute")] TableAllroles1101664 emp)
        {
            if (ModelState.IsValid)//判斷資料是否通過驗證
            {
                //emp這個entity狀態設為modified
                //SaveChanges執行時，會向sql server發出update命令
                allrole.Entry(emp).State = EntityState.Modified;
                allrole.SaveChanges();
                return RedirectToAction("AllRole");
            }
            return View(emp);
        }
        public ActionResult Delete(int? Id)
        {
            if (Id == 0)
            {
                return Content("查無此資料，請提供員工編號!");
            }
            TableAllroles1101664 emp = allrole.TableAllroles1101664.Find(Id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            TableAllroles1101664 emp = allrole.TableAllroles1101664.Find(Id);
            allrole.TableAllroles1101664.Remove(emp);
            allrole.SaveChanges();
            return RedirectToAction("AllRole");
        }

        public ActionResult QueryM()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyQueryM(String name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var QresultM = log.Tablelogins1101664.Where(m => m.name == name);
            /*Where(m => m.Name.Contains(Name))/Where(m => m.Name == Name)*/
            return View(QresultM.ToList());
        }
        public ActionResult Query()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyQuery(String ability)
        {
            if (ability == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Qresult = allrole.TableAllroles1101664.Where(m => m.ability == ability);
            /*Where(m => m.Name.Contains(Name))/Where(m => m.Name == Name)*/

            return View(Qresult.ToList());
        }

        public ActionResult Inquire()
        {
            return View();
        }

        public ActionResult SelectQuery()
        {
            var QresultAR = allrole.TableAllroles1101664.ToList();
            return View(QresultAR);
        }
        [HttpPost]
        public ActionResult MySelQuery(String name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var QresultAR = allrole.TableAllroles1101664.Where(m => m.name.ToString() == name);

            return View(QresultAR.ToList());
        }

       
    }
}