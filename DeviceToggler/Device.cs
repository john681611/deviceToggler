namespace DeviceToggler.Lib
{
    using System;
    using System.Management;

    public class Device
    {
        private ManagementObject DeviceObject { init; get; }
        public string ID { get { return (string)DeviceObject.GetPropertyValue("DeviceID"); } }
        public string Name { get { return DeviceObject.GetPropertyValue("Caption").ToString().Trim(); } }
        public bool Enabled { get { return (UInt32)DeviceObject.GetPropertyValue("ConfigManagerErrorCode") == 0; } }
        public string Class { get { return (string)DeviceObject.GetPropertyValue("PNPClass"); } }

        public Device(ManagementObject deviceObject)
        {
            DeviceObject = deviceObject;
        }

        public void Toggle()
        {
            try
            {
                Console.WriteLine($"Toggling {Name}");
                if(Enabled)
                    DeviceObject.InvokeMethod("Disable", null, null);
                else
                    DeviceObject.InvokeMethod("Enable", null, null);
            }
            catch (System.Exception e)
            {

                throw new Exception("Toggle Failed are you running in Admin mode?", e);
            }
            
        }
    }
}