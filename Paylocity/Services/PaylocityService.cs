using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paylocity.Models;
using Paylocity.ViewModels;
using Paylocity.Data;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Paylocity.Services
{
    public class PaylocityService : IPaylocityService
    {
        private HttpClient _apiClient;
        public PaylocityService(HttpClient httpClient, NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
        {
            _apiClient = httpClient;
        }

        /// <summary>
        /// Gets list of employees
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeVM>> GetEmployees()
        {
            List<EmployeeVM> result = new List<EmployeeVM>();
            try
            {
                result = await _apiClient.GetJsonAsync<List<EmployeeVM>>("api/Paylocity/GetEmployees/");
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }


        /// <summary>
        /// Gets list of dependents by employeeid
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public async Task<List<Dependent>> GetEmployeeDependents(int empid)
        {
            List<Dependent> result = new List<Dependent>();
            try
            {
                result = await _apiClient.GetJsonAsync<List<Dependent>>($"api/Paylocity/GetEmployeeDependents/{empid}");
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }


        /// <summary>
        /// Gets list of all employees and dependents and their costs
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeCostsVM>> GetAllCosts()
        {
            List<EmployeeCostsVM> result = new List<EmployeeCostsVM>();
            try
            {
                result = await _apiClient.GetJsonAsync<List<EmployeeCostsVM>>("api/Paylocity/GetAllCosts/");
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }

        /// <summary>
        /// Saves Dependent
        /// </summary>
        /// <returns></returns>
        public async Task<Dependent> UpsertDependent(Dependent dependent)
        {
            Dependent result = new Dependent();
            try
            {
                result = await _apiClient.PostJsonAsync<Dependent>("api/Paylocity/UpsertDependent/", dependent);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }

        /// <summary>
        /// Saves Employee
        /// </summary>
        /// <returns></returns>
        public async Task<Employee> UpsertEmployee(Employee employee)
        {
            Employee result = new Employee();
            try
            {
                result = await _apiClient.PostJsonAsync<Employee>("api/Paylocity/UpsertEmployee/", employee);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }

        /// <summary>
        /// Gets list of dependents by employeeid
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByID(int empid)
        {
            Employee result = new Employee();
            try
            {
                result = await _apiClient.GetJsonAsync<Employee>($"api/Paylocity/GetEmployeeByID/{empid}");
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }

        /// <summary>
        /// Gets list of all calculation parameters
        /// </summary>
        /// <returns></returns>
        public async Task<List<CalculationParameter>> GetAllCalculationParameters()
        {
            List<CalculationParameter> result = new List<CalculationParameter>();
            try
            {
                result = await _apiClient.GetJsonAsync<List<CalculationParameter>>("api/Paylocity/GetAllCalculationParameters/");
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }

        /// <summary>
        /// Saves calculation parameters
        /// </summary>
        /// <returns></returns>
        public async Task<List<CalculationParameter>> UpsertCalculationParameters(List<CalculationParameter> paramList)
        {
            List<CalculationParameter> result = new List<CalculationParameter>();
            try
            {
                result = await _apiClient.PostJsonAsync<List<CalculationParameter>>("api/Paylocity/UpsertCalculationParameters/", paramList);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                throw;
            }
            return result;
        }


    }
}
