using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VC.Device
{
    public class AndroidDeviceService : IDeviceService
    {
        public string DeviceId
        {
            get
            {
                return null;
            }
        }

        public string DeviceUuid => throw new System.NotImplementedException();

        public string GoogleAdId => throw new System.NotImplementedException();

        public int DeviceType => throw new System.NotImplementedException();

        public string CountryCode => throw new System.NotImplementedException();

        public int ElapsedRealtime => throw new System.NotImplementedException();

        public bool IsTestDevice => throw new System.NotImplementedException();

        public string DeviceModel => throw new System.NotImplementedException();

        public int DeviceModelClass => throw new System.NotImplementedException();

        public string OSVersion => throw new System.NotImplementedException();

        public int TimeToFirstInstall => throw new System.NotImplementedException();

        public int TimeToLastUpdate => throw new System.NotImplementedException();

        public void SetDeviceTest()
        {
            throw new System.NotImplementedException();
        }
    }
}
