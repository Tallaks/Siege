#if UNITY_EDITOR
using Kulinaria.Tools.BattleTrier.Editor.Validators;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidationRule(typeof(BootSceneOpened))]

namespace Kulinaria.Tools.BattleTrier.Editor.Validators
{
  public class BootSceneOpened : SceneValidator
  {
    protected override void Validate(ValidationResult result)
    {
      SceneReference theScene = ValidatedScene;

      if (theScene.Name != "BootScene" && theScene is
          {
            IsActive: true,
            IsLoaded: true
          })
        result.AddError("Not Boot Scene Opened!");
    }
  }
}
#endif