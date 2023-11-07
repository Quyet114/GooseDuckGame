using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlue : MonoBehaviour
{

    PlayerScript player;
    public int minDamage;
    public int maxDamage;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerScript>();
            InvokeRepeating("DamagePlayer", 0, 0.1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            InvokeRepeating("DamagePlayer", 0, 0.1f);
        }
    }
    void DamagePlayer()
    {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);

    }

}

