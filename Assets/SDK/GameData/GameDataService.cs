
namespace VC.GameData
{
    using VC.GameData.App;
    using VC.GameData.User;
    using VC.GameData.Device;

    public class GameDataService
    {
        public AppData AppData { get; set; }
        public UserData UserData { get; set; }
        public IDeviceData Device { get; set; }
    }
}