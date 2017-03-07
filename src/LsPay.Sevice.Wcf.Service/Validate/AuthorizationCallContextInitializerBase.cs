using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace LsPay.Service.Wcf.ServiceValidate
{
    public abstract class AuthorizationCallContextInitializerBase : ICallContextInitializer
    {
        public void AfterInvoke(object correlationState)
        {
            IPrincipal principal = correlationState as IPrincipal;
            if (null != principal)
            {
                Thread.CurrentPrincipal = principal;
            }
        }

        public object BeforeInvoke(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.IClientChannel channel, System.ServiceModel.Channels.Message message)
        {
            var originalPrincipal = Thread.CurrentPrincipal;
            Thread.CurrentPrincipal = this.GetPrincipal(ServiceSecurityContext.Current);
            return originalPrincipal;
        }

        protected abstract IPrincipal GetPrincipal(ServiceSecurityContext serviceSecurityContext);
    }
}
