using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect : MonoBehaviour
{
    // Start is called before the first frame update
    public enum AspectTypes
    {
        PLAYER,
        CAR,
        ZOMBIE,
        STOP1,
        STOP2,
        TRAFFICLIGHT
    }
    public AspectTypes aspectType;
}
