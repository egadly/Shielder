using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimiter : MonoBehaviour
{

  public const float frameRate = 60f;
  public const float frameTime = 1 / frameRate;

  float _accumulatedTime;


  void Update()
  {
    _accumulatedTime += Time.deltaTime;
    if (_accumulatedTime >= frameTime)
    {
      _accumulatedTime -= frameTime;
      UpdatePlus();
    }
    UpdateThis();
  }

  protected virtual void UpdatePlus() { }
  protected virtual void UpdateThis() { }

}
