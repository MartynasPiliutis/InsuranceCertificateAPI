using System.Web;
using System.Net.Http;
using InsuranceCertificates.Domain;
using InsuranceCertificates.Interfaces;
using InsuranceCertificates.Data;
using InsuranceCertificates.Models;
using InsuranceCertificates.Pages;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsuranceCertificates.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(ILogger<CustomerRepository> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }
        public int CheckCustomerAge(DateTime dateOfBirth)
        {
            try
            {
                _logger.LogInformation("Calculationg customer age...");
                int age = new DateTime((DateTime.Now - dateOfBirth).Ticks).Year - 1;
                return age;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not check customer age!");
                _logger.LogError(ex.Message);
                return default;
            }

        }

    }
}
