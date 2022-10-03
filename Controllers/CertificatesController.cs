using InsuranceCertificates.Data;
using InsuranceCertificates.Models;
using InsuranceCertificates.Interfaces;
using InsuranceCertificates.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InsuranceCertificates.Domain;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InsuranceCertificates.Controllers;

[ApiController]
[Route("[controller]")]
public class CertificatesController : ControllerBase
{
    private readonly ILogger<CertificatesController> _logger;
    private readonly AppDbContext _appDbContext;
    private readonly ICertificateRepository _certificateRepository;

    public CertificatesController(ILogger<CertificatesController> logger, AppDbContext appDbContext, ICertificateRepository certificateRepository)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _certificateRepository = certificateRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<CertificateModel>> Get()
    {
        return await _appDbContext.Certificates.Select(c => new CertificateModel
        {
            Number = c.Number,
            CreationDate = c.CreationDate,
            ValidFrom = c.ValidFrom,
            ValidTo = c.ValidTo,
            CustomerName = c.Customer.Name,
            CustomerDateOfBirth = c.Customer.DateOfBirth,
            InsuredItem = c.InsuredItem,
            InsuredSum = c.InsuredSum,
            CertificateSum = c.CertificateSum
        }).ToListAsync();
    }

    [HttpPost]
    public IActionResult Create(object list)
    {
        CertificateModel? certificateModel = JsonConvert.DeserializeObject<CertificateModel>(list.ToString());
        bool certificateValid = _certificateRepository.CheckNewCertificateRequirements(certificateModel);
        if (certificateValid)
        {
            _logger.LogInformation("Information is valid. Creating new certificate...");
            _certificateRepository.CreateNewCertrificate(certificateModel);
            _logger.LogInformation("New certificate created...");
            return Ok();
        }
        else return default;
    }
}