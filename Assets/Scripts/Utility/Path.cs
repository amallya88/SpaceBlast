using UnityEngine;

public abstract class Path : MonoBehaviour
{
    public abstract Vector3 GetNextPosition(float dt);
}

