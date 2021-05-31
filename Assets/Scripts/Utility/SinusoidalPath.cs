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

    // Use this for initialization
    public override void Start()
    {
        if (Vector3.Equals(startPosition, Vector3.negativeInfinity))
            startPosition = transform.position;

        if (Vector3.Equals(stopPosition, Vector3.positiveInfinity))
            stopPosition = startPosition + Vector3.right;

        previousPos = startPosition;

        // cycles per meter
        frequency = 0.3f;
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

    public void UpdateStopPosition(Vector3 newTargetPos)
    {
        startPosition = previousPos;
        stopPosition = newTargetPos;
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
