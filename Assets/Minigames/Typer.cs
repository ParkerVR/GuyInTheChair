using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer {
  private int typerWindowCursorPosition = 0;

  private static int totalTyperLetterCount = 112;
  private static int typerWindowLetterCount = 25;

  public static string info() {
    return "PA Sound System Console v1.2.\nEnter text to broadcast, following the dictated typing pace:";
  }

  private static string typerWords() {
    return "ATTENTION CONCERTGOERS THERE IS A WILD CHIMPANZEE ON THE LOOSE INSIDE THE CONCERT VENUE PLEASE HEAD TO THE NEAREST EXIT IMMEDIATELY.";
  }

  private List<string> windowText() {
    string whitespaceBuffer = new string (' ', typerWindowLetterCount+1);
    string header = "";
    string window = "[]";
    string footer = "";

    return new List<string> {
      header,
      window,
      footer,
    };
  }

  /*
  public static string countdown(int beatsLeft) {
    int secondsLeft = (int)((float)beatsLeft / beatsPerSecond);
    return string.Format("<b>Seconds until the band's bloodthirsty pet chimp breaches containment: <b>{0}</b>.", secondsLeft);
  }*/

  public string iterate() {
   /*  switch (state) {
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
    return "iterateQTE should return string"; */
    string textOutput = "";

    

    return textOutput;
  }

  string getQTEAnimationText (int qteQuintile) {
    string[] markColors = {
      "yellow",
      "yellow",
      "yellow",
      "blue"
    };
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