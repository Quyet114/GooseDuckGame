using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatlh : MonoBehaviour
{
    PlayerScript player;
    public int minDamage;
    public int maxDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponentInParent<PlayerScript>();
            InvokeRepeating("HeatlhPlayer", 0, 1f);
            Destroy(gameObject);
        }
    }
    void HeatlhPlayer()
    {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        player.takeHeatlh(damage);
    }
}
