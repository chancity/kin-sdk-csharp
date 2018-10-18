using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk;

namespace WalletActivator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your KIN wallet seed: ");
            var walletSeed = Console.ReadLine();

            try
            {
                var keyPair = KeyPair.FromSecretSeed(walletSeed);
                var result = keyPair.Activate().Result;


                Console.WriteLine($"Account activated: {result}");

                Process.Start(
                    $"https://kinexplorer.com/account/{keyPair.Address}#operations");

               
            }
            catch (Exception e)
            {
                Console.WriteLine($"Account failed to activate: {e}");
            }

            Console.WriteLine("Press any key to exit....");

            Console.ReadLine();

        }
    }
}
