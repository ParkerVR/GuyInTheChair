using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class randomActionGenerator{

public static DDR.Direction getDDRAction(){
  int minInclusive = 0;
  int maxExclusive = 4;

  int num = Random.Range(minInclusive, maxExclusive); 
  if(num == 0){
    return DDR.Direction.UP;
  } else if(num == 1){
    return DDR.Direction.DOWN;
  } else if(num == 2){
    return DDR.Direction.LEFT;
  } else {
    return DDR.Direction.RIGHT;
  }

}

public static int getSimonSaysAction(int numberOfButtons){
  return (Random.Range(1, numberOfButtons+1)); 
}


}