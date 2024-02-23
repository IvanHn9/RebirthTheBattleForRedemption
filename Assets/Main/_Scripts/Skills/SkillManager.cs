using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Dash_Skill dash { get; private set; }
    public ShootArrow_Skill shootArrow { get; private set; }
    public Parry_Skill parry { get; private set; }
    public Clone_Skill clone { get; private set; }
    public ThunderStrikeSkill thunderStrike { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
        shootArrow = GetComponent<ShootArrow_Skill>();
        parry = GetComponent<Parry_Skill>();
        clone = GetComponent<Clone_Skill>();
        thunderStrike = GetComponent<ThunderStrikeSkill>();
    }
}
