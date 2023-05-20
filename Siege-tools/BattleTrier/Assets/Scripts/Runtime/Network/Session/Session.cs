using System.Collections.Generic;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Network.Session
{
  public class Session<T> where T : struct, ISessionPlayerData
  {
    private readonly Dictionary<string, T> _clientData = new();
    private readonly Dictionary<ulong, string> _clientIdToPlayerId = new();
    private bool _hasSessionStarted;

    public void DisconnectClient(ulong clientId)
    {
      if (_hasSessionStarted)
      {
        if (_clientIdToPlayerId.TryGetValue(clientId, out string playerId))
          if (GetPlayerData(playerId)?.ClientID == clientId)
          {
            T clientData = _clientData[playerId];
            clientData.IsConnected = false;
            _clientData[playerId] = clientData;
          }
      }
      else
      {
        if (_clientIdToPlayerId.TryGetValue(clientId, out string playerId))
        {
          _clientIdToPlayerId.Remove(clientId);
          if (GetPlayerData(playerId)?.ClientID == clientId)
            _clientData.Remove(playerId);
        }
      }
    }

    public T? GetPlayerData(ulong clientId)
    {
      string playerId = GetPlayerId(clientId);
      if (playerId != null)
        return GetPlayerData(playerId);

      Debug.Log($"No client player ID found mapped to the given client ID: {clientId}");
      return null;
    }

    public T? GetPlayerData(string playerId)
    {
      if (_clientData.TryGetValue(playerId, out T data))
        return data;

      Debug.Log($"No PlayerData of matching player ID found: {playerId}");
      return null;
    }

    public string GetPlayerId(ulong clientId)
    {
      if (_clientIdToPlayerId.TryGetValue(clientId, out string playerId))
        return playerId;

      Debug.Log($"No client player ID found mapped to the given client ID: {clientId}");
      return null;
    }

    public bool IsDuplicateConnection(string playerId) =>
      _clientData.ContainsKey(playerId) && _clientData[playerId].IsConnected;

    public void OnServerEnded()
    {
      _clientData.Clear();
      _clientIdToPlayerId.Clear();
      _hasSessionStarted = false;
    }

    public void OnSessionEnded()
    {
      ClearDisconnectedPlayersData();
      ReinitializePlayersData();
      _hasSessionStarted = false;
    }

    public void OnSessionStarted() =>
      _hasSessionStarted = true;

    public void SetPlayerData(ulong clientId, T sessionPlayerData)
    {
      if (_clientIdToPlayerId.TryGetValue(clientId, out string playerId))
        _clientData[playerId] = sessionPlayerData;
      else
        Debug.LogError($"No client player ID found mapped to the given client ID: {clientId}");
    }

    public void SetupConnectingPlayerSessionData(ulong clientId, string playerId, T sessionPlayerData)
    {
      Debug.Log("Setup connecting player session data");
      var isReconnecting = false;
      if (IsDuplicateConnection(playerId))
      {
        Debug.LogError(
          $"Player ID {playerId} already exists. This is a duplicate connection. Rejecting this session data.");
        return;
      }

      if (_clientData.ContainsKey(playerId))
        if (!_clientData[playerId].IsConnected)
          isReconnecting = true;

      if (isReconnecting)
      {
        sessionPlayerData = _clientData[playerId];
        sessionPlayerData.ClientID = clientId;
        sessionPlayerData.IsConnected = true;
      }

      _clientIdToPlayerId[clientId] = playerId;
      _clientData[playerId] = sessionPlayerData;
    }

    private void ReinitializePlayersData()
    {
      foreach (ulong id in _clientIdToPlayerId.Keys)
      {
        string playerId = _clientIdToPlayerId[id];
        T sessionPlayerData = _clientData[playerId];
        sessionPlayerData.Reinitialize();
        _clientData[playerId] = sessionPlayerData;
      }
    }

    private void ClearDisconnectedPlayersData()
    {
      var idsToClear = new List<ulong>();
      foreach (ulong id in _clientIdToPlayerId.Keys)
      {
        T? data = GetPlayerData(id);
        if (data is { IsConnected: false })
          idsToClear.Add(id);
      }

      foreach (ulong id in idsToClear)
      {
        string playerId = _clientIdToPlayerId[id];
        if (GetPlayerData(playerId)?.ClientID == id)
          _clientData.Remove(playerId);

        _clientIdToPlayerId.Remove(id);
      }
    }
  }
}