using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> _customers;

        public CustomersController()
        {
            _customers = new List<Customer>()
            {
//                new Customer(){Id = 1, Name = "Sophart"},
//                new Customer(){Id = 2, Name = "Lakkhena"},
            };
        }
        public ActionResult Index()
        {
            return View(_customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}