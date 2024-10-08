using ChitChat.Application.SignalR.IConnectionManager;
using System.Collections.Concurrent;

namespace ChitChat.Infrastructure.SignalR.Helpers
{
    public class UserConnectionManager : IUserConnectionManager
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _connections = new();
        private readonly ConcurrentDictionary<string, string> _userConnection = new();

        public void AddConnection(string userId, string connectionId)
        {
            _connections.AddOrUpdate(userId,
                new HashSet<string> { connectionId },
                (key, existingSet) =>
                {
                    lock (existingSet)
                    {
                        existingSet.Add(connectionId);
                    }
                    return existingSet;
                });

            _userConnection[connectionId] = userId; // Ghi đè trực tiếp thay vì dùng AddOrUpdate
        }

        public List<string> GetAllUsers()
        {
            return _connections.Keys.ToList();
        }

        public List<string> GetConnections(string userId)
        {
            if (_connections.TryGetValue(userId, out var connections))
            {
                lock (connections)
                {
                    return connections.ToList();
                }
            }
            return new List<string>();
        }

        public void RemoveConnection(string connectionId)
        {
            if (_userConnection.TryRemove(connectionId, out var userId))
            {
                if (_connections.TryGetValue(userId, out var connections))
                {
                    lock (connections) // Khóa trên HashSet connections
                    {
                        connections.Remove(connectionId);
                        if (connections.Count == 0)
                        {
                            _connections.TryRemove(userId, out _);
                        }
                    }
                }
            }
        }
    }
}
