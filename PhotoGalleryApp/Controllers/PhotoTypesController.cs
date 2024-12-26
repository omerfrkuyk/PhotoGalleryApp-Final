﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace PhotoGalleryApp.Controllers
{

    //Way 2:
    [Authorize (Roles = "Admin")]
    public class PhotoTypesController : MvcController
    {
        // Service injections:
        private readonly IPhotoTypesService _photoTypesService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public PhotoTypesController(
			IPhotoTypesService photoTypesService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _photoTypesService = photoTypesService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: PhotoTypes

        //Way1 

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _photoTypesService.Query().ToList();
            return View(list);
        }

        // GET: PhotoTypes/Details/5
        public IActionResult Details(int id)
        {

            // Get item service logic:
            var item = _photoTypesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: PhotoTypes/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: PhotoTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoTypesModel photoTypes)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _photoTypesService.Create(photoTypes.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = photoTypes.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(photoTypes);
        }

        // GET: PhotoTypes/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _photoTypesService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: PhotoTypes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PhotoTypesModel photoTypes)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _photoTypesService.Update(photoTypes.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = photoTypes.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(photoTypes);
        }

        // GET: PhotoTypes/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _photoTypesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: PhotoTypes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _photoTypesService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
