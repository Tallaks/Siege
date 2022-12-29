using System.Collections;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Infrastructure.Services.Coroutines
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator routine);
    void StopAllCoroutines();
    void StopCoroutine(Coroutine routine);
  }
}