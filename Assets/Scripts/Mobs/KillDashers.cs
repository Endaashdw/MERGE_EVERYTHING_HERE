using UnityEngine;

public class KillEnemies : MonoBehaviour
{
    // This method is called when another collider enters this trigger collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by: " + collision.gameObject.name + " with tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("EnemyMob"))
        {
            Debug.Log("Killed object");
            Destroy(collision.gameObject);
        }
    }
}
