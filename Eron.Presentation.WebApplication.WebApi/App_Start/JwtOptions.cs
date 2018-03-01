using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

namespace Eron.Presentation.WebApplication.WebApi
{
    public class JwtOAuthOptions : OAuthAuthorizationServerOptions
    {
        public JwtOAuthOptions()
        {
            TokenEndpointPath = new PathString("/api/Token");
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60);
            AccessTokenFormat = new CustomJwtFormat(this);
            Provider = new JwtOAuthProvider();
#if DEBUG
            AllowInsecureHttp = true;
#endif
        }
    }

    public class CustomJwtOptions : JwtBearerAuthenticationOptions
    {
        public CustomJwtOptions()
        {
            var issuer = "localhost";
            var audience = "all";
            var key = Convert.FromBase64String("UHxNtYMRYwvfpO1dS5pWLKL0M2DgOj40EbN4SoBWgfcE"); ;

            AllowedAudiences = new[] { audience };
            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(issuer, key), 
            };
        }
    }
}