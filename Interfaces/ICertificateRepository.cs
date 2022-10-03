using InsuranceCertificates.Domain;
using InsuranceCertificates.Models;

namespace InsuranceCertificates.Interfaces
{
    public interface ICertificateRepository
    {
        public DateTime GetCertificateEndDate(DateTime certificateDate);
        public string CreteNewCertificateNumber(int id);
        public decimal GetCertificateInsuranceSum(decimal itemPrice);
        public int GetNewCertificateId();
        public void CreateNewCertrificate(CertificateModel certificateModel);
        public bool CheckNewCertificateRequirements(CertificateModel certificateModel);
    }
}
