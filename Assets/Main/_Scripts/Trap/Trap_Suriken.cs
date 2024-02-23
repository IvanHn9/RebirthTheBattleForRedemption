using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Suriken : MonoBehaviour
{
    [SerializeField] int Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(Damage);
        }
    }
}
