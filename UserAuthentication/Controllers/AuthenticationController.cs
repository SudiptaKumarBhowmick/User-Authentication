using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using UserAuthentication.Models;
using UserAuthentication.Models.Application_Models;

namespace UserAuthentication.Controllers
{
    public class AuthenticationController : Controller
    {
        private DB_Entities _db = new DB_Entities();
        // GET: Home
        public ActionResult Home()
        {
            if (Session["idUser"] != null)
            {
                GetUserProfileDetails();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists.";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();

                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().idUser;
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {

            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public ActionResult UserProfile()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult SaveUserProfile(
            string email,
            string address,
            string phone_number,
            string mobile,
            string profession,
            string website,
            string github,
            string facebook,
            string twitter,
            string instagram,
            string profilePhoto
            )
        {
            if (profilePhoto != "")
            {
                string jpegFormat = "data:image/jpeg;base64,";
                char separator = ',';
                string[] rawImage = profilePhoto.Split(separator);
                if (rawImage.Count() > 1)
                    profilePhoto = jpegFormat + rawImage[1];
            }
            var message = "Successfully Updated.";
            var findUser = _db.UserProfiles.FirstOrDefault(s => s.Email == email);
            if (findUser == null)
            {
                var user_profile = new UserProfile()
                {
                    Email = email,
                    Address = address,
                    PhoneNumber = phone_number,
                    Mobile = mobile,
                    Profession = profession,
                    Website = website,
                    Github = github,
                    Facebook = facebook,
                    Twitter = twitter,
                    Instagram = instagram,
                    ProfilePicture = profilePhoto
                };
                _db.UserProfiles.Add(user_profile);
                _db.SaveChanges();
                return Json(message, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            else
            {
                findUser.Address = address;
                findUser.PhoneNumber = phone_number;
                findUser.Mobile = mobile;
                findUser.Profession = profession;
                findUser.Website = website;
                findUser.Github = github;
                findUser.Facebook = facebook;
                findUser.Twitter = twitter;
                findUser.Instagram = instagram;
                findUser.ProfilePicture = profilePhoto;
                _db.SaveChanges();
                return Json(message, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
        }

        public void GetUserProfileDetails()
        {
            string email = "";
            if (Session["Email"] != null)
            {
                email = Session["Email"].ToString();
            }
            var findEmail = _db.UserProfiles.FirstOrDefault(s => s.Email == email);
            if (findEmail != null)
            {
                var user_profile_details = (from user in _db.Users
                 join profile in _db.UserProfiles on user.Email equals profile.Email
                 where user.Email == email
                 select new UserProfileModel()
                 {
                     FullName = user.FirstName + " " + user.LastName,
                     Email = profile.Email,
                     Address = profile.Address,
                     PhoneNumber = profile.PhoneNumber,
                     Mobile = profile.Mobile,
                     Profession = profile.Profession,
                     Website = profile.Website,
                     Github = profile.Github,
                     Twitter = profile.Twitter,
                     Instagram = profile.Instagram,
                     Facebook = profile.Facebook,
                     ProfilePicture = profile.ProfilePicture
                 }).ToList();

                UserProfileModel userProfileModel = new UserProfileModel();
                for (int i = 0; i < user_profile_details.Count(); i++)
                {
                    Session["FullName"] = user_profile_details[i].FullName;
                    Session["Email"] = user_profile_details[i].Email;
                    Session["Address"] = user_profile_details[i].Address;
                    Session["PhoneNumber"] = user_profile_details[i].PhoneNumber;
                    Session["Mobile"] = user_profile_details[i].Mobile;
                    Session["Profession"] = user_profile_details[i].Profession;
                    Session["Website"] = user_profile_details[i].Website;
                    Session["Github"] = user_profile_details[i].Github;
                    Session["Twitter"] = user_profile_details[i].Twitter;
                    Session["Instagram"] = user_profile_details[i].Instagram;
                    Session["Facebook"] = user_profile_details[i].Facebook;
                    ViewBag.ProfilePicture = user_profile_details[i].ProfilePicture;
                }
            }

        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}