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
        private static readonly AutoResetEvent _queueNotifier1 = new AutoResetEvent(false);
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer(25);
        private static int _queueCounter = 0;
        private static int _createdCount = 0;
        private static int _failedCount = 0;
        private static int _maxQueue = 25;
        private static int sleepBetweenQueries = 100;
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private const string AppStatePath = "./app_states/";
        static void Main(string[] args)
        {
            ServicePointManager.DefaultConnectionLimit = 200;

            //  var savedApps = KinitApplication.GetSavedApps().Result;
            // 
            //  if (savedApps.Count > 0)
            //  {
            //      foreach (KinitApplication kinitApplication in savedApps)
            //      {
            //          kinitApplication.Start();
            //      }
            //  }
            //  else
            //  {
            //      var kinitApplication = new KinitApplication(Guid.NewGuid());
            //      kinitApplication.Start();
            //  }

            _timer.Elapsed += Timer_tick;
            _timer.Enabled = true;
            _timer.Start();

            Start();
            Console.ReadLine();
            Shutdown();
            
        }
        private static void Timer_tick(object sender, ElapsedEventArgs e)
        {
            if (_queueCounter < _maxQueue)
                _queueNotifier1.Set();
        }

        public static void Shutdown()
        {
            _cancellationTokenSource.Cancel(true);
        }

        public static void Start()
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(ParserWorkLoop, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
        }
        private static void ParserWorkLoop(object cancellationToken)
        {
            var ct = (CancellationToken)cancellationToken;

            while (!ct.IsCancellationRequested)
            {
                if (_queueCounter >= _maxQueue) _queueNotifier1.WaitOne();
                Console.Title = $"Created Count: {_createdCount}, Failed Count: {_failedCount}";
                DoIt();
                Thread.Sleep(sleepBetweenQueries);
            }
        }

        private static async void DoIt()
        {
            Interlocked.Increment(ref _queueCounter);

            try
            {
                var kinitApplication = new KinitApplication(Guid.NewGuid());
                await kinitApplication.CreateAndActivate().ConfigureAwait(false);
                Interlocked.Increment(ref _createdCount);
            }
            catch (Exception e)
            {
                Interlocked.Increment(ref _failedCount);
                Console.WriteLine("DoIt: " + e.Message);
            }
            finally
            {
                Interlocked.Decrement(ref _queueCounter);
            }
        }
    }
}
