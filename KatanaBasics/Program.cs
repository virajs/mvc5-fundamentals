
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KatanaBasics
{
   
    using AppFunc = Func<IDictionary<string, object>, Task>;

    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";
            using (WebApp.Start<Startup>(uri)) 
            {
                Console.WriteLine("Your server is running at 8080");
                Console.ReadKey();
                Console.WriteLine("The server now is not running");
            }

        }

    }

    public class Startup { 
        public void Configuration(IAppBuilder app)
        {
            app.Use<HelloWorldComponents>();
        }
    }

    public class HelloWorldComponents
    { 
        AppFunc _next;
        public HelloWorldComponents(AppFunc next)
        {
            _next = next;
        }
        public Task Invoke(IDictionary<string, object> environment)
        
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response)) 
            {
                return writer.WriteAsync("Hello!!");
            };
        }
    }

}