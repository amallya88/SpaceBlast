using System.Collections;
using UnityEngine;

public class SinusoidalPath : LinearPath
{
    [Tooltip("Cycles (number of periods)")]
    public float cycles = 1.0f;

    // the previous position of the game object 
    // on a straight line between start and stop positions
    private Vector3 previousPos = new Vector3();

    // has the gameobj reached the stop position
    private bool movFinished = false;

    // frequency
    private float frequency;

    // angle between starting position and x-axis
    private float movAngle;

    // Use this for initialization
    public override void Start()
    {
        previousPos = startPosition;
        movAngle = Vector3.Angle(stopPosition - startPosition, Vector3.right);

        // cycles per meter
        frequency = cycles / Vector3.Distance(startPosition, stopPosition);
    }

    public override Vector3 GetNextPosition(float dt)
    {
        Vector3 nextPos, nextScreenPos;

        if (movFinished && !oscillate)
            return stopPosition;

        // algorithm:
        // 1. calculate next position
        var x_inc = speed * dt;
        nextPos = Vector3.MoveTowards(previousPos, stopPosition, x_inc);
        
        // nextPos is the position of object on straight line joining start and stop
        // modify y coordinate to make object move in sinusoidal pattern
        nextScreenPos = nextPos;
        var sin_x = Mathf.Sin(2 * Mathf.PI * frequency * (previousPos.x + x_inc));

        // apply rotation to movement
        if (movAngle > 1)
        {
            nextScreenPos.x = nextScreenPos.x * Mathf.Cos(movAngle) - nextScreenPos.y * Mathf.Sin(movAngle);
            nextScreenPos.y = nextScreenPos.x * Mathf.Sin(movAngle) + nextScreenPos.y * Mathf.Cos(movAngle);
        }
        nextScreenPos.y += sin_x;

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
                movFinished = true;
        }
        previousPos = nextPos;
        return nextScreenPos;
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
