using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class Typer {
  public enum CursorState {
    WAITING,
    CORRECT,
    INCORRECT
  };

  // Letter count of typerWords() excluding spaces between words - for scoring purposes
  private const int TOTAL_LETTER_COUNT = 112;
  // size of the (sliding) text shown on screen at a given time, e.g. in AB[CDE]FG... -> ABC[DEF]GH..., [CDE] and [DEF] are the windows (of size 3)
  private const int TYPER_WINDOW_SIZE = 15;
  public string TEXT_WHITESPACE_BUFFER;

  private int typerWordsCursor;
  private int localCurrentBeat;
  private CursorState cursorState;

  // Initially an empty set, but any time a letter press is missed/typed wrong, its index (into typerWords) is added to the set.
  // It will be used for setting a specific color for the missed letters in the "completedLetters" part of windowText()'s window string.
  private HashSet<int> missedLetterIndices = new HashSet<int>();



  private string typerWords;
  public Typer(int initialBeat) {
    localCurrentBeat = initialBeat;
    cursorState = CursorState.WAITING;
    typerWordsCursor = 7;
    TEXT_WHITESPACE_BUFFER = new string (' ', TYPER_WINDOW_SIZE);
    typerWords = TEXT_WHITESPACE_BUFFER + "ATTENTION CONCERTGOERS THERE IS A WILD CHIMPANZEE ON THE LOOSE INSIDE THE CONCERT VENUE PLEASE HEAD TO THE NEAREST EXIT IMMEDIATELY." + TEXT_WHITESPACE_BUFFER;

  }

  // Something to print above minigame console text.
  public static string info() {
    return "PA Sound System Console v1.2.\nEnter text to broadcast, following the dictated typing pace:";
  }


  private List<string> generateWindowText(int currentBeat) {
    int windowLeftIndex = typerWordsCursor - 7;
    int windowRightIndex = typerWordsCursor + 7;

    // Letters in the window to the left of the midpoint, to color according to whether they were hit successfully or not.
    string completedLetters = "";
    for (int i = windowLeftIndex; i < typerWordsCursor; i++) {
      string colorStr = "#00FFFF";
      if (missedLetterIndices.Contains(i)) {
        // they fucked up
        colorStr = "red";
      }
      string colorStringAdd = "<color=" + colorStr + ">" + typerWords[i] + "</color>";
      completedLetters += colorStringAdd;
    }
    
    string cursorColorStr;
    if (cursorState == CursorState.WAITING) {
      cursorColorStr = "white";
    } else if (cursorState == CursorState.CORRECT) {
      cursorColorStr = "#00FFFF";
    } else {
      cursorColorStr = "red";
    }
    string cursorLetter = "<u><color=" + cursorColorStr + ">" + typerWords[typerWordsCursor] + "</color></u>";
    // Letters in the window to the right of the midpoint (including it), to color white.
    string incomingLetters = "<color=white>" + typerWords.Substring(typerWordsCursor+1, 7) + "</color>";

    string header = "==      ▼       ==";
    string window = "| " + completedLetters + cursorLetter + incomingLetters + " |";
    string footer = "==      ▲       ==";

    return new List<string> {
      header,
      window,
      footer,
    };
  }

  public (string, bool) Update(int currentBeat, ArrayList keypresses) {

    bool shouldLockout = false;
    // Update cursorState if key press occured
    if (keypresses.Contains(typerWords[typerWordsCursor].ToString().ToUpper()) || typerWords[typerWordsCursor] == ' ') {
      // Correct key press
      if ( typerWords[typerWordsCursor] != ' ') {
        Debug.Log("Correct: " + typerWords[typerWordsCursor]);
        shouldLockout = true;
      }
      cursorState = CursorState.CORRECT;

    } else if (keypresses.Count > 0) {
      // Incorrect key press
      cursorState = CursorState.INCORRECT;
      Debug.Log("Incorrect: " + typerWords[typerWordsCursor]);
      var stringsToDebug = new ArrayList().Cast<string>().ToArray();
      Debug.Log(string.Join(" ", stringsToDebug));
      shouldLockout = true;
    } 

    // Generate text to display
    List<string> windowTextLines = generateWindowText(currentBeat);
    string textOutput = "\n" + windowTextLines[0] + "\n" + windowTextLines[1] + "\n" + windowTextLines[2] + "\n";

    // If new beat, then move cursor forward, update local beat count, and reset cursor state.
    if (currentBeat > localCurrentBeat) {
      if (cursorState == CursorState.WAITING || cursorState == CursorState.INCORRECT) {
        // Did not press key in time or incorrect key
        missedLetterIndices.Add(typerWordsCursor);
      } 
      typerWordsCursor++;
      cursorState = CursorState.WAITING;
      localCurrentBeat = currentBeat;
    }

    // Return text to print to terminal
    return (textOutput, shouldLockout);
  }
}