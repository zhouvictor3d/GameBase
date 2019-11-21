
namespace VC.GameData.Device
{
    using UnityEngine;
    public interface IDeviceData
    {
        /// <summary>
        /// 国家代码
        /// </summary>
        /// <returns></returns>
        string CountryCode { get; }

        /// <summary>
        /// 返回系统启动到现在的毫秒数，包含休眠时间
        /// </summary>
        /// <returns></returns>
        int ElapsedRealtime { get; }

        /// <summary>
        /// 设备Id
        /// </summary>
        /// <returns></returns>
        string DeviceId { get; }

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        /// <returns></returns>
        string DeviceUuid { get; }

        /// <summary>
        /// 设备模型
        /// </summary>
        string DeviceModel { get; }

        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// 设备类型
        /// </summary>
        DeviceType DeviceType { get; }

        /// <summary>
        /// 操作类型
        /// </summary>
        string OperationSystem { get; }

        /// <summary>
        /// 内存大小
        /// </summary>
        int MemorySize { get; }

        /// <summary>
        /// 获取网络类型
        /// </summary>
        DeviceNetType NetType { get; }



        /// <summary>
        /// 是否为测试设备
        /// </summary>
        /// <returns></returns>
        bool IsTestDevice { get; }

        void SetDeviceTest();
    }
}
