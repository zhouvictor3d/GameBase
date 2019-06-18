using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VC.Device
{
    public interface IDeviceService
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        /// <returns></returns>
        string DeviceId { get; }

        /// <summary>
        /// 设备Uuid
        /// </summary>
        /// <returns></returns>
        string DeviceUuid { get; }

        /// <summary>
        /// Google广告Id
        /// </summary>
        /// <returns></returns>
        string GoogleAdId { get; }

        /// <summary>
        /// 设备类型
        /// </summary>
        /// <returns></returns>
        int DeviceType { get; }

        /// <summary>
        /// 国家代码
        /// </summary>
        /// <returns></returns>
        string CountryCode { get; }

        /// <summary>
        /// GetElapsedRealtime
        /// </summary>
        /// <returns></returns>
        int ElapsedRealtime { get; }

        /// <summary>
        /// 是否为测试设备
        /// </summary>
        /// <returns></returns>
        bool IsTestDevice { get; }

        string DeviceModel { get; }

        int DeviceModelClass { get; }

        /// <summary>
        /// 系统版本
        /// </summary>
        string OSVersion { get; }

        /// <summary>
        /// 第一次安装时间
        /// </summary>
        int TimeToFirstInstall { get; }

        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        int TimeToLastUpdate { get; }

        void SetDeviceTest();
    }
}
