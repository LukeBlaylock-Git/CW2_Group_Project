using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    public GameObject PrefabUsed;//What projectile is used
    public Transform FiringPoint;//Where it shoots from

    

    public void Firing(Transform Enemy)
    {
        if (FiringPoint != null)
        {
            GameObject Projectile = Instantiate(PrefabUsed, FiringPoint.position, FiringPoint.rotation);
            Projectile.GetComponent<Projectile>().GettingTarget(Enemy);
        }
    }
}
