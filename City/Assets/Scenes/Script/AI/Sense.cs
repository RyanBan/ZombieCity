using UnityEngine;
using System.Collections;

public class Sense : MonoBehaviour
{
    public bool enableDebug = true;
    public Aspect.AspectTypes aspectName = Aspect.AspectTypes.PLAYER;
    public float detectionRate = 1.0f;

    protected float elapsedTime = 0.0f;

    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

    // Use this for initialization
    void Start()
    {
        elapsedTime = 0.0f;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSense();
    }
}
