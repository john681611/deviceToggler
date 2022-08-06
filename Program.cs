namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management; // need to add System.Management to your project references.

    class Program
    {
        static void Main(string[] args)
        {
            var usbDevices = GetUSBDevices();

            foreach (var usbDevice in usbDevices)
            {
                Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}, PNPClass: {3}, Enabled: {4}",
                    usbDevice.Key,
                    usbDevice.Value.GetPropertyValue("Caption"),
                    usbDevice.Value.GetPropertyValue("Status"),
                    usbDevice.Value.GetPropertyValue("PNPClass"),
                    (UInt32)usbDevice.Value.GetPropertyValue("ConfigManagerErrorCode") == 0);
            }

            Console.Write("ID to Disable: ");
            var id = Console.ReadLine();
            ManagementBaseObject outParams;
            if (!String.IsNullOrEmpty(id) && usbDevices.ContainsKey(id))
                outParams  = usbDevices[id].InvokeMethod("Disable", null, null);
            Console.WriteLine("Done");
        }

        static Dictionary<string, ManagementObject> GetUSBDevices()
        {
            var controllerClasses = new List<string> { "HIDClass", "LogitechWinUSBDevice", "Mouse", "Keyboard" };
            Dictionary<string, ManagementObject> devices = new Dictionary<string, ManagementObject>();


            var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity");
            foreach (ManagementObject device in searcher.Get())
            {
                var pnpClass = device.GetPropertyValue("PNPClass");
                var id = device.GetPropertyValue("DeviceID");
                var name = device.GetPropertyValue("Caption");
                if (pnpClass != null && id != null && name != null && !name.ToString().Trim().ToLower().StartsWith("hid-compliant") && controllerClasses.Contains(pnpClass.ToString()))
                {
                    var idString = id.ToString();
                    if ((devices.ContainsKey(idString) && devices[idString].GetPropertyValue("PNPClass").ToString() == "HIDClass") || !devices.ContainsKey(idString))
                    {
                        devices[idString] = device;
                    }
                }
            }
            return devices;
        }
    }
}