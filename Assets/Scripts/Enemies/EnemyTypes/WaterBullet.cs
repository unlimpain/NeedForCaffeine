using PlayerScripts;
using UnityEngine;

namespace Enemies.EnemyTypes
{   
    public class WaterBullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                FindObjectOfType<Player>().TakeDamage(WaterBottle.AttackDamage);
            }
            Destroy(gameObject);
        }
    }
}