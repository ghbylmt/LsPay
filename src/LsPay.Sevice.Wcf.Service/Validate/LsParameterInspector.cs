using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace LsPay.Service.Wcf.ServiceValidate
{
    public class LsParameterInspector : IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            throw new NotImplementedException();
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            throw new NotImplementedException();
        }
    }
}
