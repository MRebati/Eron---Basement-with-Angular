using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

namespace Eron.Presentation.WebApplication.WebApi.Models
{
    public class OAuthGrantResourceOwnerCredentialsContextWithRecaptcha: OAuthGrantResourceOwnerCredentialsContext
    {
        public OAuthGrantResourceOwnerCredentialsContextWithRecaptcha(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            string clientId,
            string userName,
            string password,
            IList<string> scope) 
            : base(context, options, clientId, userName, password, scope)
        {
        }

        public string Recaptcha { get; set; }
    }
}