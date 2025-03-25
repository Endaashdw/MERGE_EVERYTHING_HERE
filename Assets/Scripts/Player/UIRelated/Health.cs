using UnityEngine;
using Player;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;}
    private codaScript playerScript;
    public bool dead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = startingHealth;
        playerScript = GetComponent<codaScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Testing. Press E to deal 1 damage.
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float _damage)
    {
        //ensuring HP does not go below 0
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        print("Lost " + _damage + " hearts"); //debug text
        if (currentHealth > 0)
        {
            print("Player still alive");
        } else
        {
            if (!dead)
            {
                //playerScript.KillPlayer();
                print("You died");
                dead = true;
            }
        }
    }

    //Method to heal player to max. Currently used to fully heal player
    public void FullHeal()
    {
        currentHealth = startingHealth;
    }

    
}
