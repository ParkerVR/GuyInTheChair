using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInterface
{ 

    public MapInterface(){

    }
    public enum Tile {
      WALL,
      DOOR,
      LOOT

    }
    public readonly Dictionary<Tile, string> TileDescriptions = new Dictionary<Tile, string>() {
      {Tile.WALL, "It's a wall..."},
      {Tile.DOOR, "It's a door! Click it to select."},
      {Tile.LOOT, "It's the loot, help Agent Pod get to it."}
    };
}
