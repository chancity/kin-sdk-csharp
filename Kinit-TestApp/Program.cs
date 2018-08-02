using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using kin_kinit_mocker;
using Newtonsoft.Json;
using stellar_dotnet_sdk;

namespace Kinit_TestApp
{
    class Program
    {
        private const string AppStatePath = "./app_states/";
        static void Main(string[] args)
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

           // for (int i = 0; i < 2; i++)
           // {
           //     var kinitApplication = new KinitApplication(Guid.NewGuid());
           //     kinitApplication.Start();
           // }

            Console.ReadLine();
            
        }
    }
}
