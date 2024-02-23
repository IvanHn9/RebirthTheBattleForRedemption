using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private BoxCollider2D cd;
    private Animator animator;
    // Start is called before the first frame update

    void Start()
    {
         cd = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        cd.enabled = false;
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //if(Input.GetKeyDown(KeyCode.E))
                GameManager.instance.GoToNextScene();
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        cd.enabled = true;
        animator.SetBool("Idle", true);
    }
}
