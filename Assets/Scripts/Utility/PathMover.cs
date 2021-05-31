using UnityEngine;

/// <summary>
/// This class moves the attached object in the direction specified
/// </summary>
public class PathMover : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Type of path for movement")]
    public Path pathType;


    /// <summary>
    /// Description:
    /// Standard Unity function called every frame
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Description:
    /// Moves this object the speed and in the direction specified
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void Move()
    {
        transform.position = pathType.GetNextPosition(Time.deltaTime);
    }
}
