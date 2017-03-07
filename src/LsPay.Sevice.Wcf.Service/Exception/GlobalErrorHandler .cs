using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace LsPay.Service.Wcf.ServiceException
{
    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(System.Exception error)
        {
            return true;
        }

        public void ProvideFault(System.Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            throw new NotImplementedException();
        }
    }
}
