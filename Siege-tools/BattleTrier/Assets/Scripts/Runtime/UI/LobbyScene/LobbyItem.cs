using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI.LobbyScene
{
  public class LobbyItem : MonoBehaviour
  {
    [SerializeField] private TMP_Text _name;
    public void Initialize(Lobby lobby) => 
      _name.text = lobby.Name;
  }
}