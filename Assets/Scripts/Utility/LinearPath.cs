using System.Collections;
using UnityEngine;

/// <summary>
///  Class to store and handle 2D linear paths
/// </summary>
public class LinearPath : MonoBehaviour
{
    [Tooltip("Starting position for path")]
    public Vector3 startPosition = Vector3.negativeInfinity;

    [Tooltip("Target or destination position")]
    public Vector3 stopPosition = Vector3.positiveInfinity;

    [Tooltip("Speed for movement along the direction vector")]
    public float speed = 1.0f;

    [Tooltip("Continuously oscillate between the path endpoints")]
    public bool oscillate = false;

    // the previous position of the game object on the path
    private Vector3 prevPos = new Vector3();

    // has the gameobj reached the stop position
    private bool moveFinished = false;

    // Use this for initialization
    public virtual void Start()
    {
        if(Vector3.Equals(startPosition, Vector3.negativeInfinity))
            startPosition = transform.position;
        
        if (Vector3.Equals(stopPosition, Vector3.positiveInfinity))
            stopPosition = startPosition;

        prevPos = startPosition;
    }

    /// <summary>
    /// Description:
    /// Returns the Vector3 of the next position along the defined path in World Coordiantes 
    /// based on the speed and time elapsed
    /// Inputs: 
    /// Time.deltaTime as dt
    /// Returns: 
    /// Vector3
    /// </summary>
    public virtual Vector3 GetNextPosition(float dt)
    {
        Vector3 nextPos;

        if (moveFinished && !oscillate)
            return stopPosition;

        // algorithm:
        // 1. calculate next position
        nextPos = Vector3.MoveTowards(prevPos, stopPosition, speed * dt);

        // 2. validate next position
        if (Vector2.Equals(nextPos, stopPosition))
        {
            nextPos = stopPosition;
            if (oscillate)
            {
                stopPosition = startPosition;
                startPosition = nextPos;
            }
            else
                moveFinished = true;
        }
        prevPos = nextPos;
        return nextPos;
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
