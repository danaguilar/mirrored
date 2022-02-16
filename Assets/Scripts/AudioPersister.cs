using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPersister : MonoBehaviour
{
  static AudioPersister persister;
  void Awake() {
    if(persister == null) {
      persister = this;
      DontDestroyOnLoad(persister);
    }
    else{ 
      Destroy(this);
    }
  }
}
