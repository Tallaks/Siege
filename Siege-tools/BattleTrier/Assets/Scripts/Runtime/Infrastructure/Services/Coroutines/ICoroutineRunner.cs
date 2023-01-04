using System.Collections;
using UnityEngine;

namespace Kulinaria.Tools.BattleTrier.Runtime.Infrastructure.Services.Coroutines
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator routine);
    void StopAllCoroutines();
    void StopCoroutine(Coroutine routine);
  }
}