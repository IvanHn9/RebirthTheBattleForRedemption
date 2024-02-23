using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class Clone_Skill : Skill
{


    [Header("Clone info")]
    [SerializeField] private float attackMultiplier;
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    //[Space]

    //[Header("Clone attack")]
    //[SerializeField] private UI_SkillTreeSlot cloneAttackUnlockButton;
    //[SerializeField] private float cloneAttackMultiplier;
    //[SerializeField] private bool canAttack;

    public bool canApplyOnHitEffect { get; private set; }

    [Header("Multiple clone")]
    [SerializeField] private UI_SkillTreeSlot multipleUnlockButton;
    [SerializeField] private float multiCloneAttackMultiplier;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    


    protected override void Start()
    {
        base.Start();

        //cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
        multipleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultiClone);
    }

    #region Unlock region
    protected override void CheckUnlock()
    {
        //UnlockCloneAttack();
        UnlockMultiClone();
    }

    //private void UnlockCloneAttack()
    //{
    //    if (cloneAttackUnlockButton.unlocked)
    //    {
    //        canAttack = true;
    //        attackMultiplier = cloneAttackMultiplier;
    //    }
    //}
    private void UnlockMultiClone()
    {
        if (multipleUnlockButton.unlocked)
        {
            canDuplicateClone = true;
            attackMultiplier = multiCloneAttackMultiplier;
        }
    }



    #endregion


    public void CreateClone(Transform _clonePosition,Vector3 _offset)
    {

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().
            SetupClone(_clonePosition, cloneDuration, true,_offset,FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate,player,attackMultiplier);
    }


    public void CreateCloneWithDelay(Transform _enemyTransform)
    {
        StartCoroutine(CloneDelayCoroutine(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }

    private IEnumerator CloneDelayCoroutine(Transform _trasnform,Vector3 _offset)
    {
        yield return new WaitForSeconds(.3f);
            CreateClone(_trasnform,_offset);
    }
}
