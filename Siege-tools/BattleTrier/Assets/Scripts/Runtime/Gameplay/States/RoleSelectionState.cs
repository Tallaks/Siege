using Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Scenes;
using UnityEngine.SceneManagement;

namespace Kulinaria.Tools.BattleTrier.Runtime.Gameplay.States
{
  public class RoleSelectionState : ParameterlessState
  {
    private readonly ISceneLoader _sceneLoader;

    public RoleSelectionState(ISceneLoader sceneLoader) =>
      _sceneLoader = sceneLoader;

    public override void Enter() =>
      _sceneLoader.LoadScene("RoleSelection", true, LoadSceneMode.Additive);

    public override void Exit() =>
      _sceneLoader.UnloadScene("RoleSelection");
  }
}