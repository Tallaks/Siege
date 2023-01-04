using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Network.Authentication
{
  public class LobbyService
  {
    public Action<string> OnGameCreated { get; set; }

    private Guid _hostAllocationId;
    private RelayServerData _serverData;
    
    public async void CreateGame()
    {
      Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
      _hostAllocationId = allocation.AllocationId;

      string joinCode = await RelayService.Instance.GetJoinCodeAsync(_hostAllocationId); 
      OnGameCreated?.Invoke(joinCode);
      _serverData = new RelayServerData(allocation, "dtls");
      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(_serverData);
      NetworkManager.Singleton.StartHost();
    }

    public async void JoinGame(string joinCode)
    {
      JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
      Debug.Log(joinAllocation);
      _serverData = new RelayServerData(joinAllocation, "dtls");
      NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(_serverData);
      NetworkManager.Singleton.StartClient();
    }
  }
}