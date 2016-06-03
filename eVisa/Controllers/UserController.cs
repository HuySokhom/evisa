using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace eVisa.Controllers
{
    public class UserController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();

        static string language = "";
        //
        // GET: /User/
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("FQLogin");
            }
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "User Account";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }

        //
        // GET: /User/Profile
        public ActionResult Profile()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("FQLogin");
            }
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "User Account";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }

        //
        // GET: /User/History
        public ActionResult History()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("FQLogin");
            }
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "User Account";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }

        //
        // GET: /User/Change
        public ActionResult Change()
        {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "User Account";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }


        // POST: /User/SignUp 
        [HttpPost]        
        public ActionResult SignUp(Users data, HttpPostedFileBase file)
        {
            Users u = new Users();

            u.surname = data.surname;
            u.contact_name = data.contact_name;
            u.country = data.country;
            u.country_issue = data.country_issue;
            u.create_date = DateTime.Now;
            u.modified = DateTime.Now;
            u.dob = data.dob;
            u.given_name = data.given_name;
            u.heard_from = data.heard_from;
            u.middle_name = data.middle_name;
            u.nationality = data.nationality;
            u.passport_expire = data.passport_expire;
            u.passport_issue = data.passport_issue;
            u.passport_no = data.passport_no;
            u.phone_number = data.phone_number;
            u.photo = data.photo;
            u.primary_email = data.primary_email;
            u.residential_address = data.residential_address;
            u.secondary_email = data.secondary_email;
            u.sex = data.sex;
            u.user_id = data.user_id;
            u.password = Crypto.Hash(data.password); 

            db.Users.Add(u);
            db.SaveChanges();

            /* start send email */

            MailMessage mail = new MailMessage();
            mail.To.Add("huysokhom@yahoo.com");
            mail.From = new MailAddress("kom.huy@gmail.com");
            mail.Subject = "Hello There";
            string Body = "Hello my friend!";
            mail.Body = Body;
            mail.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            ("kom.huy@gmail.com", "04061992");// Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }

        // POST: LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Success()
        {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "Search Reference Number.";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult LoginAction(Users model)
        {
            var user = new UserManager();
            if (user.IsValid(model.user_id, model.password))
            {
                var ident = new ClaimsIdentity( new[] { 
                    // adding following 2 claim just for supporting default antiforgery provider
                    new Claim(ClaimTypes.NameIdentifier, model.user_id),
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                    new Claim(ClaimTypes.Name, model.user_id),

                    // optionally you could add roles if any
                    //new Claim(ClaimTypes.Role, "RoleName"),
                    //new Claim(ClaimTypes.Role, "AnotherRole"),
                },
                DefaultAuthenticationTypes.ApplicationCookie);
                
                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            // invalid username or password
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FQLogin()
        {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "Your application description page.";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }

        public ActionResult FQSignup()
        {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "Your application description page.";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            return View();
        }

        /** 
         * Functionality for Image Upload with POST HTTP
         */
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadImage()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    int i = checkValidExtension(_ext);
                    if ( i == 0 )
                    {
                        return Json(Convert.ToString("Invalid image type."), JsonRequestBehavior.AllowGet);
                    }

                    /* validate if image size is bigger than 2MB */
                    decimal size = (pic.ContentLength / 1024) / 1024;
                    if( size > 2 ){
                        return Json(
                            Convert.ToString("Invalid image size. image should be smaller than 2MB."), 
                            JsonRequestBehavior.AllowGet
                        );
                    }

                    // rename file
                    _imgname = Guid.NewGuid().ToString();

                    var _comPath = Server.MapPath("/Uploads/Users/") + _imgname + _ext;
                    _imgname = _imgname + _ext;

                    ViewBag.Msg = _comPath;

                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    /*
                     * to resizing image uncommand
                     * 
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                    img.Resize(200, 200);
                    img.Save(_comPath);
                     
                     * 
                     */
                    // end resize 
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }


        public int checkValidExtension(string ext)
        {
            string[] AllowedExtensions = new string[] { ".gif", ".jpeg", ".jpg", ".png" };
            for (int i = 0; i < AllowedExtensions.Length; i++)
            {
                if (ext == AllowedExtensions[i])
                    return 1;
            }

            return 0;
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion
    }
}