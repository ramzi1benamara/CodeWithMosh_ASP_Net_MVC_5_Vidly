using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            return Ok(_dbContext.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>)
            );
        }

        //GET /api/customers/[id]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _dbContext.Customers.Single(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _dbContext.Customers.Add(customer);

            _dbContext.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri($"{Request.RequestUri}/{customerDto.Id}"), customerDto);
        }

        //PUT /api/Customers/[id]
        [HttpPut]
        public IHttpActionResult UpdateCustomer(CustomerDto customerDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            _dbContext.SaveChanges();

            return Ok(customerDto);
        }

        //DELETE /api/customers/[id]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();


            _dbContext.Customers.Remove(customerInDb);

            _dbContext.SaveChanges();

            return Ok(Mapper.Map<Customer, CustomerDto>(customerInDb));
        }
    }
}
