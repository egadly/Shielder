using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lord : FrameLimiter {

  protected Timer _checkTimer = new Timer(30);
  
  protected override void UpdatePlus() {
    if ( _checkTimer ) {
      if ( transform.childCount == 0 ) World.Message("winna");
      else _checkTimer.Set(30);
    } else {
      _checkTimer.Increment();
    }
  }

}
