using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using InsuranceCertificates.Pages;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCertificates.Interfaces
{
    public interface IErrorMessage
    {
        public Task<IActionResult> GetErrorMessage(string errorMessage);
    }
}
