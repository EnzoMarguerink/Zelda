using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOctorok : Enemy
{
    [SerializeField] private int maxDistance;

    [SerializeField] private GameObject projectile;
    [SerializeField] private int chanceToShoot;

    private bool debug = true;

    public new void Start()
    {
        base.Start();
        damage = 1;
        hp = 2;
        ChooseMovementTarget();
        UpdateAnimatorValues();

        SetRoamingTarget(new Vector2(-2, 0));
    }

    public void ChooseMovementTarget()
    {
        Vector2 randomTarget = new Vector2();
        bool foundTarget = false;
        int maxtries = 4;
        int trycounter = 0;

        origin = transform.position;
    }

    public RaycastHit2D IsPathClear(Vector2 target)
    {
        //return new RaycastHit2D();
        Vector2 dest = new Vector2(origin.x + target.x, origin.y + target.y);
        float dist = Vector2.Distance(origin, dest);

        return Physics2D.Raycast(origin, target, dist, 1 << LayerMask.NameToLayer("Walls"));
    }

    public override void ReachedDestination()
    {
    }

    public void Shoot()
    {
    }

    public void AimClosestTowardsPlayer()
    {
    }

    public void LaunchProjectile()
    {
    }

    public void ShootCompleted()
    {
        ChooseMovementTarget();
    }
}