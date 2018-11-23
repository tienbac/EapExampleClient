using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EapExampleClient.Models;

namespace EapExampleClient.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly string SERVER = "https://eapexample20181123102939.azurewebsites.net/";

        private readonly string SERVER_EMPLOYEE_URI = "api/employees";

        private readonly string SERVER_SEARCH_URI = "api/search/";

        private static List<Employee> list;
        private static List<Employee> list2;
        // GET: Employees
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVER);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(SERVER_EMPLOYEE_URI);
                list = await response.Content.ReadAsAsync<List<Employee>>();

            }
            return View(list);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,Salary,Department,Email")] Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVER);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync(SERVER_EMPLOYEE_URI, employee);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }         
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,Salary,Department,Email")] Employee employee)
        {
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVER);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(SERVER_SEARCH_URI + "/" + searchString);
                list = await response.Content.ReadAsAsync<List<Employee>>();
            }
            
            return RedirectToAction("Index", list);
        }
    }
}
