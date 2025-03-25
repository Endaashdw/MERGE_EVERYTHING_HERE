using UnityEngine;
using Player;
using Environment;

namespace Mobs
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float alignSpeed = 3f; 
        [SerializeField] private float firingSpeed = 3f; 
        [SerializeField] private float health = 20; 
        [SerializeField] private int difficulty = 1; //multiplier for HP/Damage
        [SerializeField] private GameObject bulletPrefab; 
        private Transform shootPoint;

        private float timer = 0f;
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

            if (shootPoint == null)
            {
                shootPoint = transform.Find("Firepoint");
                if (shootPoint == null)
                {
                    Debug.LogError("Firepoint not found! Make sure it is named correctly and is a child of Shooter.");
                }
            }

            difficultyScale();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            // Align Y position with the player
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, player.position.y, alignSpeed * Time.deltaTime);
            transform.position = pos;

            // Fire bullet when the timer exceeds the firing speed
            if (timer >= firingSpeed)
            {
                FireBullet();
                timer = 0f; // Reset the timer after firing
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Triggered Collision");
            if (collision.gameObject.name.Contains("Bullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                health -= bullet.GetDamage();
                Debug.Log("enemy took damage: " + bullet.GetDamage());
                if (health <= 0)
                {
                    Debug.Log("enemy died");
                    Destroy(gameObject); 
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Triggered Player");
                DroneMovement drone = collision.gameObject.GetComponent<DroneMovement>();
                drone?.KillPlayer();
            }
        }

        private void FireBullet()
        {
            print("Fire!");
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }

        private void difficultyScale()
        {
            health *= difficulty;

            float dampening = Mathf.Pow(difficulty, 0.5f); // square root of difficulty

            alignSpeed *= dampening;
            firingSpeed /= dampening;
        }
    }

}
