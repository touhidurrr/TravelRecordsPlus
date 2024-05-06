using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;

namespace TravelRecords.Services;

public record LoginInputs(string username, string password);

public record RegisterInputs(
    string name,
    string email,
    string username,
    string password
);

public record User(int id, string name, string email, string username);

public record LoginResponse(User user, string token);

public class Rest
{
    private RestClient _client;
    private User _user;
    private TaskCompletionSource<User> _waitForUserTaskSource = new TaskCompletionSource<User>();

    public async Task DoAfterUserIsAvailable(Action action)
    {
            await this._waitForUserTaskSource.Task;
            action();
    }

    public Rest()
    {
        _client = new RestClient("http://localhost:3000");
    }

    public void SetToken(string token)
    {
        _client.AddDefaultHeader("Authorization", $"Bearer {token}");
    }

    public void SetUser(User user)
    {
        _user = user;
        _waitForUserTaskSource.SetResult(user);
    }

    public User GetUser() => _user;

    public async Task<LoginResponse?> Login(LoginInputs loginInputs)
    {
        var request = new RestRequest("/login", Method.Post)
            .AddJsonBody(loginInputs);
        return await _client.PostAsync<LoginResponse>(request);
    }

    public async Task<LoginResponse?> Register(RegisterInputs regInputs)
    {
        var request = new RestRequest("/register", Method.Post)
            .AddJsonBody(regInputs);
        return await _client.PostAsync<LoginResponse>(request);
    }
}