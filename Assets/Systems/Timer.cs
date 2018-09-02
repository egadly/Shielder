using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Timer {

  public int time;
  public int goal;

  public Timer( int input ) {
    time = 0;
    goal = input;
  }

  public void Set( int input ) {
    goal = input;
    time = 0;
  }

  public int Increment() {
    time += 1;
    return time;
  }

  public static implicit operator bool( Timer input ) {
    if ( input.time >= input.goal ) return true;
    else return false;
  }



}
