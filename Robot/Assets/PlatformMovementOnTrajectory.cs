using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementOnTrajectory : MonoBehaviour
{
    private int currentTrajectoryPointIndex = 0;
    private Vector2 nextWayPoint;
    private float distanceToTargetWayPoint;
    private Vector2 currentDirection;
    private int incrementValue = 1;

    public bool canMove = true;
    public List<Vector2> Trajectory;
    public float moveSpeed = 1f;
    public bool isCyclicMovement = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTrajectoryPointIndex = 0;
        currentDirection = (Trajectory[0] - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
        distanceToTargetWayPoint = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Trajectory[0]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (distanceToTargetWayPoint > 0.15f)
        {
            if (!canMove) return;
            gameObject.transform.Translate(currentDirection * Time.fixedDeltaTime * moveSpeed);
            distanceToTargetWayPoint = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Trajectory[currentTrajectoryPointIndex]);
            Debug.Log($"Moving...Current Point Index is {currentTrajectoryPointIndex}. Distance: {distanceToTargetWayPoint}.Next Point {Trajectory[currentTrajectoryPointIndex]}");
        }
        else if (currentTrajectoryPointIndex < Trajectory.Count && currentTrajectoryPointIndex >= 0)
        {
            currentTrajectoryPointIndex+=incrementValue;

            if (currentTrajectoryPointIndex > Trajectory.Count)
            {
                distanceToTargetWayPoint = 1f;
                return;
            }

            currentDirection = (Trajectory[currentTrajectoryPointIndex] - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
            distanceToTargetWayPoint = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Trajectory[currentTrajectoryPointIndex]);
        }
        else
        {
            if(currentTrajectoryPointIndex >= Trajectory.Count)
            {
                incrementValue = -1;
                currentTrajectoryPointIndex = Trajectory.Count;
            }
            if(currentTrajectoryPointIndex < 0)
            {
                incrementValue = 1;
                currentTrajectoryPointIndex = 0;
            }
            if (!isCyclicMovement) canMove = false;
        }
    }
}
