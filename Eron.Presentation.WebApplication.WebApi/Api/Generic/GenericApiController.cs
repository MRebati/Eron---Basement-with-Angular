using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Ninject;

namespace Eron.Presentation.WebApplication.WebApi.Api.Generic
{
    public class GenericApiController : BaseApiController
    {
        private IKernel _kernel;
        public GenericApiController(IKernel kernel)
        {
            this._kernel = kernel;
        }

        [Route("app/{serviceName}/{methodName}")]
        public async Task<IHttpActionResult> Get(string serviceName, string methodName, object[] obj)
        {
            serviceName = serviceName.ToLower();

            Assembly assembly = Assembly.Load("Eron.Business.Core");
            var types = assembly.GetTypes();
            var type = types.FirstOrDefault(x => x.IsInterface && x.Name.ToLower() == "i" + serviceName + "appservice");

            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod(methodName);

                if (methodInfo != null)
                {
                    object result = null;
                    var parameters = methodInfo.GetParameters();
                    var inputParameters = new object();
                    foreach (var param in parameters)
                    {
                        inputParameters = param.DefaultValue;
                    }
                    var classInstance = _kernel.Get(type);

                    if (IsAsyncMethod(methodInfo))
                    {
                        result = await (dynamic)methodInfo.Invoke(classInstance, obj);
                    }
                    else
                    {
                        result = methodInfo.Invoke(classInstance, obj);
                    }

                    return Ok(result);
                }
            }
            return Ok();
        }

        [Route("app/{serviceName}/{methodName}")]
        public IHttpActionResult Post(string serviceName, string methodName, object obj)
        {
            Assembly assembly = Assembly.LoadFile("...Assembly1.dll");
            var types = assembly.GetTypes();
            var type = types.FirstOrDefault(x => x.IsInterface && x.Name == "I" + serviceName + "AppService");

            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod(methodName);

                if (methodInfo != null)
                {
                    object result = null;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(type);

                    if (parameters.Length == 0)
                    {
                        // This works fine
                        try
                        {
                            result = methodInfo.Invoke(classInstance, new[] { obj });
                            return Ok(result);
                        }
                        catch
                        {
                            throw;
                        }

                    }
                    else
                    {
                        object[] parametersArray = new object[] { "Hello" };

                        // The invoke does NOT work;
                        // it throws "Object does not match target type"             
                        result = methodInfo.Invoke(methodInfo, parametersArray);
                    }
                }
            }
            return Ok();
        }

        private bool IsAsyncMethod(MethodInfo methodInfo)
        {
            Type attType = typeof(AsyncStateMachineAttribute);
            var attrib = (AsyncStateMachineAttribute)methodInfo.GetCustomAttribute(attType);

            return (attrib != null);
        }
    }
}