using LsPay.Service.Wcf.Model;

namespace LsPay.Service.Interface
{
    /// <summary>
    /// 公共支付公共接口
    /// </summary>
    public interface IPayUtility
    {
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns>签到结果</returns>
        SignResponseModel Sign(VisualSelfServiceEquipment equipment);
    }
}
