using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using kin_kinit_mocker;
using Newtonsoft.Json;
using stellar_dotnet_sdk;

namespace Kinit_TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            Start();
            Console.ReadLine();
            
        }

        public static void Start()
        {
              var savedApps = KinitApplication.GetSavedApps().Result;
         
            if (savedApps.Count > 0)
              {
                  foreach (KinitApplication kinitApplication in savedApps)
                  {
                      
                      kinitApplication.Start();
                  }
              }
              else
              {
                  var kinitApplication = new KinitApplication(Guid.NewGuid());
                  kinitApplication.Start();
              }
        }
    }
}
