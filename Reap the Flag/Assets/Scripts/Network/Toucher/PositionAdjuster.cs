using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    public void adjustPosition(GameObject obj, WorldLocation location, WorldRotation rotation) {
        obj.transform.position = new Vector3();
    }
}
