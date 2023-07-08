public class QTE {

  // QuickTimeEvent minigame
  public enum QTEState {
    OFF,
    INFO,

    ON,
    WON,
    LOST
  }


  void startQTE(float minTime, float maxTime) {
    float randomTime = Random.Range(minTime, maxTime);
    setTimer(randomTime+1);
    qteState = QTEState.ON;

  }

  void iterateQTE() {

    switch (qteState) {
      case QTEState.OFF:
        break;
      case QTEState.INFO:
        TerminalTextMeshPro.text = "Rhythm Quick-Time-Event! Press <color=yellow>space</color> when the X turns green!! Space to start.";
        if (Input.GetKeyDown("space")) { 
          startQTE(5, 8);
        }
        break;
      case QTEState.ON:
        string timerText = iterateTimer();
        if (timeLeft == 0) {qteState = QTEState.LOST;}
        float qtePercentage =(timeLeft / timeSet);
        int qteQuintile = (int)(qtePercentage*5.0)+1; // Counts down 5-4-3-2-1

        string wrappedText = getQTEAnimationText(qteQuintile) + $"\n{timerText} {qteQuintile}"; 
        TerminalTextMeshPro.text = wrappedText;

        if (Input.GetKeyDown("space")) { 
          if (qteQuintile == 1) {
            qteState = QTEState.WON;
          } else {
            qteState = QTEState.LOST;
          }
        }
        break;
      
      case QTEState.WON:
        TerminalTextMeshPro.text = "WON!";
        break;
      case QTEState.LOST:
        TerminalTextMeshPro.text = "LOST :(";
        break;
    }
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