using UnityEngine;

namespace Enemies.EnemyTypes
{   
    public class WaterBullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}