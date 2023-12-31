﻿using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using PagedList;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StaffController : Controller
    {
        DlctContext db = new DlctContext();
        private readonly HttpClient _httpClient;
        public StaffController()
        {
            _httpClient = new HttpClient();
        }

        //View List

        public async Task<IActionResult> Index()
        {
            var apiResponse = await _httpClient.GetAsync("https://localhost:7109/api/AdminApi/");
            
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();
                var staff = JsonConvert.DeserializeObject<List<Staff>>(responseContent);
                return View(staff);
            }
            else
            {
                var staffList = await db.Staff
                    .Include(s => s.Branch)
                    .Include(s => s.RoleId)
                    .ToListAsync();
                return View(staffList);
            }
        }


        public IActionResult Chat()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Staff");
            }

            var username = HttpContext.Session.GetString("Username");
            var avatar = HttpContext.Session.GetString("Avatar");

            ViewBag.Username = username;
            ViewBag.Avatar = avatar;

            return View();
        }

        [HttpGet]
        public IActionResult Sendmail(int staffId)
        {
            TempData["StaffId"] = staffId;
            ViewBag.StaffId = staffId; 
            return View();
        }

        [HttpPost]
        public IActionResult Sendmail(Mails model)
        {
            int staffId;

            if (TempData.ContainsKey("StaffId") && TempData["StaffId"] != null)
            {
                staffId = (int)TempData["StaffId"];
                TempData.Keep("StaffId");
            }
            else
            {
                return RedirectToAction("Error");
            }

            var staffMember = db.Staff.FirstOrDefault(s => s.StaffId == staffId);

            if (staffMember != null)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Admin", "huynhhiepvan1998@gmail.com"));
                message.Subject = model.Subject;
                message.Body = new TextPart("plain")
                {
                    Text = model.Content
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("huynhhiepvan1998@gmail.com", "nmqt ljyf skbz xcrs");

                    message.To.Add(new MailboxAddress(staffMember.Name, staffMember.Email));

                    client.Send(message);

                    client.Disconnect(true);
                }

                TempData["StaffId"] = staffId;
            }

            return View();
        }

        [HttpGet]
        public IActionResult showPassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult showPassword(string username)
        {
            var staff = db.Staff.FirstOrDefault(s => s.Username == username);
            if (staff != null)
            {
                TempData["Password"] = staff.Password;
                return RedirectToAction("showPassword");
            }
            ModelState.AddModelError("", "Username not found.");
            return View();
        }

        //button choose image
        [HttpPost]
        public IActionResult ProcessUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json("No file uploaded");
            }
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Json("/images/" + fileName);
        }

        //login
        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(Staff staff)
        {
            var UserName = Request.Form["Username"].ToString();
            var Password = Request.Form["Password"].ToString();
            var Name = Request.Form["Name"].ToString();
            Staff nv = db.Staff.FirstOrDefault(x => x.Username == UserName);
            if (nv != null)
            {
                if (nv.Status == true)
                {
                    var passwordHasher = new PasswordHasher<Staff>();
                    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(nv, nv.Password, Password);
                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("Username", nv.Username);
                        if(nv.Avatar != null)
                        {
                            HttpContext.Session.SetString("Avatar", nv.Avatar);
                        }
                        HttpContext.Session.SetString("UserId", nv.StaffId.ToString());
                        HttpContext.Session.SetString("Role", nv.RoleId.ToString());
                        HttpContext.Session.SetString("Name", nv.Name);
                        string returnUrl = HttpContext.Session.GetString("ReturnUrl");

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            HttpContext.Session.Remove("ReturnUrl");

                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewData["ErrorPass"] = "sai mật khẩu hoặc Tên đăng nhập không tồn tại vui lòng nhập lại";
                    }
                }
                else
                {
                    ViewData["ErrorAccount"] = "Tài khoản chưa được kích hoạt. Vui lòng liên hệ quản trị viên.";
                }
            }
            else
            {
                ViewData["ErrorAccount"] = "sai mật khẩu hoặc Tên đăng nhập không tồn tại vui lòng nhập lại";
            }
            return View("Login");
        }

        //register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                HttpContext.Session.SetString("ReturnUrl", Url.Action("Index", "Staff"));

                return RedirectToAction("Login", "Staff");
            }
            var roles = db.Roles.ToList();
            var branches = db.Branches.ToList();
            ViewBag.Roles = new SelectList(roles, "RoleId", "Name");
            ViewBag.Branches = new SelectList(branches, "BranchId", "Address");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Staff registrationModel)
        {
            var apiUrl = "https://localhost:7109/api/AdminApi/register";

            if (string.IsNullOrEmpty(registrationModel.Name) && string.IsNullOrEmpty(registrationModel.Username)
                && string.IsNullOrEmpty(registrationModel.Password) && string.IsNullOrEmpty(registrationModel.Phone)
                && string.IsNullOrEmpty(registrationModel.Address)  && string.IsNullOrEmpty(registrationModel.Email) 
                && string.IsNullOrEmpty(registrationModel.Phone))
            {
                ModelState.AddModelError("Name", "cannot be empty.");
            }

            else
            {
                var eRegex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!])(?!.*\s).{8,32}$");
                var phoneRegex = new Regex(@"^(03|05|07|08|09|01[2|6|8|9])(?!84)[0-9]{8}$");
                if (registrationModel.Phone != null && (!phoneRegex.IsMatch(registrationModel.Phone) || registrationModel.Phone.Length > 10))
                {
                    ModelState.AddModelError("Phone", "Invalid Vietnamese phone number");
                }
                if (registrationModel.Password != null && !eRegex.IsMatch(registrationModel.Password))
                {
                    ModelState.AddModelError("Username", "Invalid password format.");
                }
            }

            if (string.IsNullOrEmpty(Request.Form["BranchId"]))
            {
                ModelState.AddModelError("BranchId", "Branch is required.");
            }

            if (string.IsNullOrEmpty(Request.Form["RoleId"]))
            {
                ModelState.AddModelError("RoleId", "Role is required.");
            }

            if (ModelState.IsValid)
            {
                string createdByUserName = HttpContext.Session.GetString("Name");

                registrationModel.CreatedBy = createdByUserName;
                registrationModel.Status = Request.Form["Status"] == "true";
                var json = JsonConvert.SerializeObject(registrationModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("API Response Content: " + responseContent);

                    dynamic errorResponse = JsonConvert.DeserializeObject(responseContent);

                    if (errorResponse != null)
                    {
                        ModelState.AddModelError("", errorResponse.Message);

                        if (errorResponse.Errors != null)
                        {
                            foreach (var error in errorResponse.Errors)
                            {
                                ModelState.AddModelError("", error.ToString());
                            }
                        }
                    }

                    var roles = db.Roles.ToList();
                    var branches = db.Branches.ToList();
                    ViewBag.Roles = new SelectList(roles, "RoleId", "Name");
                    ViewBag.Branches = new SelectList(branches, "BranchId", "Address");

                    return View(registrationModel);
                }
            }
            else
            {
                var roles = db.Roles.ToList();
                var branches = db.Branches.ToList();

                ViewBag.Roles = new SelectList(roles, "RoleId", "Name");
                ViewBag.Branches = new SelectList(branches, "BranchId", "Address");

                return View(registrationModel);
            }
        }

        ////Delete
        public async Task<IActionResult> Delete(int staffId)
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                HttpContext.Session.SetString("ReturnUrl", Url.Action("Index", "Staff"));

                return RedirectToAction("Login", "Staff");
            }

            var staff = await db.Staff.FirstOrDefaultAsync(s => s.StaffId == staffId);
            if (staff == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7109/api/AdminApi/delete/{staffId}";
            var response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                staff.IsDisabled = true;
                staff.Status = false;
                db.Entry(staff).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response Content: " + responseContent);

                var errorResponse = JsonConvert.DeserializeObject<object>(responseContent);

                ModelState.AddModelError("", errorResponse.ToString());
                return RedirectToAction("Index");
            }
        }

        //reloadEmlpoyeee
        [HttpGet]
        public async Task<IActionResult> ReloadEmployee(int staffId)
        {
            var apiUrl = $"https://localhost:7109/api/AdminApi/reload/{staffId}";
            var response = await _httpClient.PutAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response Content: " + responseContent);

                var errorResponse = JsonConvert.DeserializeObject<object>(responseContent);

                ModelState.AddModelError("", errorResponse.ToString());
                return RedirectToAction("Index");
            }
        }

        // Search
        public async Task<IActionResult> SearchStaff(string keyword)
        {
            List<Staff> staffList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7109/");
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    var response = await client.GetAsync("api/AdminApi"); 
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        staffList = JsonConvert.DeserializeObject<List<Staff>>(responseContent);
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    var response = await client.GetAsync($"api/AdminApi/search?keyword={keyword}");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        staffList = JsonConvert.DeserializeObject<List<Staff>>(responseContent);
                    }
                    else
                    {
                        return View("Index");
                    }
                }
            }
            return View("Index", staffList);
        }

        [HttpGet]
        public IActionResult Edit(int staffId)
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                HttpContext.Session.SetString("ReturnUrl", Url.Action("Index", "Staff"));

                return RedirectToAction("Login", "Staff");
            }
            var staff = db.Staff
              .Include(s => s.Scheduledetails)
              .ThenInclude(s => s.Schedule)
              .FirstOrDefault(s => s.StaffId == staffId);

            if (staff == null)
            {
                return NotFound();
            }

            var roles = db.Roles.ToList();
            var branches = db.Branches.ToList();

            ViewBag.Roles = new SelectList(roles, "RoleId", "Name");
            ViewBag.Branches = new SelectList(branches, "BranchId", "Address");

            return View(staff);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int staffId, Staff updateModel)
        {
            if (!ModelState.IsValid)
            {
                var roles = db.Roles.ToList();
                var branches = db.Branches.ToList();

                ViewBag.Roles = new SelectList(roles, "RoleId", "Name");
                ViewBag.Branches = new SelectList(branches, "BranchId", "Address");

                return View(updateModel);
            }

            var phoneRegex = new Regex(@"^(03|05|07|08|09|01[2|6|8|9])(?!84)[0-9]{8}$");
            if (!phoneRegex.IsMatch(updateModel.Phone) || updateModel.Phone.Length > 10)
            {
                ModelState.AddModelError("Phone", "Số điện thoại không hợp lệ");
                return View(updateModel);
            }
            updateModel.Status = Request.Form["Status"] == "true";

            var apiUrl = $"https://localhost:7109/api/AdminApi/update/{staffId}";

            var json = JsonConvert.SerializeObject(updateModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var updatedStaff = await db.Staff.FirstOrDefaultAsync(s => s.StaffId == staffId);
                    if (updatedStaff != null)
                    {
                        string editorName = HttpContext.Session.GetString("Name");
                        updatedStaff.UpdatedBy = editorName;

                        db.Entry(updatedStaff).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the editor's name: " + ex.Message);
                }
                return RedirectToAction("Index");
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response Content: " + responseContent);

                var errorResponse = JsonConvert.DeserializeObject<object>(responseContent);
                var roles = db.Roles.ToList();
                var branches = db.Branches.ToList();

                ViewBag.Roles = new SelectList(roles, "RoleId", "Name");
                ViewBag.Branches = new SelectList(branches, "BranchId", "Address");

                ModelState.AddModelError("", errorResponse.ToString());
                return View(updateModel);
            }
        }

        //detail
        [HttpGet]
        public async Task<IActionResult> Detail(int staffId)
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                HttpContext.Session.SetString("ReturnUrl", Url.Action("Index", "Staff"));

                return RedirectToAction("Login", "Staff");
            }
            var apiUrl = $"https://localhost:7109/api/AdminApi/detail/{staffId}";

            var apiResponse = await _httpClient.GetAsync(apiUrl);
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseContent = await apiResponse.Content.ReadAsStringAsync();
                var StaffDetail = JsonConvert.DeserializeObject<Staff>(responseContent);

                return View(StaffDetail);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Avatar");
            HttpContext.Session.Remove("Role");

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
