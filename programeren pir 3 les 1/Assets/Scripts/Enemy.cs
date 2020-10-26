using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle = 0,
    roaming,
    shooting,
    death
}

public class Enemy : MonoBehaviour
{
    private Animator animator;
    protected Player player;

    [SerializeField] protected Direction direction;
    [SerializeField] protected EnemyState state;

    [SerializeField] protected Vector3 origin;
    [SerializeField] protected Vector3 destination;
    [SerializeField] protected Vector3 startPosition;

    [SerializeField] private float timePerTile;
    private float lerpTimer = 0;
    private float distanceToLerp;

    [SerializeField] protected int hp;
    [SerializeField] protected int damage = 0;

    public void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("animator not found " + name);
        }

        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("player not found" + name);
        }
        direction = Direction.left;
        state = EnemyState.roaming;

        origin = transform.position;
        startPosition = transform.position;

        ResetLerp();

        tag = "Enemy";

        UpdateAnimatorValues();
    }

    public virtual void FixedUpdate()
    {
        UpdateAnimatorValues();

        
        if (state == EnemyState.roaming)
        {
            ProcessRoaming();
        }
    }

    public void UpdateAnimatorValues()
    {
        if (animator != null)
        {
            animator.SetInteger("State", (int)state);
            animator.SetFloat("Direction", (float)direction);
        }
        else
        {
            Debug.LogError("no animator found on" + name);
        }
    }

    public void ResetLerp()
    {
        lerpTimer = 0.0f;
    }

    protected void SetRoamingTarget(Vector2 target)
    {
        ResetLerp();

        origin = transform.position;

        destination = origin + new Vector3(target.x, target.y, 0f);

        distanceToLerp = Vector2.Distance(origin, destination);

        direction = CalculateDirection(origin, destination);

        state = EnemyState.roaming;
    }

    public Direction CalculateDirection(Vector2 pos1, Vector2 pos2, int amountdirections = 4)
    {
        float angle = 360f - CalculateAngle(pos1, pos2);

        float part = 360f / amountdirections;

        if (angle.Equals(360))
        {
            angle = 0;
        }

        int result = Mathf.RoundToInt(angle / part);

        if (result == amountdirections)
        {
            result = 0;
        }
        return (Direction)result;
    }

    public float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    public void ProcessRoaming()
    {
        lerpTimer += Time.deltaTime;

        if (lerpTimer > (timePerTile * distanceToLerp))
        {
            lerpTimer = (timePerTile * distanceToLerp);
        }

        if (!distanceToLerp.Equals(0f) && !timePerTile.Equals(0f))
        {
            float perc = lerpTimer / (timePerTile * distanceToLerp);
            transform.position = Vector3.Lerp(origin, destination, perc);
        }

        if (lerpTimer.Equals(timePerTile * distanceToLerp))
        {
            ReachedDestination();
        }
    }

    public virtual void ReachedDestination()
    {
        ResetLerp();
        state = EnemyState.idle;
    }
}
