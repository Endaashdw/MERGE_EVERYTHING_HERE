using System;
using UnityEditor.UI;
using UnityEngine;
using Player;

namespace Environment
{
    public class EnemyBullet : Bullet
    {
		private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.gameObject.CompareTag("EnemyMob"))
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Triggered Player");
                    Health drone = collision.gameObject.GetComponent<Health>();
                    drone.TakeDamage(damage);
                }
            }
		}
    }
}