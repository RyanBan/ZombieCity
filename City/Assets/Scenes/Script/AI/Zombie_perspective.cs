using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_perspective : Sense
{

    private enum ControlMode
    {
        Default,
        Walk_findPlayer,
        Wander_findPlayer,
        Wander_findCar
    }
    [SerializeField] private ControlMode mode = ControlMode.Default;

    public int fieldOfView = 90;
    public int viewDistance = 20;

    private Transform playerTransform;
    private Vector3 rayDirection;

    private Vector3 targetPosition;

    [SerializeField] public float m_moveSpeed = 0.2f;

    private float rotationSpeed = 2.0f;
    private float targetPositionTolerance = 5f;
    private float minX = -10.0f;
    private float maxX = 10.0f;
    private float minZ = -10.0f;
    private float maxZ = 10.0f;

    int quit = 0;
    protected override void Initialize()
    {
        switch (mode)
        {
            case ControlMode.Default:
                playerTransform = null;
                break;
            case ControlMode.Walk_findPlayer:
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                break;
            case ControlMode.Wander_findPlayer:
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                break;
            case ControlMode.Wander_findCar:
                playerTransform = GameObject.FindGameObjectWithTag("MovingCar").transform;
                break;
            default:
                Debug.LogError("Unsupported state");
                break;
        }
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
        {
            switch (mode)
            {
                case ControlMode.Default:
                    DefaultUpdate();
                    break;
                case ControlMode.Walk_findPlayer:
                    Walk_findPlayer_Update();
                   
                    break;
                case ControlMode.Wander_findPlayer:
                    GetNextPosition();
                    Wander_findPlayer_Update();
                    break;
                case ControlMode.Wander_findCar:
                    GetNextPosition();
                    Wander_findCar_Update();
                    break;
                default:
                    Debug.LogError("Unsupported state");
                    break;
            }
            
        }
    }

    void DefaultUpdate()
    {
        transform.position += transform.forward * m_moveSpeed * 1 * Time.deltaTime;
    }

    //Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;
        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {
                        //move toward to player
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rayDirection), 100.0f * Time.deltaTime);
                        
                    }
                }
            }
        }
    }
    void Walk_findPlayer_Update()
    {
        transform.position += transform.forward * m_moveSpeed * 1 * Time.deltaTime;
        DetectAspect();
    }

    void Wander_findPlayer_Update()
    {

        if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance)
        {
            GetNextPosition();
        }
        
        if(quit == 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * m_moveSpeed * 2 * Time.deltaTime;
        }

        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;
        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {
                        //move toward to player
                        quit = 1;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rayDirection), 100.0f * Time.deltaTime);
                        transform.position += transform.forward * m_moveSpeed * 3 * Time.deltaTime;

                    }
                }
                if(aspect == null)
                {
                    quit = 0;
                }
            }
        }
    }

    void Wander_findCar_Update()
    {

        if (Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance)
        {
            GetNextPosition();
        }

        if (quit == 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * m_moveSpeed * 1 * Time.deltaTime;
        }

        RaycastHit hit;
        rayDirection = playerTransform.position - transform.position;
        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {
                        //move toward to player
                        quit = 1;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rayDirection), 100.0f * Time.deltaTime);
                        transform.position += transform.forward * m_moveSpeed * 2 * Time.deltaTime;

                    }
                }
                if (aspect == null)
                {
                    quit = 0;
                }
            }
        }
    }


    void GetNextPosition()
    {
        targetPosition = new Vector3(transform.position.x + Random.Range(minX, maxX), transform.position.y, transform.position.z + Random.Range(minZ, maxZ));
    }
    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (playerTransform == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += fieldOfView * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= fieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
