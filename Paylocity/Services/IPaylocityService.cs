using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paylocity.Models;
using Paylocity.ViewModels;
using Paylocity.Data;

namespace Paylocity.Services
{
    public interface IPaylocityService
    {
        Task<List<EmployeeVM>> GetEmployees();
        Task<List<Dependent>> GetEmployeeDependents(int empID);
        Task<List<EmployeeCostsVM>> GetAllCosts();
        Task<Employee> UpsertEmployee(Employee employee);
        Task<Dependent> UpsertDependent(Dependent dependent);
        Task<Employee> GetEmployeeByID(int empID);
        Task<List<CalculationParameter>> GetAllCalculationParameters();
        Task<List<CalculationParameter>> UpsertCalculationParameters(List<CalculationParameter> paramList);

    }
}
