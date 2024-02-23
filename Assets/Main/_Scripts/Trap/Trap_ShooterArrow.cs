using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_ShooterArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int damage;
    [SerializeField] Direction direction;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float idleTime;
    [SerializeField] Transform shootTransform;

    private float timer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            animator.SetBool("Shoot", true);
        }
    }
    public void AnimatinTrigger()
    {
        
            timer = idleTime;
            GameObject arrow = Instantiate(arrowPrefab, shootTransform.position, Quaternion.identity);
            Trap_Arrow arrowScript = arrow.GetComponent<Trap_Arrow>();
            arrowScript.SetUpArrow(direction, arrowSpeed, damage);
        
    }
    public void AnimationFinishTrigger()
    {
        animator.SetBool("Shoot", false);
    }
}
