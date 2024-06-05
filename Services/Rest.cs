using System;
using System.Threading.Tasks;
using RestSharp;
using TravelRecords.Models;

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

public record AddAccountInputs(
    string name,
    float balance,
    int userId
);

public record AddTransactionInputs(
    string @ref,
    float amount,
    int accountId
);

public record UpdateUserInputs(
    string? name,
    string? email,
    string? password,
    string? newPassword
);

public record SuccessResponse(bool success);

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
        _client = new RestClient("https://travelrecords.touhidur.tech");
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

    public async Task<Account[]?> GetAccounts()
    {
        var request = new RestRequest("/accounts")
            .AddParameter("userId", _user.id);
        return await _client.GetAsync<Account[]>(request);
    }

    public async Task<Account?> AddAccount(AddAccountInputs addAccountInputs)
    {
        var request = new RestRequest("/accounts", Method.Post)
            .AddJsonBody(addAccountInputs);
        return await _client.PostAsync<Account>(request);
    }

    public async Task<SuccessResponse?> RemoveAccount(int accountId)
    {
        var request = new RestRequest("/accounts/{accountId}", Method.Delete)
            .AddUrlSegment("accountId", accountId);
        return await _client.DeleteAsync<SuccessResponse>(request);
    }

    public async Task<Transaction[]?> GetTransactions(int accountId)
    {
        var request = new RestRequest("/accounts/{accountId}/transactions")
            .AddUrlSegment("accountId", accountId);
        return await _client.GetAsync<Transaction[]>(request);
    }

    public async Task<Transaction[]?> AddTransaction(AddTransactionInputs inputs)
    {
        var request = new RestRequest("/accounts/transactions", Method.Post)
            .AddJsonBody(inputs);
        return await _client.PostAsync<Transaction[]>(request);
    }

    public async Task<SuccessResponse?> UpdateUser(int userId, UpdateUserInputs inputs)
    {
        var request = new RestRequest("/users/{userId}", Method.Patch)
            .AddUrlSegment("userId", userId)
            .AddJsonBody(inputs);
        return await _client.PatchAsync<SuccessResponse>(request);
    }
}