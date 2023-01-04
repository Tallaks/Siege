using Unity.Netcode;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Level.Board
{
  public class BoardSpawner : MonoBehaviour
  {
    [SerializeField] private NetworkObject _boardPrefab;

    public void SpawnBoard()
    {
      if (_boardPrefab.IsOwner)
      {
        Debug.Log("Spawning game board");
        NetworkObject spawnedObject = Instantiate(_boardPrefab);
        spawnedObject.Spawn();
      }
    }
  }
}