using System.Drawing;
using System.Windows.Forms;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceToggler.GUI.Desktop
{
    public partial class DeviceTogglerBlazorWrapper : Form
    {
        public DeviceTogglerBlazorWrapper()
        {
            InitializeComponent();

            Text = $"DeviceToggler {DeviceToggler.Lib.DeviceTogglerCore.VERSION}";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorWebView();
            serviceCollection.AddBlazoredLocalStorage();
            var blazor = new BlazorWebView()
            {
                Dock = DockStyle.Fill,
                HostPage = "wwwroot/index.html",
                Services = serviceCollection.BuildServiceProvider()
            };
            blazor.RootComponents.Add<App>("#app");
            Controls.Add(blazor);
        }

    }
}
