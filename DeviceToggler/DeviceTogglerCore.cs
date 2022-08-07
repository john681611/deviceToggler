namespace DeviceToggler.Lib
{
    using System.Collections.Generic;
    using System.Management;

    public class DeviceTogglerCore
    {
        public const string VERSION = "0.0.11";
        public static Dictionary<string, Device> GetUSBDevices()
        {
            var controllerClasses = new List<string> { "HIDClass", "LogitechWinUSBDevice", "Mouse", "Keyboard" };
            Dictionary<string, Device> devices = new Dictionary<string, Device>();
            var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity");
            foreach (ManagementObject device in searcher.Get())
            {
                var deviceObject = new Device(device);
                if (deviceObject.Class != null && deviceObject.ID != null && deviceObject.Name != null && !deviceObject.Name.ToLower().StartsWith("hid-compliant") && controllerClasses.Contains(deviceObject.Class))
                {
                    if ((devices.ContainsKey(deviceObject.ID) && devices[deviceObject.ID].Class == "HIDClass") || !devices.ContainsKey(deviceObject.ID))
                    {
                        devices[deviceObject.ID] = new Device(device);
                    }
                }
            }
            return devices;
        }
    }
}