using UnityEngine;

/// <summary>
///  Class to store and handle 2D circular paths
/// </summary>
public class CircularPath : Path
{
    [Tooltip("Center for circular path")]
    public Vector3 center = Vector3.negativeInfinity;

    [Tooltip("Speed of rotation (deg/s)")]
    public float speed = 1.0f;

    [Tooltip("Move indefinitely (forever)")]
    public bool loopMotion = false;

    private Vector3 radius;

    // the previous position of the game object on the path
    private float currAngle = 0.0f;

    // has the gameobj reached the stop position
    private bool moveFinished = false;

    // Use this for initialization
    void Start()
    {
        if (Vector3.Equals(center, Vector3.negativeInfinity))
            center = transform.position + 8 * Vector3.right;
        radius = transform.position - center;
    }

    /// <summary>
    /// Description:
    /// Returns the Vector3 of the next position along the defined circular path in World Coordiantes 
    /// based on the required speed
    /// Inputs: 
    /// Time.deltaTime as dt
    /// Returns: 
    /// Vector3 next position of object in world coords
    /// </summary>
    public override Vector3 GetNextPosition(float dt)
    {
        Vector3 nextPos;

        if (moveFinished)
            return transform.position;

        // algorithm:
        // 1. calculate angle increment
        var next_angle = currAngle + (speed * Mathf.Deg2Rad * dt);

        // rotate radius vector by required angle
        // rotation is accomplished by using rotation matrix for 2D vector space [cos(theta) -sin(theta); sin(theta) cos(theta)] * (x y) col vector
        var xP = radius.x * Mathf.Cos(next_angle) - radius.y * Mathf.Sin(next_angle);
        var yP = radius.x * Mathf.Sin(next_angle) + radius.y * Mathf.Cos(next_angle);
        nextPos = center + new Vector3(xP, yP, radius.z);

        // 2. validate next position
        if (next_angle > 2*Mathf.PI && !loopMotion)
            moveFinished = true;

        currAngle = next_angle;
        return nextPos;
    }

    public void UpdateCenter(Vector3 newCenter)
    {
        center = newCenter;
        if(!Vector3.Equals(center, newCenter))
            radius = transform.position - newCenter;

    }

    /* 
     *      FOR STANDALONE DEBUGGING
    */
    //private void Update()
    //{
    //    Move();
    //}

    //private void Move()
    //{
    //    transform.position = GetNextPosition(Time.deltaTime);
    //}
}
