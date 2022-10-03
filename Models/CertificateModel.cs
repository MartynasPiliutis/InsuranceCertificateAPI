namespace InsuranceCertificates.Models;

public class CertificateModel
{
    public string? Number { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public string? CustomerName { get; set; }

    public DateTime CustomerDateOfBirth { get; set; }

    public string? InsuredItem { get; set; }

    public decimal InsuredSum { get; set; }

    public decimal CertificateSum { get; set; }
}