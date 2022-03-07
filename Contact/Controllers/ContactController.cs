using Contact.Data;
using Contact.Models.ViewModels;
using Contact.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Contact.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactDbContext _dbContext;

        public ContactController(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ActionResult Index()
        {
            var contacts = _dbContext.Contacts.ToList();
            ViewBag.Contacts = contacts;
            return View();
        }

        public JsonResult GetContactList()
        {
            List<ContactViewModel> contacts = _dbContext.Contacts.Select(c => new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                JobTitle = c.JobTitle,
                BirthDate = c.BirthDate
            }).ToList();

            return Json(contacts);
        }

        public JsonResult GetContactById(int contactId)
        {
            UserContact contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == contactId);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(contact, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value);
        }

        public JsonResult SaveContactInDatabase(ContactViewModel model)
        {
            bool result = false;
            try
            {
                if(model.Id > 0)
                {
                    UserContact contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == model.Id);
                    contact.Name = model.Name;
                    contact.PhoneNumber = model.PhoneNumber;
                    contact.JobTitle = model.JobTitle;
                    contact.BirthDate = model.BirthDate;
                    _dbContext.SaveChanges();
                    result = true;
                }
                else
                {
                    UserContact contact = new UserContact
                    {
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        JobTitle = model.JobTitle,
                        BirthDate = model.BirthDate
                    };
                    _dbContext.Contacts.Add(contact);
                    _dbContext.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Json(result);
        }

        public JsonResult DeleteContactRecord(int contactId)
        {
            bool result = false;
            UserContact contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == contactId);
            if(contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
                result = true;
            }
            return Json(result);
        }       

    }
}
