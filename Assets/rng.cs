using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class randomActionGenerator{

public static string getDDRAction(){
  int minInclusive = 0;
  int maxExclusive = 4;

  int num = Random.Range(minInclusive, maxExclusive); 
  if(num == 0){
    return "Up";
  } else if(num == 1){
    return "Down";
  } else if(num == 2){
    return "Left";
  } else {
    return "Right";
  }
  

}

public static int getSimonSaysAction(int numberOfButtons){
  return (Random.Range(0, numberOfButtons)); 
}


}