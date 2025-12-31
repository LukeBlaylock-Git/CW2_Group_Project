using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
public class TowerAttack : MonoBehaviour
{
    //Other Script being used
    public TowerShooter shooter;

    //Target vairables
    public float TowerRange = 5f;
    public Transform TowerRotate; // The Range, Rotating bit and the speed it turns
    public float TowerRotateSpeed = 5f;

    //Firing vairables
    public float FireRate = 2f; // How fast it can fire and how long it has to wait between shots
    private float Cooldown = 1f;

    public Transform CurrentlyTargeted; //Who is getting shot

    private void FixedUpdate() // I am using fixed update so it isnt being called every frame for memory and lag reasons
    {
        LocateTarget();
        RotateAtTarget();
        FiringHandeler();
    }

    void LocateTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, TowerRange);//When it overlaps

        float ClosestDistance = Mathf.Infinity; //How far it can shoot
        Transform ClosestEnemy = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy")) //Is it tagged as an enemy
            {
                float Dist = Vector3.Distance(transform.position, hit.transform.position);
                if (Dist < ClosestDistance) // Is it in distance
                {
                    ClosestDistance = Dist;
                    ClosestEnemy = hit.transform;
                }
            }
        }
        CurrentlyTargeted = ClosestEnemy; // Could be changed to add a variety of targeting options
    }

    void RotateAtTarget()
    {
        if (CurrentlyTargeted == null) return; //Is there something to shoot at

        Vector3 Direction = CurrentlyTargeted.position - TowerRotate.position;
        Quaternion Rotation = Quaternion.LookRotation(Direction);
        TowerRotate.rotation = Quaternion.Lerp( //The math for the roatation of the towers weapon
            TowerRotate.rotation, Rotation, Time.deltaTime * TowerRotateSpeed );

    }

    void FiringHandeler()
    {
        if (CurrentlyTargeted == null) return;

        Cooldown -= Time.deltaTime;
        if (Cooldown <= 0f) //Is it ready to fire
        {
            //TESTING PURPSOSES ONLY
            //Debug.Log("Would Fire at" + CurrentlyTargeted.name);

            if (CurrentlyTargeted != null && shooter != null)
            {

                shooter.Firing(CurrentlyTargeted);
            }

            Cooldown = 1f / FireRate;//How long till next shot is allowed
        }
    }
}

// Helped by Copilot to provide the concept as to how to make this code