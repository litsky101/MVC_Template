using MVC_Template.Model;
using MVC_Template.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Template.Controller
{
    public class EmployeeController
    {
        MySQLHelper conn = null;
        AppInterface view;
        string connString = Properties.Settings.Default.DB;

        public EmployeeController(AppInterface _view)
        {
            view = _view;
            view.SetController(this);
        }

        public int Save()
        {
            try
            {
                int result = 0;
                var mySqlParam = new Dictionary<string, object>()
                {
                    { "@lname", view.LastName },
                    { "@fname", view.FirstName },
                    { "@mname", view.MiddleName },
                    { "@gender", view.Gender }
                };

                using (conn = new MySQLHelper(connString))
                {
                    conn.BeginTransaction();//begin transaction

                    conn.ArgSQLCommand = new StringBuilder(@"INSERT INTO employee (LastName, FirstName, MiddleName, Gender) 
                    VALUES(@lname, @fname, @mname, @gender);");
                    conn.ArgSQLParam = mySqlParam;

                    result = conn.ExecuteMySQL();

                    conn.CommitTransaction();//save changes
                }

                return result;
            }
            catch
            {

                throw;
            }
        }

        public Employee Find()
        {
            try
            {
                Employee emp = new Employee();

                var mySqlParam = new Dictionary<string, object>()
                {
                    { "@id", view.SearchKey }
                };

                using (conn = new MySQLHelper(connString))
                {
                    conn.ArgSQLCommand = new StringBuilder(@"SELECT id, lastname, firstname, middlename, gender, active FROM employee WHERE id = @id");
                    conn.ArgSQLParam = mySqlParam;

                    using (var dr = conn.MySQLReader())
                    {
                        while (dr.Read())
                        {
                            emp.ID = Convert.ToInt32(dr["id"]);
                            emp.LastName = dr["lastname"].ToString();
                            emp.FirstName = dr["firstname"].ToString();
                            emp.MiddleName = dr["middlename"].ToString();
                            emp.Gender = dr["gender"].ToString();
                            emp.Status = Convert.ToBoolean(dr["active"]);
                        }
                    }
                }

                return emp;

            }
            catch 
            {

                throw;
            }
        }

        public int Update()
        {
            try
            {
                int result = 0;
                var mySqlParam = new Dictionary<string, object>()
                {
                    { "@id", view.ID },
                    { "@lname", view.LastName },
                    { "@fname", view.FirstName },
                    { "@mname", view.MiddleName },
                    { "@gender", view.Gender },
                    { "@status", view.Status }
                };

                using (conn = new MySQLHelper(connString))
                {
                    conn.BeginTransaction();//begin transaction

                    conn.ArgSQLCommand = new StringBuilder(@"UPDATE employee SET LastName = @lname, FirstName = @fname, MiddleName = @mname, 
                        Gender = @gender, Active = @status WHERE ID = @Id;");
                    conn.ArgSQLParam = mySqlParam;

                    result = conn.ExecuteMySQL();

                    conn.CommitTransaction();//save changes
                }

                return result;
            }
            catch
            {

                throw;
            }
        }
    }
}
