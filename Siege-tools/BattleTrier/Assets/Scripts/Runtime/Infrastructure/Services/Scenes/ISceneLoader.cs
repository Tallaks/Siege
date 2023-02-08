using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes
{
  public interface ISceneLoader
  {
    void LoadScene(string name, bool useNetwork, LoadSceneMode mode);
  }
}