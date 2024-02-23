using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderStrikeSkill : Skill
{
    [SerializeField] private GameObject thunnderPrefab;
    public bool unlockedThunderStrike;
    [SerializeField] private UI_SkillTreeSlot thunderSkillButton;
    [SerializeField] private GameObject strikeUI;
    [SerializeField] private int numberOfStrike;
    [SerializeField] private Vector3 offSet;
    private Vector3 pos;

    protected override void Start()
    {
        base.Start();

        thunderSkillButton.GetComponent<Button>().onClick.AddListener(UnlockThunder);
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockThunder();
    }
    private void UnlockThunder()
    {
        if (thunderSkillButton.unlocked)
        {
            
            unlockedThunderStrike = true;
            strikeUI.SetActive(true);
        }
        else
            strikeUI.SetActive(false);

    }
    public void CastThunderStrike()
    {
        if (player.facingDir == 1)
        {
           pos = new Vector3(player.transform.position.x + offSet.x, player.transform.position.y + offSet.y, 0);
        }
        else
        {
           pos = new Vector3((player.transform.position.x - offSet.x), player.transform.position.y + offSet.y, 0);
        }
        Instantiate(thunnderPrefab, pos, transform.rotation);
        cooldownTimer = cooldown;
    }
}
