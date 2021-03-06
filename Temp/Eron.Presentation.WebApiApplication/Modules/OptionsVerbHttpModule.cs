﻿using System;
using System.Net;
using System.Web;

namespace Eron.Presentation.WebApiApplication.Modules
{
    public class OptionsVerbHttpModule: IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.BeginRequest +=
                (new EventHandler(this.Application_BeginRequest));
            application.EndRequest +=
                (new EventHandler(this.Application_EndRequest));
        }

        private void Application_BeginRequest(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            var header = context.Request.HttpMethod;
            if (header.ToLower().Contains("option"))
            {
                context.Response.ClearHeaders();
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
        }

        private void Application_EndRequest(object source, EventArgs e)
        {

        }

        public void Dispose() { }


    }
}