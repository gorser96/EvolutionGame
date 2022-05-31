namespace EvolutionBack.Services.Hubs;

public class ConnectionMapping<TKey> where TKey : notnull
{
    private readonly Dictionary<TKey, HashSet<string>> _connections = new();

    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    public void Add(TKey key, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(key, out var connections))
            {
                connections = new HashSet<string>();
                _connections.Add(key, connections);
            }

            lock (connections)
            {
                connections.Add(connectionId);
            }
        }
    }

    public IEnumerable<string> GetConnections(TKey key)
    {
        if (_connections.TryGetValue(key, out var connections))
        {
            return connections;
        }

        return Enumerable.Empty<string>();
    }

    public void Remove(TKey key, string connectionId)
    {
        lock (_connections)
        {
            if (!_connections.TryGetValue(key, out var connections))
            {
                return;
            }

            lock (connections)
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }
}
