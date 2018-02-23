using System;
using System.Net.Http;
using System.Windows;
using ClassLibrary1;


namespace WpfApp2
{
	public partial class App : Application
	{
		private void handleAppStartup(object sender, StartupEventArgs e)
		{
			Console.WriteLine("Hello World!");

			Envelope envelope = new Envelope();
			Console.WriteLine(
					envelope.PostAsync(
									new Uri("http://example.com"),
									new HttpClient())
							.Result);

			Console.WriteLine("SUCCESS!");
		}
	}
}
