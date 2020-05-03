﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bothniabladet.Data;
using Bothniabladet.Models.ImageModels;
using System.Web;
using System.IO;
using Bothniabladet.Services;
using Microsoft.AspNetCore.Http;

namespace Bothniabladet.Controllers
{
    public class ImagesController : Controller
    {
        //possible to remove the AppDbContext once Services are fully implemented
        private readonly AppDbContext _context;
        public ImageService _service;
        public ImagesController(AppDbContext context, ImageService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Index()
        {
            var models = _service.GetImages();
            return View(models);
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var imageViewModel = _service.GetImageDetail(id);
            
            if (imageViewModel == null)
            {
                return NotFound();
            }
            ICollection<string> thumbnailDataStrings = new List<string>();

            foreach (EditedImage editedImage in imageViewModel.EditedImages) 
            {
                thumbnailDataStrings.Add(string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(editedImage.ImageData)));
            }
            //add thumbnail datastrings to the viewmodel, this whole process is ugly and the logic is in the wrong place. Refactor.
            imageViewModel.ThumbNailDataStrings = thumbnailDataStrings;
            //add image to viewbag
            ViewBag.ImageDataUrl = imageViewModel.ImageDataString;

            return View(imageViewModel);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View(new CreateImageCommand() { Sections = _service.GetSectionChoices() });  //retrive the Section choices from the database
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImageCommand cmd, string[] dynamicField)
        {
            cmd.Keywords = dynamicField;
            //conversions between images and bytearrays could be handled by a conversionservice to simplify the controller code
            using (var memoryStream = new MemoryStream())
            {
                
                await cmd.ImageData.FormFile.CopyToAsync(memoryStream); //get the image data from the formfile
                // Upload the file if less than 12ish MB
                if (memoryStream.Length < 12097152)
                {
                    cmd.ImageMemoryStream = memoryStream;   //add the image data to the command object
                    var id = _service.CreateImage(cmd);
                    return RedirectToAction("");    //make this redirect to the added image's Details page.
                    //return RedirectToAction(nameof(View), new { id = id });
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }

            //If we got to here, something went wrong
            return View(cmd);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,ImageTitle,ImageData,BasePrice,Issue,SectionPublished,Section,CreatedAt")] Image image)
        {
            if (id != image.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // POST: Images/AddEdited
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdited(ImageDetailViewModel viewModel)
        {
            //Refactor: I'm really not fond of how I'm doing this here, but don't really know how to pass only the command from the view
            AddEditedCommand cmd = viewModel.CreateEditedImage;
            //conversions between images and bytearrays could be handled by a conversionservice to simplify the controller code
            using (var memoryStream = new MemoryStream())
            {

                await cmd.ImageData.FormFile.CopyToAsync(memoryStream); //get the image data from the formfile as a stream
                // Upload the file if less than 12ish MB
                if (memoryStream.Length < 12097152)
                {
                    var id = _service.CreateEditedImage(cmd);
                    return RedirectToAction("");    //make this redirect to the added EditedImage's Details page.
                    //return RedirectToAction(nameof(View), new { id = id });
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }

            //If we got to here, something went wrong
            return View(cmd);

        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
