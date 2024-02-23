using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WizardBulletController : MonoBehaviour
{
    private float speed;
    private int moveDir;
    private CharacterStats stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveDir,0,0) * speed * Time.deltaTime ;
    }
    public void SetUpBullet(int dir,float speed,CharacterStats stats)
    {
        this.speed = speed;
        moveDir = dir;
        this.stats = stats;
        Destroy(gameObject,7f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            stats.DoDamage(collision.gameObject.GetComponent<CharacterStats>());
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
