using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace blazingchat.Client;
public class GlobalChatBase:ComponentBase, IAsyncDisposable
{
    protected HubConnection? hubConnection;
    protected List<string> messages = new List<string>();
    protected string? userInput;
    protected string? messageInput;
    protected DateTime? DateInput;
    [Inject] private NavigationManager Navigation {get;set;}

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(Navigation.ToAbsoluteUri("/chathub"))
            .Build();

        hubConnection.On<string, string, DateTime,string>("ReceiveMessage", (user, message, Date,connectionId) =>
        {
           if (user!=userInput&&message!=messageInput)
            {
                var encodedMsg = $"Receive: {Date.ToString()}: {user}: {message}";
                 messages.Add(encodedMsg);
                  StateHasChanged();
            }
            else if(user==userInput&&message==messageInput)
            {
                 var encodedMsg = $"Send: {Date.ToString()}: {user}: {message}";
                  messages.Add(encodedMsg);
                  StateHasChanged();
            }
           
        });

        await hubConnection.StartAsync();
        
    }

    protected async Task Send()
    {
        if (hubConnection is not null)
            {
                DateInput=DateTime.Now;
            
                await hubConnection.SendAsync("SendMessage", userInput, messageInput,DateInput,null);
            }
    }

    public bool IsConnected =>
        hubConnection?.State == HubConnectionState.Connected;

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }
}