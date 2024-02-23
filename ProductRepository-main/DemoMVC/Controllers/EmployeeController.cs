using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;
using System.Data.SqlClient;
using Dapper;
using System.Data;
namespace DemoMVC.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee f)
        {
            string?  Name, DeptName,Gender;
            decimal Salary;
            bool isActive;
            DateTime dob;
            
            Name =f.Name;
            DeptName = f.DeptName;
            Gender = f.Gender;
            Salary = Convert.ToDecimal(f.Salary);
            if (f.active == "Active")
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            dob = Convert.ToDateTime(f.dob);
            string temp = Convert.ToString(dob);
            string[] temp1 = new string[2];
            temp1 = temp.Split(" ");
            string date = temp1[0];
            string connectionString = "Server=192.168.0.23,1427;Initial Catalog=interns;Integrated Security=False;user id=Interns;password=test;";
            string insertCmd = "Insert into EmployeeList (Name,DeptName,Gender,Salary,isActive,dob) Values (@Name,@DeptName,@Gender,@Salary,@isActive,@dob)";
            var values = new { Name = Name, DeptName = DeptName, Gender = Gender, Salary = Salary, isActive = isActive, dob = date};
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(insertCmd, values);
            }
            Console.WriteLine("Successful insertion of Employee details");
            ViewData["Message"] = "Successful insertion";
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
