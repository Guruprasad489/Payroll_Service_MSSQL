using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeePayroll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll service program");

            EmpRepository empRepository = new EmpRepository();
            
            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter the Program number to get executed \n0.Exit \n1.Get All employees \n2.Add Data to Table \n3.Update Employee");
                    int option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            empRepository.GetAllEmployees();
                            Console.WriteLine("\nGet all employees with data adapter :");
                            empRepository.GetAllEmployeesWithDataAdapter();
                            break;
                        case 2:
                            EmployeeModel obj = new EmployeeModel
                            {
                                Name = "ABD",
                                Salary = 20000.00,
                                Startdate = DateTime.Now,
                                Gender = 'M',
                                Phone = 912423,
                                Department = "Account",
                                Address = "Bangalore",
                                Basic_Pay = 5000.00,
                                Deductions = 1000.00,
                                Taxable_Pay = 300.00,
                                Income_Tax = 400.00,
                                Net_Pay = 25000,
                                Dept_Id = 3
                            };
                            empRepository.AddEmployee(obj);
                            break;
                        case 3:
                            EmployeeModel model = new EmployeeModel();
                            Console.WriteLine("Enter id of employee whose data you want to update");
                            model.Id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter new name");
                            model.Name = Console.ReadLine();
                            Console.WriteLine("Enter new salary");
                            model.Salary = Convert.ToDouble(Console.ReadLine());
                            var result = empRepository.UpdateEmployee(model);
                            Console.WriteLine("Id: " +result.Id+ ", Name: " +result.Name+ " has updated with Salary: " +result.Salary);
                            break;
                        default:
                            Console.WriteLine("Please Enter Correct option");
                            break;
                    }
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
