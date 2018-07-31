using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace FilmsManagerApi
{
	public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Debug.Write("@@@@@             " + ex.Message);
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				  .UseKestrel()
				  .UseUrls("http://*:62991")
				  .UseContentRoot(Directory.GetCurrentDirectory())
				  .UseStartup<Startup>()
				  .Build();
	}
}
