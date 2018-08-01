﻿using System;
using System.Collections.Concurrent;
using kin_kinit_mocker.Modules;
using kin_kinit_mocker.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace kin_kinit_mocker
{
    public class KinitApplication : IDataStoreProvider
    {
        public string InstanceId { get; private set; }
        private static ConcurrentDictionary<string, IDataStore> _dataStores;
        private readonly UserRepository _userRepository;
        private readonly TasksRepository _tasksRepository;

        public KinitApplication()
        {
            InstanceId = Guid.NewGuid().ToString("N");
            _dataStores = new ConcurrentDictionary<string, IDataStore>();

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider provider = services.BuildServiceProvider();
            _userRepository = provider.GetRequiredService<UserRepository>();
            _tasksRepository = provider.GetRequiredService<TasksRepository>();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IDataStoreProvider>((s) => new DataStoreProviderModule(this))
                .AddSingleton<UserRepository>()
                .AddSingleton<TasksRepository>()
                .AddSingleton<OffersRepository>();
        }

        public IDataStore DataStore(string storage)
        {
            string storeKey = $"{InstanceId}-{storage}";
            return _dataStores.GetOrAdd(storeKey, s => new InMemoryDataStore(storeKey));
        }
    }
}
