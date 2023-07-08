using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TerminalController : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI TerminalTextMeshPro;

  // Statemachine Graphic: https://www.figma.com/file/gwJvQdj8agWhFsik7Ykp9s/StateMachines

  // TS = TerminalState
  enum TS {
    HOME,
    MAP_HOVER,
    MAP_CLICK,
    MINIGAME_INFO,
    MINIGAME_PLAY,
    MINIGAME_WRAPUP
  }

  // 'Reverse Flow' transitions extra indented.
  List<(TS, TS)> StateMachineValidTransitions = {
    {TS.HOME, TS.MAP_HOVER},
      {TS.MAP_HOVER, TS.HOME},
    {TS.MAP_HOVER, TS.MAP_CLICK},
    {TS.MAP_CLICK, TS.MINIGAME_INFO},
      {TS.MINIGAME_INFO, TS.HOVER},
    {TS.MINIGAME_INFO, TS.MINIGAME_PLAY},
    {TS.MINIGAME_PLAY, TS.MINIGAME_WRAPUP},
    {TS.MINIGAME_WRAPUP, TS.HOME},
    {TS.MINIGAME_WRAPUP, TS.MAP_HOVER}
  };

  private TS ts;

  private QTE qteController;

  // Start is called before the first frame update
  void Start() { 
    ts = TS.HOME;

    qteController = QTE();
    
  }

  // Update is called once per frame
  void Update() { 
    if (ts == TS.HOME) {
      TerminalTextMeshPro.text = getHomeText();
    }
  }

  string DEFAULT_HOME_TEXT = @"
You're the Guy In The Chair. Your bud is on a heist in a space ship, but he's gonna need your help!

Beat some minigames and help him win. You got this!
";

  def getHomeText() {

  }

}


