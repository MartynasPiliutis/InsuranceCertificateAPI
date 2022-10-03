namespace InsuranceCertificates.Interfaces
{
    public interface ICustomerRepository
    {
        public int CheckCustomerAge(DateTime dateOfBirth);
    }
}
