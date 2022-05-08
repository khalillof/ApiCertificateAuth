using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Claims;

namespace ApiCertificateAuth
{
    public static class CertificateAuthentication
    {
        public static void AddCertificateAuthetication(this IServiceCollection services)
        {
            // add  services
            //services.AddTransient<X509CertificateAuthValidation>();
            services.AddTransient<IX509CertificateAuthValidation, X509CertificateAuthValidation>();

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate(options =>
                {
                    //options.RevocationMode = X509RevocationMode.NoCheck;
                    options.AllowedCertificateTypes = CertificateTypes.All;

                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context =>
                        {
                        var validationService = context.HttpContext.RequestServices.GetRequiredService<IX509CertificateAuthValidation>();

                            if (validationService.ValidateCertificate(context.ClientCertificate))
                            {
                                var claims = new[]
                                {
                        new Claim(
                            ClaimTypes.NameIdentifier,
                            context.ClientCertificate.Subject,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer),
                        new Claim(
                            ClaimTypes.Name,
                            context.ClientCertificate.Subject,
                            ClaimValueTypes.String, context.Options.ClaimsIssuer)
                    };

                                context.Principal = new ClaimsPrincipal(
                                    new ClaimsIdentity(claims, context.Scheme.Name));
                                context.Success();
                            }
                            else
                            {
                                context.Fail("Invalid certificate");                                
                            }
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context => {
                            context.Fail("Invalid certificate");
                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddAuthorization();
        }
    }
}
