using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

// This class should handle all text wrapping to terminal
public class TerminalController : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI TerminalTMP;

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
  private List<(TS, TS)> StateMachineValidTransitions = new List<(TS, TS)>{
    (TS.HOME, TS.MAP_HOVER),
      (TS.MAP_HOVER, TS.HOME),
    (TS.MAP_HOVER, TS.MAP_CLICK),
    (TS.MAP_CLICK, TS.MINIGAME_INFO),
      (TS.MINIGAME_INFO, TS.MAP_HOVER),
    (TS.MINIGAME_INFO, TS.MINIGAME_PLAY),
    (TS.MINIGAME_PLAY, TS.MINIGAME_WRAPUP),
    (TS.MINIGAME_WRAPUP, TS.HOME),
  };

  private Dictionary<TS, TS> StateMachineStandardProgression = new Dictionary<TS, TS>() {
    {TS.HOME, TS.MAP_HOVER},
    {TS.MAP_HOVER, TS.MAP_CLICK},
    {TS.MAP_CLICK, TS.MINIGAME_INFO},
    {TS.MINIGAME_INFO, TS.MINIGAME_PLAY},
    {TS.MINIGAME_PLAY, TS.MINIGAME_WRAPUP},
    {TS.MINIGAME_WRAPUP, TS.HOME},
  };

  private TS ts;

  private QTE qteController;
  private MapInterface mapInterface;
  private string home_text_feed = "";
  // Start is called before the first frame update
  void Start() { 
    ts = TS.HOME;
    home_text_feed = "";
    mapInterface = new MapInterface();
    qteController = new QTE();

  }

  // Update is called once per frame
  void Update() { 
    switch (ts) {
      case TS.HOME: 
        TerminalTMP.text = getHomeText();
        break; 
      case TS.MAP_HOVER:
        TerminalTMP.text = getHoverText(MapInterface.Tile.DOOR);
        break;
      case TS.MAP_CLICK:
        TerminalTMP.text = getHoverText(MapInterface.Tile.DOOR);
        break;
      case TS.MINIGAME_INFO:
      
        TerminalTMP.text = QTE.info();
        break;
      case TS.MINIGAME_PLAY:
        switch (qteController.state) {
          case QTE.State.OFF:
            qteController.start(4,8);
            TerminalTMP.text = QTE.info();
            break;
          case QTE.State.ON:
            TerminalTMP.text = qteController.iterate();
            break;
          case QTE.State.LOST:
            ts = TS.MINIGAME_WRAPUP;
            TerminalTMP.text = qteController.iterate();
            break;
          case QTE.State.WON:
            ts = TS.MINIGAME_WRAPUP;
            TerminalTMP.text = qteController.iterate();
            break;
        }
        break;
      case TS.MINIGAME_WRAPUP:
        TerminalTMP.text = qteController.iterate();
        break;
    }
    
    if (Input.GetKeyDown("n")) { 
      ts = StateMachineStandardProgression[ts];
    }
  }



  
  string getHomeText() {
    string DEFAULT_HOME_TEXT = @"
You're the Guy In The Chair. Your bud is on a heist in a space ship, but he's gonna need your help!

Beat some minigames and help him win. You got this!
";
    return DEFAULT_HOME_TEXT + home_text_feed;
  }

  string getHoverText(MapInterface.Tile t){
    return mapInterface.TileDescriptions[t];
  }

}


