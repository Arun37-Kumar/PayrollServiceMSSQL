using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PayrollServiceMSSQL
{
    class EmployeeRepo
    {

        /// <summary>
        /// UC1 Connection to server
        /// </summary>
        public static string connectionString = @"Data Source=DESKTOP-IMDHCGD\SQLEXPRESS;Initial Catalog=Payroll_Service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        SqlConnection connection = null;
        public void GetAllEmployees()
        {
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    EmployeeModel model = new EmployeeModel();
                    string query = "select * from employee_payroll";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Id = Convert.ToInt32(reader["Id"] == DBNull.Value ? default : reader["Id"]);
                            model.Name = Convert.ToString(reader["Name"] == DBNull.Value ? default : reader["Name"]);
                            model.Basic_Pay = Convert.ToDouble(reader["Basic_Pay"] == DBNull.Value ? default : reader["Basic_Pay"]);
                            model.Startdate = (DateTime)(reader["Startdate"] == DBNull.Value ? default : reader["Startdate"]);
                            model.Gender = Convert.ToChar(reader["Gender"] == DBNull.Value ? default : reader["Gender"]);
                            model.Phone = Convert.ToInt64(reader["Phone"] == DBNull.Value ? default : reader["Phone"]);
                            model.Address = Convert.ToString(reader["Address"] == DBNull.Value ? default : reader["Address"]);
                            model.Department = Convert.ToString(reader["Department"] == DBNull.Value ? default : reader["Department"]);
                            model.Deductions = Convert.ToDouble(reader["Deductions"] == DBNull.Value ? default : reader["Deductions"]);
                            model.Taxable_Pay = Convert.ToDouble(reader["Taxable_Pay"] == DBNull.Value ? default : reader["Taxable_Pay"]);
                            model.Income_Tax = Convert.ToDouble(reader["Income_Tax"] == DBNull.Value ? default : reader["Income_Tax"]);
                            model.Net_Pay = Convert.ToDouble(reader["Net_Pay"] == DBNull.Value ? default : reader["Net_Pay"]);
                            Console.WriteLine($"{model.Id}, {model.Name}, {model.Basic_Pay}, {model.Startdate}, {model.Gender}, {model.Phone}, {model.Address}, {model.Department}, {model.Deductions}, {model.Taxable_Pay}, {model.Income_Tax}, {model.Net_Pay} \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close(); //Require if we don't use using above 
            }
        }

        /// <summary>
        /// UC2 Adding employees
        /// </summary>
        /// <param name="obj"></param>
        public void AddEmployee(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("spAddEmployees", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", obj.Name);
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

        /// <summary>
        /// UC3 Updating the Employee
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateEmployee(EmployeeModel obj)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("spUpdateEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@Name", obj.Name);
                command.Parameters.AddWithValue("@Basic_Pay", obj.Basic_Pay);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Employee details updated successfully");
                }
                else
                {
                    Console.WriteLine("Failed to update employee details");
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


    }
}
