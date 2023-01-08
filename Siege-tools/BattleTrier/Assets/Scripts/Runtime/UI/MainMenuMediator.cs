using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kulinaria.Tools.BattleTrier.Runtime.UI
{
  public class MainMenuMediator : MonoBehaviour
  {
    [SerializeField] private Transform _parent;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _joinByCodeButton;
    [SerializeField] private Button _joinButton;

    [SerializeField] private GameObject _joinPanel;
    [SerializeField] private TMP_InputField _codeInputField;
    
    public void Initialize()
    {
      _parent.gameObject.SetActive(true);
      _joinPanel.SetActive(false);

      _createButton.onClick.AddListener(() =>
      {
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Additive);
        _parent.gameObject.SetActive(false);
      });
      _joinByCodeButton.onClick.AddListener(() => _joinPanel.SetActive(true));
      _joinButton.onClick.AddListener(() => Debug.Log("JoinGame"));
    }

    public void HideUntilAuth() => 
      _parent.gameObject.SetActive(false);
  }
}