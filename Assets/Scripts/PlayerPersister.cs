using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersister : MonoBehaviour
{
  static PlayerPersister persister;
  void Awake() {
    if(persister == null) {
      persister = this;
      DontDestroyOnLoad(persister);
    }
    else{ 
      Destroy(gameObject);
    }
  }
}
