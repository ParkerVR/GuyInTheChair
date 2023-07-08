

public class Timers {
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