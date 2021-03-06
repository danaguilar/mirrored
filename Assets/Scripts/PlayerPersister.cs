using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersister : MonoBehaviour
{
  public static PlayerPersister persister;
  void Awake() {
    if(persister == null) {
      persister = this;
      DontDestroyOnLoad(persister);
    }
    else{ 
      Destroy(gameObject);
    }
  }

  public static PlayerMovement GetPlayerMovement() {
    return persister.GetComponent<PlayerMovement>();
  }
}
