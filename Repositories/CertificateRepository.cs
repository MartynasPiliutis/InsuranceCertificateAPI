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
using InsuranceCertificates.Controllers;

namespace InsuranceCertificates.Repositories
{

    public class CertificateRepository : ICertificateRepository
    {
        private readonly ILogger<CertificateRepository> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly IErrorMessage _errorMessage;

        public CertificateRepository(ILogger<CertificateRepository> logger, AppDbContext appDbContext, ICustomerRepository customerRepository, IErrorMessage errorMessage)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _customerRepository = customerRepository;
            _errorMessage = errorMessage;
        }
        public void CreateNewCertrificate(CertificateModel certificateModel)
        {
            try
            {
                
                _logger.LogInformation("Collecting data...");
                _appDbContext.Certificates.Add(new Certificate()
                {
                    Number = CreteNewCertificateNumber(GetNewCertificateId()),
                    CreationDate = DateTime.UtcNow,
                    ValidFrom = DateTime.UtcNow,
                    ValidTo = DateTime.UtcNow.AddYears(1),
                    CertificateSum = GetCertificateInsuranceSum(certificateModel.InsuredSum),
                    InsuredItem = certificateModel.InsuredItem,
                    InsuredSum = certificateModel.InsuredSum,
                    Customer = new Customer()
                    {
                        Name = certificateModel.CustomerName,
                        DateOfBirth = certificateModel.CustomerDateOfBirth
                    }
                });
                _appDbContext.SaveChanges();
                _logger.LogInformation("Finishing certificate creation...");
            }
            catch (Exception ex)
            {
                _logger.LogError("Did not create new certificate!");
                _logger.LogError(ex.Message);
            }
        }

        public DateTime GetCertificateEndDate(DateTime certificateDate)
        {
            try
            {
                _logger.LogInformation($"Getting certificate end date...");
                DateTime endDate = new(certificateDate.AddYears(1).Year, certificateDate.Month, certificateDate.Day, 0, 0, 0);
                return endDate;
            }
            catch (Exception ex)
            {
                _logger.LogError("Did not get certificate end date!");
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public decimal GetCertificateInsuranceSum(decimal itemPrice)
        {
            try
            {
                _logger.LogInformation($"Getting certificate insurance sum...");
                decimal insuranceSum = 0;
                if (itemPrice >= 20 && itemPrice <= 50) return _ = 8;
                if (itemPrice > 50 && itemPrice <= 100) return _ = 15;
                if (itemPrice > 100 && itemPrice <= 200) return _ = 25;
                return insuranceSum;
            }
            catch (Exception ex)
            {
                _logger.LogError("Did not get certificate insurance sum!");
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public int GetNewCertificateId()
        {
            try
            {
                _logger.LogInformation($"Getting new certificate id...");
                int certificateId = _appDbContext.Certificates.Max(x => x.Id);
                certificateId++;
                return certificateId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Did not get new certificate id!");
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public string CreteNewCertificateNumber(int id)
        {
            try
            {
                _logger.LogInformation($"Generating new certificate number...");
                string certId = id.ToString();
                string certificateNumber = "";
                for (int i = 5 - certId.Length; i > 0; i--)
                {
                    certificateNumber += "0";
                }
                return certificateNumber += certId;
            }
            catch (Exception ex)
            {
                _logger.LogError("Certificate number was not ganerated!");
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public bool CheckNewCertificateRequirements(CertificateModel certificateModel)
        {
            try
            {
                _logger.LogInformation($"Checking requirements for new certificate...");
                int age = _customerRepository.CheckCustomerAge(certificateModel.CustomerDateOfBirth);
                if (age < 18 || certificateModel.InsuredSum < 20 || certificateModel.InsuredSum > 200)
                {
                    if (age < 18)
                    {
                        string errorMsgAge = $"Customer is {age} years old. Customer age does not meet certification requirements. Please try again...";
                        _errorMessage.GetErrorMessage($"{errorMsgAge}");
                    }
                    if (certificateModel.InsuredSum < 20 || certificateModel.InsuredSum > 200)
                    {
                        string errorMsgSum = $"Item value is {certificateModel.InsuredSum}. Item value does not meet certification requirements. Please try again...";
                        _errorMessage.GetErrorMessage($"{errorMsgSum}");
                    }
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                _errorMessage.GetErrorMessage($"Certificate requirements chack was not complete! {ex.Message}");

                return default;
            }
            
        }
    }
}
