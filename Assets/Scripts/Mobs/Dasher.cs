using Unity.VisualScripting;
using UnityEngine;
using Player;
using Environment;

namespace EnemyMob
{
    public class Dasher : MonoBehaviour
    {
        [SerializeField] private float alignSpeed = 3f; 
        [SerializeField] private float dashSpeed = 10f;  
        [SerializeField] private float alignDuration = 4f;
        [SerializeField] private float health = 30;  
        [SerializeField] private float damage = 2;  
        [SerializeField] private int difficulty = 1; //multiplier for HP/Damage


        private float timer = 0f;
        private bool isDashing = false;
        private Transform player; 

        private void Awake()
        {
            if (player == null)
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                {
                    player = playerObj.transform;
                }
                else
                {
                    Debug.LogError("Player not found in the scene. Please ensure the player has the tag 'Player'.");
                }
            }

            difficultyScale();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (!isDashing)
            {
                Vector3 pos = transform.position;
                pos.y = Mathf.MoveTowards(pos.y, player.position.y, alignSpeed * Time.deltaTime);
                transform.position = pos;

                if (timer >= alignDuration)
                {
                    isDashing = true;

                    float direction = Mathf.Sign(player.position.x - transform.position.x);
                    dashSpeed = Mathf.Abs(dashSpeed) * direction;
                }
            }
            else
            {
                transform.position += new Vector3(dashSpeed * Time.deltaTime, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Triggered Collision");
            if (collision.gameObject.name.Contains("Bullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    health -= bullet.GetDamage();
                    Debug.Log("enemy took damage: " + bullet.GetDamage());
                    if (health <= 0)
                    {
                        Debug.Log("enemy died");
                        Destroy(gameObject);
                    }
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Triggered Player");
                Health drone = collision.gameObject.GetComponent<Health>();
                drone.TakeDamage(damage);
            }
        }

        private void difficultyScale()
        {
            health *= difficulty;
            damage *= difficulty;

            float dampening = Mathf.Pow(difficulty, 0.5f); // square root of difficulty

            alignSpeed *= dampening;
        }
    }
}
