using Kulinaria.Tools.BattleTrier.Infrastructure.Services.Network.Authentication;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Installers
{
  public class LobbyInstaller : MonoInstaller, IInitializable
  {
    public static string JoinCode;
    
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _joinByCodeButton;

    [SerializeField] private GameObject _joinPanel;
    [SerializeField] private TMP_InputField _codeInputField;
    [SerializeField] private Button _joinButton;

    public async void Initialize()
    {
      _joinPanel.SetActive(false);
      _createButton.onClick.AddListener(() => Container.Resolve<LobbyService>().CreateGame());
      Container.Resolve<LobbyService>().OnGameCreated += code =>
      {
        JoinCode = code;
        SceneManager.LoadSceneAsync("Level1");
      };
      
      _joinByCodeButton.onClick.AddListener(() => _joinPanel.SetActive(true));
      _joinButton.onClick.AddListener(() => Container.Resolve<LobbyService>().JoinGame(_codeInputField.text));
    }

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<LobbyInstaller>().FromInstance(this).AsSingle();
      Container.Bind<LobbyService>().AsSingle();
    }
  }
}