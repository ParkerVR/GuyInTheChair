using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// Generally uses seconds
public class Timer {

  public bool timerOn;
  public bool wasLastFrameBeat;
  public float timeTotal; 
  public float beatLength;
  
  public float lastBeatTime;
  public int beatSum;

  public Timer() {
    timerOn = true;
    lastBeatTime = 0;
    beatLength = 60 / Globals.BPM;
    beatSum = 0;
    wasLastFrameBeat = true;
  }

  public string getTimeString(){
  
    float minutes = Mathf.FloorToInt(timeTotal / 60);
    float seconds = Mathf.FloorToInt(timeTotal % 60);

    return string.Format("{0:00} : {1:00}", minutes, seconds);
  
  }

  // Returns % drift to the nearest beat the current frame is, rounding to 0 for perfects
  public float iterateTimer() {
    if(timerOn) {
      float retval = calculateDriftPercentage();
      timeTotal += Time.deltaTime;
      // Just resetting flag after beats.
      if (wasLastFrameBeat) {
        wasLastFrameBeat = false;
      } else {
        // PERFECT! this is a beat frame.
        if (lastBeatTime > beatLength*beatSum) {
          wasLastFrameBeat = true;
          beatSum += 1;
          retval = 0;
        }
      }
      return retval;
    }
    return -1;
  }

  // max drift percentage is .50 (50%)
  private float calculateDriftPercentage() {
    float driftSeconds = lastBeatTime-beatLength*beatSum;
    float driftPercentage = driftSeconds / beatLength;
    if (driftPercentage < 0.5) {
      return driftPercentage;
    } else {
      return 1-driftPercentage;
    }
  }
  private int closestBeatNum(float timeNow){
    int current = beatSum;
    float threshold = lastBeatTime + (beatLength / 2);
    if (timeTotal > threshold) {
      return beatSum + 1;
    }
    else return beatSum;
  }
 
}