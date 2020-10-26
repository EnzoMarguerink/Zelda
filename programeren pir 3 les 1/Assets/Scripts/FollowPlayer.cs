using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (transform.position.x - target.position.x >= 8)
            StartMove(-16, 0);
        if (transform.position.x - target.position.x <= -8)
            StartMove(16, 0);
        if (transform.position.y - target.position.y >= 5.5f)
            StartMove(0, -11);
        if (transform.position.y - target.position.y <= -5.5f)
            StartMove(0, 11);
    }

    private void StartMove(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }
}
