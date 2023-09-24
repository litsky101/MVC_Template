using MVC_Template.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Template.Model
{
    /// <summary>
    /// Use this interface to connect controller and view
    /// </summary>
    public interface AppInterface
    {
        int ID { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        bool Status { get; set; }
        string Gender { get; set; }
        string SearchKey { get;set; }

        void SetController(EmployeeController controller);
    }
}
