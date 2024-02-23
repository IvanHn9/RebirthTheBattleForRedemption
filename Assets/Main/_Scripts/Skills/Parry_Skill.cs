using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{

    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    [SerializeField] private GameObject parryUI;
    public bool parryUnlocked { get; private set; }

    [Header("Parry restore")]
    [SerializeField] private UI_SkillTreeSlot restoreUnlockButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPerentage;
    public bool restoreUnlocked { get; private set; }
    public bool canBeHeal;
    [Header("Parry with mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked { get; private set; }

    public override void UseSkill()
    {
        base.UseSkill();


        if (restoreUnlocked)
        {
            if(canBeHeal) 
            {
                int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthPerentage);
                player.stats.IncreaseHealthBy(restoreAmount);
                canBeHeal = false;
            }   
        }
    }

    protected override void Start()
    {
        base.Start();

        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);
    }

    protected override void CheckUnlock()
    {
        
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();
        
    }
    private void UnlockParry()
    {
        if (parryUnlockButton.unlocked)
        {
            
            parryUnlocked = true;
            parryUI.SetActive(true);
        }
        else
        {
            parryUI.SetActive(false);
        }
            

    }

    private void UnlockParryRestore()
    {
        if (restoreUnlockButton.unlocked)
            restoreUnlocked = true;
    }

    private void UnlockParryWithMirage()
    {
        if (parryWithMirageUnlockButton.unlocked)
            parryWithMirageUnlocked = true;
    }

    public void MakeMirageOnParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
    }

}