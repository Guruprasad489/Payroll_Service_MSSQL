using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NetEmployeePayroll
{
    public class EmpRepository
    {
        public static string connectionstring = "Data Source=GURUPRASAD;Initial Catalog=Payroll_Service;Integrated Security=True";
        SqlConnection connection = null;

        public void GetAllEmployees()
        {
            try
            {
                using (connection = new SqlConnection(connectionstring))
                {
                    EmployeeModel model = new EmployeeModel();
                    string query = "select * from Employee_Payroll";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    connection.Open();
                    var reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Id = Convert.ToInt32(reader["Id"] == DBNull.Value ? default : reader["Id"]);
                            model.Name = Convert.ToString(reader["Name"] == DBNull.Value ? default : reader["Name"]);
                            model.Salary = Convert.ToDouble(reader["Salary"] == DBNull.Value ? default : reader["Salary"]);
                            model.Startdate = (DateTime)(reader["Startdate"] == DBNull.Value ? default : reader["Startdate"]);
                            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
                            model.Phone = Convert.ToInt64(reader["Phone"] == DBNull.Value ? default : reader["Phone"]);
                            model.Address = Convert.ToString(reader["Address"] == DBNull.Value ? default : reader["Address"]);
                            model.Department = Convert.ToString(reader["Department"] == DBNull.Value ? default : reader["Department"]);
                            model.Basic_Pay = Convert.ToDouble(reader["Basic_Pay"] == DBNull.Value ? default : reader["Basic_Pay"]);
                            model.Deductions = Convert.ToDouble(reader["Deductions"] == DBNull.Value ? default : reader["Deductions"]);
                            model.Taxable_Pay = Convert.ToDouble(reader["Taxable_Pay"] == DBNull.Value ? default : reader["Taxable_Pay"]);
                            model.Income_Tax = Convert.ToDouble(reader["Income_Tax"] == DBNull.Value ? default : reader["Income_Tax"]);
                            model.Net_Pay = Convert.ToDouble(reader["Net_Pay"] == DBNull.Value ? default : reader["Net_Pay"]);
                            Console.WriteLine($"{model.Id}, {model.Name}, {model.Salary}, {model.Startdate}, {model.Gender}, {model.Phone}, {model.Address}, {model.Department}, {model.Basic_Pay}, {model.Deductions}, {model.Taxable_Pay}, {model.Income_Tax}, {model.Net_Pay} \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows present");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    connection.Close();
            //}
        }

        //UC 5 - Method to retrieve all employees from particular date range 
        public void GetAllEmployeesWithDataAdapter(string query)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataSet);
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Console.WriteLine(dataRow["Id"] + ", " + dataRow["Name"] + ", " + dataRow["Salary"] + ", " + dataRow["StartDate"] + ", " + dataRow["Gender"] + ", " + dataRow["Phone"] + ", " + dataRow["Address"] + ", " + dataRow["Department"] + ", " + dataRow["Basic_Pay"] + ", " + dataRow["Deductions"] + ", " + dataRow["Taxable_Pay"] + ", " + dataRow["Income_Tax"] + ", " + dataRow["Net_Pay"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //UC7- Method To Add Employee details    
        public void AddEmployee(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("spAddEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Salary", obj.Salary);
                command.Parameters.AddWithValue("@Gender", obj.Gender);
                command.Parameters.AddWithValue("@Startdate", obj.Startdate);
                command.Parameters.AddWithValue("@Phone", obj.Phone);
                command.Parameters.AddWithValue("@Department", obj.Department);
                command.Parameters.AddWithValue("@Address", obj.Address);
                command.Parameters.AddWithValue("@Basic_Pay", obj.Basic_Pay);
                command.Parameters.AddWithValue("@Deductions", obj.Deductions);
                command.Parameters.AddWithValue("@Taxable_Pay", obj.Taxable_Pay);
                command.Parameters.AddWithValue("@Income_Tax", obj.Income_Tax);
                command.Parameters.AddWithValue("@Net_Pay", obj.Net_Pay);
                command.Parameters.AddWithValue("@Dept_Id", obj.Dept_Id);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Employee details added successfully");
                }
                else
                {
                    Console.WriteLine("Failed to add employee details");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Method To Update Employee details    
        public EmployeeModel UpdateEmployee(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("spUpdateEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Salary", obj.Salary);
                command.Parameters.AddWithValue("@Basic_Pay", obj.Salary);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Employee details updated successfully");
                    return obj;
                }
                else
                {
                    Console.WriteLine("Failed to update employee details");
                    return default;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
            finally
            {
                connection.Close();
            }
        }

        //Method To Delete Employee details    
        public void DeleteEmployee(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("spDeleteEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@Name", obj.Name);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Employee details deleted successfully");
                }
                else
                {
                    Console.WriteLine("Failed to deleted employee details");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //UC8 - Insert details Into Two Tables
        public void InsertIntoTwoTables(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                SqlCommand command = new SqlCommand("spInsertIntoTwoTables", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Gender", obj.Gender);
                command.Parameters.AddWithValue("@Address", obj.Address);
                command.Parameters.Add("Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                var result = command.ExecuteScalar();
                string id = command.Parameters["Id"].Value.ToString();
                int newId = Convert.ToInt32(id);

                string query = $"Insert into Payroll_Details(EmpId, Salary) values({newId},7894)";
                SqlCommand cmd = new SqlCommand(query, connection);
                int res = cmd.ExecuteNonQuery();
                if (res != 0)
                {
                    Console.WriteLine("Inserted into salary table successfully");
                }
                else
                {
                    Console.WriteLine("Failed to insert into salary table");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Refactor UC8 using transactional query
        public void InsertIntoTwoTablesWithTransactions()
        {
            using (connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                SqlTransaction sqlTran = connection.BeginTransaction();                //Start a local transaction
                SqlCommand command = connection.CreateCommand();                       //Enlist a command in the current transaction
                command.Transaction = sqlTran;

                try
                {
                    //Execute two seperate commands
                    command.CommandText = $"insert into employee_payroll(Name, Salary, Address) values('Rahul', '36000', 'Blr')";
                    command.ExecuteScalar();
                    command.CommandText = "insert into Payroll_Details(EmpId, Salary) values(18, 36000)";
                    int res = command.ExecuteNonQuery();
                    if (res != 0)
                    {
                        //commit transaction
                        sqlTran.Commit();
                        Console.WriteLine("Both queries successfull");
                    }
                }
                catch (Exception ex)
                {
                    //Handle the exception if transaction fails to commit
                    Console.WriteLine(ex.Message);
                    try
                    {
                        //Attempt to rollback transaction
                        sqlTran.Rollback();
                    }
                    catch (Exception exRollBack)
                    {
                        //Throws an invalidOperationexception if the connection is closed or the transaction has already been rolled back on the server
                        Console.WriteLine(exRollBack.Message);
                    }
                }
            }
        }
    }
}
