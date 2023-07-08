using UnityEngine;
using TMPro;


public class QTE {


  // QuickTimeEvent minigame
  public enum State {
    OFF,
    ON,
    WON,
    LOST
  }

  public State state = State.OFF;
  private Timer t;

  public static string info() {
    return "Rhythm Quick-Time-Event! Press <color=yellow>space</color> when the X turns green!! Space to start.";
  }
  public void start(float minTime, float maxTime) {
    t = new Timer(Random.Range(minTime, maxTime)+1);
    state = State.ON;

  }
  public string iterate() {

    switch (state) {
      case State.OFF:
        return "INVALID STATE";

      case State.ON:
        t.iterateTimer();
        if (t.timeLeft == 0) {state = State.LOST;}
        float qtePercentage =(t.timeLeft / t.timeSet);
        int qteQuintile = (int)(qtePercentage*5.0)+1; // Counts down 5-4-3-2-1


        string textOutput = getQTEAnimationText(qteQuintile);
        if (Globals.DEBUG) {
          textOutput += $"\n{t.getTimeString()} {qteQuintile}"; 
        }

        if (Input.GetKeyDown("space")) { 
          if (qteQuintile == 1) {
            state = State.WON;
          } else {
            state = State.LOST;
          }
        }
        return textOutput;
      
      case State.WON:
        return "WON!";
      case State.LOST:
        return "LOST :(";

    }
    return "iterateQTE should return string";
  }

  string getQTEAnimationText (int qteQuintile) {
    string[] markColors = {"yellow", "yellow", "yellow", "blue"};
    for (int i = 0; i < 5-qteQuintile; i++) {
      markColors[i] = "green";
    }
    
    string qteAnimationText = @$"
 / ============= \
<  <color={markColors[0]}>O   <color={markColors[1]}>O   <color={markColors[2]}>O   <color={markColors[3]}>X</color></color></color></color>  >
 \ ============= /
";
    return qteAnimationText;
  }
}