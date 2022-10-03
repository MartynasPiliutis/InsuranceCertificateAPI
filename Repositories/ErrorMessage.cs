using InsuranceCertificates.Data;
using InsuranceCertificates.Interfaces;
using InsuranceCertificates.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsuranceCertificates.Repositories
{
    public class ErrorMessage : IErrorMessage
    {
        private readonly ILogger<ErrorMessage> _logger;
        public readonly ErrorModel _errorModel;

        public ErrorMessage(ILogger<ErrorMessage> logger, ErrorModel errorModel)
        {
            _logger = logger;
            _errorModel = errorModel;
        }

        [Route("api/Pages/intex.html")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public Task<IActionResult> GetErrorMessage(string errorMessage)
        {
            _errorModel.RequestId = errorMessage;
            return default;
        }
    }
}
