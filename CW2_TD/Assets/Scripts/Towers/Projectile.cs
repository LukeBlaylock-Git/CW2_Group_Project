using JetBrains.Annotations;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float ProjectileSpeed = 10f; //What are the base stats
    public int ProjectileDamage = 5;
    public float TowerRange = 5f;


    public Transform CurrentlyTargeted;

    public void GettingTarget(Transform enemy) //Get the target to fire at
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

    private void Update()
    {
        if (CurrentlyTargeted == null) { // If there isnt a target
            //Destroy(gameObject);
            return; 
        }
        
        Vector3 Direction = (CurrentlyTargeted.position - transform.position).normalized; //Move to target
        transform.position += Direction * ProjectileSpeed * Time.deltaTime;

    }

    public void OnTriggerEnter(Collider other) //When they hit each other
    {
        if (other.transform == CurrentlyTargeted)
        {
            // Get their health and what to do if they are not already dead
            Enemy HP = other.GetComponent<Enemy>();
            if (HP != null)
            {
                HP.TakeDamage(ProjectileDamage);
            }

            Destroy(gameObject) ;
        }
    }
}
