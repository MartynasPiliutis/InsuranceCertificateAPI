namespace InsuranceCertificates.Domain;

public class Certificate
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public Customer? Customer { get; set; }

    public string? InsuredItem { get; set; }

    public decimal InsuredSum { get; set; }

    public decimal CertificateSum { get; set; }
}