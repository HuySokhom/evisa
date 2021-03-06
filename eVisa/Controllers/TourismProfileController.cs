﻿using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class TourismProfileController : Controller
    {

        private eVisaContext db = new eVisaContext();

        //
        // GET: /TourismProfile/
        public ActionResult Index()
        {
            BaseFunction bfn = new BaseFunction();

            ViewBag.Language = bfn.getLanguage(1);

            return View();
          
        }

        [HttpGet]
        public ActionResult Create() {

            return View();
        }

        [HttpPost]
        public ActionResult Create(TourismView viewModel) {

            TourismProfile entity = new TourismProfile();

            if (ModelState.IsValid)
            {

                //Save image
                var uploadImageFolder = "/Images/Tourism/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (db.Tourisms.Where(m => m.IsActive == 1).Any(m => m.Name == viewModel.Name))
                {
                    ModelState.AddModelError("Exist Photo ", "Wrong Formate");

                    return View();
                };

                if (!Directory.Exists(fullUploadImageFolder))
                {
                    Directory.CreateDirectory(fullUploadImageFolder);
                }

                var imageExtension = Path.GetExtension(viewModel.Photo.FileName);
                if (string.IsNullOrEmpty(imageExtension))
                {
                    ModelState.AddModelError("Photo", "Wrong Formate");
                    return View();
                }


                var uploadImagePath = Path.Combine(uploadImageFolder, viewModel.Name.Trim() + imageExtension);
                viewModel.Photo.SaveAs(Server.MapPath(uploadImagePath));

                viewModel.Url = uploadImagePath;
                entity.Name = viewModel.Name;
                entity.Tel = viewModel.Tel;
                entity.Website = viewModel.Website;
                entity.Content = viewModel.Content;
                entity.Email = viewModel.Email;
                entity.Fax = viewModel.Fax;
                entity.Language_Code = viewModel.Language_Code;
               
                entity.Url = uploadImagePath;
                entity.IsActive = 1;
                entity.CreatedBy = User.Identity.Name;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = User.Identity.Name;
            }
            else
            {
                ModelState.AddModelError("Photo", "Wrong Formate");
                return RedirectToAction("Index", "TourismProfile");
            }

          
            db.Tourisms.Add(entity);
            db.SaveChanges();
            return RedirectToAction("Index", "TourismProfile");
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();
            List<TourismView> list = new List<TourismView>();

            if (Id.HasValue)
            {
                var entity = db.Tourisms.Find(Id);

                var model = new TourismEditView
                {
                    Id = entity.Id,
                    Url = entity.Url,
                    Name = entity.Name,
                    Tel = entity.Tel,
                    Website = entity.Website,
                    Content = entity.Content,
                    Email = entity.Email,
                    Fax = entity.Fax,
                    Language_Code = entity.Language_Code
               
                };
                
                    //list.Add(new TourismView()
                    //{
                    //    Id = model.Id,
                    //    Url = bfn.ResolveServerUrl(model.Url,false),
                    //    Name = model.Name,
                    //    Tel = model.Tel,
                    //    Website = model.Website,
                    //    Content = model.Content,
                    //    Email = model.Email,
                    //    Fax = model.Fax,
                    //    Language_Code = model.Language_Code
                    //});
               
                return Json(model, JsonRequestBehavior.AllowGet);

            }
            var models = new MenuView
            {
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getMenuList(1)
            };

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(TourismEditView viewModel)
        {
            TourismProfile entity = new TourismProfile();

            if (ModelState.IsValid)
            {
                string url = "";
                entity = db.Tourisms.Find(viewModel.Id);

                var uploadImageFolder = "/Images/Tourism/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (viewModel.Photo == null)
                {
                    url = entity.Url;
                }
                else
                {

                    /* Delete Old Image */
                    var oldImagePath = Server.MapPath(entity.Url);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    /* new Image */
                    var imageExtension = Path.GetExtension(viewModel.Photo.FileName);
                    if (string.IsNullOrEmpty(imageExtension))
                    {
                        ModelState.AddModelError("Photo", "Wrong Formate");
                        return View();
                    }

                    var uploadImagePath = Path.Combine(uploadImageFolder, viewModel.Name + imageExtension);
                    viewModel.Photo.SaveAs(Server.MapPath(uploadImagePath));
                    url = uploadImagePath;
                }

                entity.Url = url;
                entity.Name = viewModel.Name;   
                entity.Tel = viewModel.Tel;   
                entity.Website = viewModel.Website;  
                entity.Content = viewModel.Content; 
                entity.Email = viewModel.Email;   
                entity.Fax = viewModel.Fax;
                entity.Language_Code = viewModel.Language_Code;
                entity.UpdatedBy = User.Identity.Name;
                entity.UpdatedDate = DateTime.Now;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "TourismProfile", new { lange = viewModel.Language_Code });
            }

            return RedirectToAction("Create", "MissionWorld");
        }

        public JsonResult getTourism(string code) {

            if (code != "")
            {
                var viewModel = db.Tourisms
                    .Where(e => e.Language_Code == code && e.IsActive == 1)
                    .Select(e => new TourismView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Url = e.Url,
                        Content = e.Content,
                        Tel = e.Tel,
                        Email = e.Email,
                        Fax = (e.Fax == null ? "" : e.Fax),
                        Website = e.Website,
                        Language_Code = e.Language_Code
                    })
                  .AsEnumerable();

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Tourisms.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            entity.UpdatedBy = User.Identity.Name;
            entity.IsActive = 0;
            db.Entry(entity).State = EntityState.Modified;
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }
	}
}