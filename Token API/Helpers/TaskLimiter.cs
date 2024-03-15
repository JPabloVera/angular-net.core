namespace Token_API;

public class TaskLimiter{
    
    private readonly TimeSpan _time_span;
    private readonly SemaphoreSlim _semaphore;
    private readonly HttpClient _client;

    public TaskLimiter(int count, TimeSpan time_span, HttpClient client = null)
    {
        _semaphore = new SemaphoreSlim(count, count);
        _time_span = time_span;
        _client = client;
    }
    
    public async Task<T> DoAsync<T>(Func<Task<T>> task_factory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);
        var task = task_factory();
        task.ContinueWith(async e =>
        {
            await Task.Delay(_time_span);
            _semaphore.Release(1);
        });
        return await task;
    }

    public async Task<HttpResponseMessage> FetchAsync(string url) {
        if (_client == null)
            return null;

        var response = await DoAsync<HttpResponseMessage>(async () => 
            await _client.GetAsync(url));

        return response;
    }
}