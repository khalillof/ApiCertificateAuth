using System.Security.Cryptography.X509Certificates;

namespace ApiCertificateAuth
{
    public interface IX509CertificateAuthValidation
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
    public class X509CertificateAuthValidation : IX509CertificateAuthValidation
    {              
        private readonly string[] validThumbprints = new[]
              {
            "E1C8B70E73FDBF6A7230E0C48D793EE37118E168", "D7CBC1CBAE8FB83C17661EF4398C18DB5375260A"
              };
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            // for prod shoud use encripted password svc or vault for both certificate and password
            //var certificate = new X509Certificate2(Path.Combine("example_cert.pfx"), "Kh@123456");
            //return clientCertificate.Thumbprint == certificate.Thumbprint;
            return validThumbprints.Contains(clientCertificate.Thumbprint);
            
        }
    }


}
