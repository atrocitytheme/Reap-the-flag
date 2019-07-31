using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType 
{
    // Start is called before the first frame update
    NON_INITIALIZED,
    INITIALIZED,
    IDLE,
    DAMAGED, // state when damaged
    KILLED, // state when killed
    PENDING,
    OB,
    EXIT
}
