using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using ViewModels.GlobalVariable;

namespace ViewModels.SignalR
{
    public class ServerConnection
    {
        public class RandomRetryPolicy : IRetryPolicy
        {
            private readonly Random _random = new();

            public TimeSpan? NextRetryDelay(RetryContext retryContext)
            {
                if (retryContext.ElapsedTime < TimeSpan.FromSeconds(60))
                {
                    return TimeSpan.FromSeconds(_random.NextDouble() * 10);
                }
                else
                {
                    return TimeSpan.FromSeconds(_random.NextDouble() * 60);
                }
            }
        }

        private HubConnection _connection = null!;

        public async Task ConnectToHubAsync()
        {
            try
            {
                _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5154/meetingHub/",option =>
                {
                    option.AccessTokenProvider = async () => await Task.FromResult(AccessToken.Instance.Token);
                })
                .WithAutomaticReconnect(new RandomRetryPolicy())
                .Build();

                _connection.Reconnecting += error =>
                {
                    return Task.CompletedTask;
                };


                _connection.On<int>("ReceiveError", meet =>
                {
                    Debug.Print(meet.ToString());
                });


                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                //  throw new Exception("Connect to project-service's hub false.", ex);
            }
        }

        public void CheckConnection(Action sentToHubAction)
        {
            int count = 0;
            int maxAttempts = 1;

            async void RecursiveCheck()
            {
                if (_connection != null && _connection.State == HubConnectionState.Connected)
                {
                    sentToHubAction();
                }
                else
                {
                    await ConnectToHubAsync();
                    if (count <= maxAttempts)
                    {
                        count++;
                        RecursiveCheck();
                    }
                }
            }
            RecursiveCheck();
        }

        public void InvokeAsync(Action<HubConnection> action)
        {
            CheckConnection(() => { action(_connection); });
        }
    }
}

