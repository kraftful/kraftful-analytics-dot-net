using System;
using Segment.Model;
using Xamarin.Essentials;

namespace Kraftful.Analytics.Core
{
    public struct AppDeviceInfoData
    {
        public string AppVersion;
        public string DeviceId;
        public string DeviceModel;
        public string DeviceType;
        public string OsName;
    }

    public interface IAppDeviceInfo
    {
        public AppDeviceInfoData GetAppDeviceInfo();
    }

    public class XamarinAppDeviceInfo: IAppDeviceInfo
    {
        public AppDeviceInfoData GetAppDeviceInfo()
        {
            var info = new AppDeviceInfoData();
            info.AppVersion = AppInfo.VersionString;
            info.DeviceId = DeviceInfo.Model;
            info.DeviceModel = DeviceInfo.Name;
            info.DeviceType = DeviceInfo.Platform.ToString();
            info.OsName = DeviceInfo.Platform.ToString();
            return info;
        }
    }
}

