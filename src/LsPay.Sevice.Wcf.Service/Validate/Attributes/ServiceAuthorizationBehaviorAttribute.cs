using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace LsPay.Service.Wcf.ServiceValidate.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAuthorizationBehaviorAttribute : Attribute,IServiceBehavior
    {

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //提供方法执行的上下文环境
            //OperationContext context = OperationContext.Current;
            //获取传进的消息属性
            //MessageProperties properties = context.IncomingMessageProperties;
            ///获取消息发送的远程终结点IP和端口
            //RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }
    }
}
