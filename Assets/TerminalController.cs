using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class TerminalController : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI TerminalTextMeshPro;




  // Start is called before the first frame update
  void Start() {

    startQTE(5, 8);
    
  }

  // Update is called once per frame
  void Update() { 
    iterateQTE();
  }

  // QuickTimeEvent minigame
  private enum QTEState {
    OFF,
    ON,
    WON,
    LOST
  }

  private QTEState qteState = QTEState.OFF;

  void startQTE(float minTime, float maxTime) {
    float randomTime = Random.Range(minTime, maxTime);
    
    qteState = QTEState.ON;

    setTimer(randomTime+1);
  }

  void iterateQTE() {

    switch (qteState) {
      case QTEState.ON:
        string timerText = iterateTimer();
        if (timeLeft == 0) {qteState = QTEState.LOST;}
        float qtePercentage =(timeLeft / timeSet);
        int qteQuartile = (int)(qtePercentage*4.0)+1; // Counts down 4-3-2-1
        TerminalTextMeshPro.text = @$"MINIGAME RUNNING!!!!! HIT SPACE BUTTON AT 1!
hehe {timerText} {qteQuartile}";
        if (Input.GetKeyDown("space")) { 
          if (qteQuartile == 1) {
            qteState = QTEState.WON;
          } else {
            qteState = QTEState.LOST;
          }
        }
        break;
      case QTEState.OFF:
        break;
      case QTEState.WON:
        TerminalTextMeshPro.text = "WON!";
        break;
      case QTEState.LOST:
        TerminalTextMeshPro.text = "LOST :(";
        break;


    }
    
  }

  private float timeSet;
  private float timeLeft;
  private bool timerOn;
  void setTimer(float setTime) {
    timeSet = setTime;
    timeLeft = setTime;
    timerOn = true;
  }
  string iterateTimer() {
    if(timerOn) {
      if (timeLeft > 0) {
        timeLeft -= Time.deltaTime;
        string timeString = updateTimer(timeLeft);
        return timeString;
      } else {
        timeLeft = 0;
        timerOn = false;
        return "00:00";
      }
    } 
    return "";
  }
  // Sets timer in seconds and returns the string
  string updateTimer(float setTime) {
    float minutes = Mathf.FloorToInt(setTime / 60);
    float seconds = Mathf.FloorToInt(setTime % 60);

    timeLeft = setTime;

    return string.Format("{0:00} : {1:00}", minutes, seconds);
  }
}


