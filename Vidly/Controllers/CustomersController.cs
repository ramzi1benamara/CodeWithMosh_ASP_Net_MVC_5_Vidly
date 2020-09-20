using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var customers = _dbContext.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            var membershipTypes = _dbContext.MembershipTypes.ToList();

            var viewModel = new CustomerViewModel()
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            if (customer == null)
                return HttpNotFound();

            return View("Form", viewModel);
        }

        public ActionResult New()
        {
            var membershipTypes = _dbContext.MembershipTypes.ToList();
            var viewModel = new CustomerViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("Form", viewModel);
        }

        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                _dbContext.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == customer.Id);

                if (customerInDb != null)
                {
                    customerInDb.Name = customer.Name;
                    customerInDb.BirthDate = customer.BirthDate;
                    customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                }
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}