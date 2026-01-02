using UnityEngine;

[CreateAssetMenu(menuName = "TD/Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("Attack")]
    public float Range = 5f;
    public float FireRate = 2f;

    [Header("Projectile")]
    public float ProjectileSpeed = 10f;
    public int ProjectileDamage = 5;
}
