using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using finalfreelancing.Data;
using finalfreelancing.Models;
using finalfreelancing.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using finalfreelancing.Migrations;

namespace finalfreelancing.Controllers
{
    public class FreelancersController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly SignInManager<User> _signInManager;
        //private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;


        public FreelancersController(AppDbContext context,
            //   SignInManager<User> signInManager,
            // UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment)
           // RoleManager<IdentityRole> roleManager)
        {
            _context = context;
         //   _signInManager = signInManager;
          //  _userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
          //  this.roleManager = roleManager;
        }

        // GET: Freelancers
        public async Task<IActionResult> Index()
        {
              return _context.freelancers != null ? 
                          View(await _context.freelancers.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.freelancers'  is null.");
            
        }

        // GET: Freelancers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.freelancers == null)
            {
                return NotFound();
            }

            var freelancer = await _context.freelancers
                .FirstOrDefaultAsync(m => m.FreelancerId == id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return View(freelancer);
        }

        // GET: Freelancers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Freelancers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FreelancerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var freelancer = new Freelancer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Career = model.Career,
                    Exp = model.Exp,
                    Pic = await UploadImag("Images/", model.Pic!, webHostEnvironment.WebRootPath),
                };
               
                _context.Add(freelancer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        //public async Task<IActionResult> Create(FreelancerViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new User
        //        {
        //            Name = model.Name,
        //            Email = model.Email,
        //            Career = model.Career,
        //            Exp = model.Exp,
        //            Pic = await UploadImag("Images/", model.Pic!, webHostEnvironment.WebRootPath),
        //            UserName = model.Email

        //        };
        //        await _userManager.CreateAsync(user,"");

        //        //var u = _userManager.FindByEmailAsync(User.Identity!.Name!).Result;
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //}
        public async Task<string> UploadImag(string folderPath, IFormFile file, string WebRootPath)
        {
            folderPath += $"{Guid.NewGuid().ToString()}_{file.FileName}";
            string serverFolder = Path.Combine(WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        // GET: Freelancers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.freelancers == null)
            {
                return NotFound();
            }

            var freelancer = await _context.freelancers.FindAsync(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FreelancerId,Pic,Name,Exp,Career,Rating,Email")] Freelancer freelancer)
        {
            if (id != freelancer.FreelancerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(freelancer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreelancerExists(freelancer.FreelancerId))
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
            return View(freelancer);
        }

        // GET: Freelancers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.freelancers == null)
            {
                return NotFound();
            }

            var freelancer = await _context.freelancers
                .FirstOrDefaultAsync(m => m.FreelancerId == id);
            if (freelancer == null)
            {
                return NotFound();
            }

            return View(freelancer);
        }

        // POST: Freelancers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.freelancers == null)
            {
                return Problem("Entity set 'AppDbContext.freelancers'  is null.");
            }
            var freelancer = await _context.freelancers.FindAsync(id);
            if (freelancer != null)
            {
                _context.freelancers.Remove(freelancer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FreelancerExists(int id)
        {
          return (_context.freelancers?.Any(e => e.FreelancerId == id)).GetValueOrDefault();
        }
    }
}
