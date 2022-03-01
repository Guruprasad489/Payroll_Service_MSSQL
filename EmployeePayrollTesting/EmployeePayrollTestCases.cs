using ADO.NetEmployeePayroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmployeePayrollTesting
{
    [TestClass]
    public class EmployeePayrollTestCases
    {
        EmpRepository empRepository;
        [TestInitialize]
        public void SetUp()
        {
            empRepository = new EmpRepository();
        }
        [TestMethod]
        [DataRow(7, "ABD", 45000.00, 45000.00)]
        [DataRow(10, "GHGhghj", 123, default)]
        public void TestUpdateEmployeeMethod(int id, string name, double salary, double expected)
        {
            EmployeeModel empModel = new EmployeeModel()
            {
                Id = id,
                Name = name,
                Salary = salary,
            };
            try
            {
                var actual = empRepository.UpdateEmployee(empModel);
                Assert.AreEqual(expected, actual.Salary);
            }
            catch(Exception ex)
            {
                string expecterError = "Object reference not set to an instance of an object.";
                Assert.AreEqual(expecterError, ex.Message);
            }
        }
    }
}
