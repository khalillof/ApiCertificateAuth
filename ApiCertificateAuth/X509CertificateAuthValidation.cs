using System.Security.Cryptography.X509Certificates;

namespace ApiCertificateAuth
{
    public interface IX509CertificateAuthValidation
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
    public class X509CertificateAuthValidation : IX509CertificateAuthValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            // for prod shoud use encripted password svc or vault for both certificate and password
            var certificate = new X509Certificate2(Path.Combine("example_cert.pfx"), "Kh@123456");
            return clientCertificate.Thumbprint == certificate.Thumbprint;
        }
    }


}
