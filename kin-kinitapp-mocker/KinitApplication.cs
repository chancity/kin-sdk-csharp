using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using kin_kinit_mocker.Model;
using kin_kinit_mocker.Model.Earn;
using kin_kinit_mocker.Network;
using kin_kinit_mocker.Network.Model.Requests;
using kin_kinit_mocker.Network.Model.Responses;
using kin_kinit_mocker.Repository;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Refit;

namespace kin_kinit_mocker
{
    public class KinitApplication : IDataStoreProvider
    {
        private const string AppStatePath = "./app_states/";
        private static readonly string _basePath = "https://api.kinitapp.com/";

        private static readonly Random getrandom = new Random();
        private readonly OffersApi _offerApi = RestService.For<OffersApi>(_basePath);
        private readonly OnboardingApi _onboardingApi = RestService.For<OnboardingApi>(_basePath);

        private readonly PhoneAuthenticationApi _phoneAuthenticationApi =
            RestService.For<PhoneAuthenticationApi>(_basePath);

        private readonly TasksApi _taskApi = RestService.For<TasksApi>(_basePath);
        private readonly WalletApi _walletApi = RestService.For<WalletApi>(_basePath);
        private OffersRepository _offersRepository;
        private TasksRepository _tasksRepository;
        private UserRepository _userRepository;

        [JsonProperty]
        public string InstanceId { get; private set; }

        [JsonProperty]
        private ConcurrentDictionary<string, InMemoryDataStore> DataStores { get; set; }

        [JsonConstructor]
        private KinitApplication() { }

        public KinitApplication(Guid guid)
        {
            InstanceId = guid.ToString("N");
            DataStores = new ConcurrentDictionary<string, InMemoryDataStore>();
            ConfigureServices();
        }

        public IDataStore DataStore(string storage)
        {
            string storeKey = $"{InstanceId}-{storage}";
            return DataStores.GetOrAdd(storeKey, s => new InMemoryDataStore());
        }

        [OnDeserialized]
        private void AfterDeserialized(StreamingContext streamingContext)
        {
            ConfigureServices();
        }

        private void ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<IDataStoreProvider>(this)
                .AddSingleton<UserRepository>()
                .AddSingleton<TasksRepository>()
                .AddSingleton<OffersRepository>();

            ServiceProvider provider = services.BuildServiceProvider();
            _userRepository = provider.GetRequiredService<UserRepository>();
            _tasksRepository = provider.GetRequiredService<TasksRepository>();
            _offersRepository = provider.GetRequiredService<OffersRepository>();

            _userRepository.IsFreshInstall = false;
            Directory.CreateDirectory(AppStatePath);
        }


        public void Start()
        {
            Task.Factory.StartNew(MainLoop, TaskCreationOptions.LongRunning);
        }
        public async Task CreateAndActivate()
        {

            if (!_userRepository.IsRegistered)
            {
                await Register().ConfigureAwait(false);
                await OnBoard().ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("User is already registered launching bot app");
                await AppLaunch().ConfigureAwait(false);
                await UpdateOffers().ConfigureAwait(false);
                return;
            }

            while (true)
            {
                await Task.Delay(1000).ConfigureAwait(false); ;

                try
                {
                    if (!_userRepository.IsWalletActivated)
                    {
                        _userRepository.IsWalletActivated = await _userRepository.UserInfo.KayPair.Activate().ConfigureAwait(false);

                        if (!_userRepository.IsWalletActivated)
                        {
                            continue;
                        }

                        Console.WriteLine("Wallet trust KIN asset....");
                        return;
                    }
                }
                catch (ApiException apiExceptionx)
                {
                    Console.WriteLine(apiExceptionx.Content.Trim());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }

        private bool _launched = false;
        private async void MainLoop()
        {
            while (true)
            {
                await Task.Delay(1000).ConfigureAwait(true); ;

                try
                {
                    if (!_launched)
                    {
                        await CreateAndActivate().ConfigureAwait(false);
                        _launched = true;
                    }

                    await UpdateTask().ConfigureAwait(false);
                    AnswerTaskQuestions();
                    await SubmitTaskResults().ConfigureAwait(false);
                    _tasksRepository.ReplaceTask();
                }
                catch (ApiException apiExceptionx)
                {
                    Console.WriteLine(apiExceptionx.Content.Trim());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
                finally
                {
                    await SaveState().ConfigureAwait(false);
                }

            }
        }

        private async Task AppLaunch()
        {
            StatusConfigResponse appLauchResponse = await _onboardingApi.AppLaunch(_userRepository.UserInfo.Id, new AppVersionRequest("1.0.9")).ConfigureAwait(false);
            Console.WriteLine($"appLauchResponse:\n {JsonConvert.SerializeObject(appLauchResponse, Formatting.Indented)}");
            Console.WriteLine();
            ApplyConfigurationFromServer(appLauchResponse.Config);
        }

        private async Task<StatusConfigResponse> Register()
        {
            var registrationInfoRequest = new RegistrationInfoRequest(_userRepository.UserInfo.Id);
            Console.WriteLine($"registrationInfoRequest:\n{JsonConvert.SerializeObject(registrationInfoRequest, Formatting.Indented)}\n");
            StatusConfigResponse registrationInfoResponse = await _onboardingApi.Register(registrationInfoRequest).ConfigureAwait(false);
            Console.WriteLine($"registrationInfoResponse:\n {JsonConvert.SerializeObject(registrationInfoResponse, Formatting.Indented)}");
            Console.WriteLine();
            ApplyConfigurationFromServer(registrationInfoResponse.Config);
            _userRepository.IsRegistered = true;
            return registrationInfoResponse;
        }

        private async Task<StatusResponse> OnBoard()
        {
            var onBoardRequest = new AccountInfoRequest(_userRepository.UserInfo.PublicAddress);
            Console.WriteLine($"onBoardRequest:\n{JsonConvert.SerializeObject(onBoardRequest, Formatting.Indented)}\n");
            StatusResponse onBoardResponse = await _onboardingApi.OnBoard(_userRepository.UserInfo.Id, onBoardRequest).ConfigureAwait(false);
            Console.WriteLine($"onBoardResponse:\n {JsonConvert.SerializeObject(onBoardResponse, Formatting.Indented)}");
            Console.WriteLine();
            return onBoardResponse;
        }

        private async Task UpdateOffers()
        {
            OffersResponse offersResponse = await _offerApi.Offers(_userRepository.UserInfo.Id).ConfigureAwait(false);
            Console.WriteLine($"offersResponse:\n{JsonConvert.SerializeObject(offersResponse, Formatting.Indented)}");
            Console.WriteLine();

            _offersRepository.UpdateOffers(offersResponse.OfferList);
        }

        private async Task<TaskSubmitResponse> SubmitTaskResults()
        {
            if (_tasksRepository.EarnTask == null || !_tasksRepository.IsTaskComplete)
            {
                return null;
            }

            _tasksRepository.EarnTask.IsValid();

            string taskId = _tasksRepository.EarnTask.Id;
            string publicAddress = _userRepository.UserInfo.PublicAddress;
            List<ChosenAnswer> chosenAnswers = _tasksRepository.GetChosenAnswers();
            var submitInfoRequest = new SubmitInfoRequest(taskId, publicAddress, chosenAnswers);
            Console.WriteLine($"submitInfoRequest:\n{JsonConvert.SerializeObject(submitInfoRequest, Formatting.Indented)}\n");
            var submitInfoResponse = await _taskApi.SubmitTaskResults(_userRepository.UserInfo.Id, submitInfoRequest).ConfigureAwait(false);
            Console.WriteLine($"submitInfoResponse:\n{JsonConvert.SerializeObject(submitInfoRequest, Formatting.Indented)}");
            Console.WriteLine();

            return submitInfoResponse;
        }

        private void AnswerTaskQuestions()
        {
            if (_tasksRepository.EarnTask == null || _tasksRepository.IsTaskComplete ||
                !_tasksRepository.IsTaskAvaliable)
            {
                return;
            }

            foreach (Question question in _tasksRepository.EarnTask.Questions)
            {
                List<string> listOfAnswerIds;

                if (question.IsTypeMultiple())
                {
                    listOfAnswerIds = question.Answers.Where(a => a.IsValid())
                        .PickRandom(GetRandomNumber(1, question.Answers.Count))
                        .Select(x => x.Id).ToList();
                }
                else
                {
                    listOfAnswerIds = new List<string>();
                    listOfAnswerIds.Add(question.Answers.Where(a => a.IsValid()).PickRandom().Id);
                }

                _tasksRepository.SetChosenAnswers(question.Id, listOfAnswerIds);
            }
        }

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        private async Task UpdateTask()
        {
            if (_tasksRepository.EarnTask != null && _tasksRepository.IsTaskComplete)
            {
                return;
            }

            NextTasksResponse nextTasks = await _taskApi.NextTasks(_userRepository.UserInfo.Id);

            if (nextTasks.Tasks.Count > 0 && nextTasks.Tasks[0].IsValid())
            {
                if (_tasksRepository.EarnTask == null || _tasksRepository.IsTaskComplete)
                {
                    _tasksRepository.ReplaceTask(nextTasks.Tasks[0]);
                    Console.WriteLine($"updateTask:\n{JsonConvert.SerializeObject(nextTasks.Tasks[0], Formatting.Indented)}");
                    Console.WriteLine();
                }
            }


        }

        private void ApplyConfigurationFromServer(ConfigResponse configResponse)
        {
            _userRepository.Tos = configResponse.Tos;
            _userRepository.FcmTokenSent = configResponse.AuthTokenEnabled;
            _userRepository.IsPhoneVerificationEnabled = configResponse.PhoneVerificationEnabled;
            _userRepository.IsP2PEnabled = configResponse.P2PEnabled;
            _userRepository.P2PMaxKin = configResponse.P2PMaxKin;
            _userRepository.P2PMinKin = configResponse.P2PMinKin;
            _userRepository.P2PMinTasks = configResponse.P2PMinTasks;
        }

        public async Task SaveState()
        {
            string toWrite = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter($"{AppStatePath}/{InstanceId}.temp", false))
            {
                await sw.WriteLineAsync(toWrite).ConfigureAwait(false);
            }

            File.Copy($"{AppStatePath}/{InstanceId}.temp", $"{AppStatePath}/{InstanceId}.state", true);
        }

        public static async Task<List<KinitApplication>> GetSavedApps()
        {
            Directory.CreateDirectory(AppStatePath);

            List<KinitApplication> ret = new List<KinitApplication>();
            foreach (string file in Directory.GetFiles(AppStatePath, "*.state", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(file))
                {
                    string fileJson = "";

                    using (StreamReader sr = new StreamReader(file))
                    {
                        fileJson = await sr.ReadToEndAsync();
                    }

                    if (fileJson.IsNullOrBlank())
                    {
                        File.Delete(file);
                        continue;
                    }

                    try
                    {
                        KinitApplication newObj = JsonConvert.DeserializeObject<KinitApplication>(fileJson);
                        ret.Add(newObj);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to deserialize {file}: {e.Message}");
                    }
                }
            }

            return ret;
        }
    }
}