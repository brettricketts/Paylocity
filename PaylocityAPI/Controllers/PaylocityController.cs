using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaylocityAPI.Data;
using PaylocityAPI.Models;
using PaylocityAPI.ViewModels;

namespace PaylocityAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PaylocityController : ControllerBase
    {
        private readonly PaylocityContext _appContext;

        public PaylocityController(PaylocityContext context)
        {
            _appContext = context;
        }

        /// <summary>
        /// Gets list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            List<Employee> result = new List<Employee>();
            try
            {
                using (var db = _appContext)
                {
                    result = db.Employee.Where(e => e.Active == true).ToList<Employee>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Gets list of dependents by EmployeeID
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        [HttpGet("{empID}")]
        public List<Dependent> GetEmployeeDependents(int empID)
        {
            List<Dependent> result = new List<Dependent>();
            try
            {
                using (var db = _appContext)
                {
                    result = db.Dependent.Where(d => d.EmployeeID == empID).ToList<Dependent>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }


        /// <summary>
        /// Gets employee by EmployeeID
        /// </summary>
        /// <param name="empID"></param>
        /// <returns></returns>
        [HttpGet("{empID}")]
        public Employee GetEmployeeByID(int empID)
        {
            Employee result = new Employee();
            try
            {
                using (var db = _appContext)
                {
                    result = db.Employee.Where(e => e.EmployeeID == empID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Gets list of all employees and dependents and their costs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<EmployeeCostsVM> GetAllCosts()
        {
            List<CalculationParameter> listCalcParams = new List<CalculationParameter>();
            List<EmployeeCostsVM> listEmployeeCostsVM = new List<EmployeeCostsVM>();
            List<EmployeeCostsVM> listDependentCosts = new List<EmployeeCostsVM>();
            List<EmployeeCostsVM> result = new List<EmployeeCostsVM>();
            try
            {
                using (var db = _appContext)
                {
                    listCalcParams = db.CalculationParameter.ToList<CalculationParameter>();
                    var employeebenefits = listCalcParams.Where(v => v.Parameter == "EmployeeBenefits").Select(s => s.Value).FirstOrDefault();
                    var dependentbenefits = listCalcParams.Where(v => v.Parameter == "DependentBenefits").Select(s => s.Value).FirstOrDefault();
                    var paycheckamount = listCalcParams.Where(v => v.Parameter == "PaycheckAmount").Select(s => s.Value).FirstOrDefault();
                    var paycheckcount = listCalcParams.Where(v => v.Parameter == "PaycheckCount").Select(s => s.Value).FirstOrDefault();
                    var discountpercent = listCalcParams.Where(v => v.Parameter == "DiscountPercent").Select(s => s.Value).FirstOrDefault()/100;

                    listEmployeeCostsVM = (from e in db.Employee
                                           where e.Active == true
                                         select new EmployeeCostsVM
                                         {
                                             EmployeeID = e.EmployeeID,
                                             FirstName = e.FirstName,
                                             LastName = e.LastName,
                                             IsDependent = false,
                                             BenefitsCost = employeebenefits - (employeebenefits * (e.LastName.Substring(0,1).ToUpper() == "A" ? discountpercent : 0)),
                                             Pay = paycheckamount * paycheckcount,
                                             Total = (paycheckamount * paycheckcount) - (employeebenefits - (employeebenefits * (e.LastName.Substring(0, 1).ToUpper() == "A" ? discountpercent : 0)))
                                         }
                                        ).ToList<EmployeeCostsVM>();

                    listDependentCosts = (from d in db.Dependent
                                          where d.Active == true
                                          select new EmployeeCostsVM
                                          {
                                              EmployeeID = d.EmployeeID,
                                              FirstName = d.FirstName,
                                              LastName = d.LastName,
                                              IsDependent = true,
                                              BenefitsCost = dependentbenefits - (dependentbenefits * (d.LastName.Substring(0, 1).ToUpper() == "A" ? discountpercent : 0)),
                                              Pay = 0,
                                              Total = -(dependentbenefits - (dependentbenefits * (d.LastName.Substring(0, 1).ToUpper() == "A" ? discountpercent : 0))),
                                          }
                                        ).ToList<EmployeeCostsVM>();

                    result = listEmployeeCostsVM.Union(listDependentCosts).ToList();
                }

            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }


            return result;
        }


        /// <summary>
        /// Inserts new employee into the database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public Employee UpsertEmployee([FromBody] Employee employee)
        {
            Employee result = null;
            try
            {
                using (var db = _appContext)
                {
                    _appContext.Employee.Update(employee);
                    _appContext.SaveChanges();
                    result = employee;
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Inserts new dependent into the database.
        /// </summary>
        /// <param name="dependent"></param>
        /// <returns></returns>
        [HttpPost]
        public Dependent UpsertDependent([FromBody] Dependent dependent)
        {
            Dependent result = null;
            try
            {
                using (var db = _appContext)
                {
                    _appContext.Dependent.Update(dependent);
                    _appContext.SaveChanges();
                    result = dependent;
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// Gets list of all calculation parameters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CalculationParameter> GetAllCalculationParameters()
        {
            List<CalculationParameter> paramslist = new List<CalculationParameter>();
            try
            {
                paramslist = _appContext.Set<CalculationParameter>().ToList();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

            return paramslist;
        }


        [HttpPost]
        public List<CalculationParameter> UpsertCalculationParameters([FromBody] List<CalculationParameter> paramList)
        {
            List<CalculationParameter> result = new List<CalculationParameter>();
            try
            {
                foreach (var item in paramList)
                {
                    _appContext.CalculationParameter.Update(item);
                    _appContext.SaveChanges();
                }
                result = paramList;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return result;
        }


    }
}
