using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Eron.Presentation.WebApiApplication.Providers;
using Microsoft.Owin.Security.OAuth;

namespace Eron.Presentation.WebApiApplication
{
    public class OAuthAuthorizationConfig : ApplicationOAuthProvider
    {
        public OAuthAuthorizationConfig(string publicClientId) : base(publicClientId)
        {
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.OwinContext.Request.Method == "OPTIONS" && context.IsTokenEndpoint)
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "POST" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "accept", "authorization", "content-type", "Access-Control-Allow-Origin" });
                context.OwinContext.Response.StatusCode = 200;
                context.RequestCompleted();

                return Task.FromResult<object>(null);
            }

            return base.MatchEndpoint(context);
        }


    }
}