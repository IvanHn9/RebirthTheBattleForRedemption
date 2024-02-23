using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameObject bossUI;
    private Slider bossSlider;

    public bool isInBossStage;
    private SkillManager skills;

    //TODO: skill UI and soul info
    [SerializeField] private Image dashImage;
    [SerializeField] public Image arrowImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private Image strikeImage;
    [SerializeField] private Image healPotionImage;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        bossSlider = bossUI.GetComponentInChildren<Slider>();
        bossUI.SetActive(false);
    }
    void Start()
    {
        UpdateHealthUI();
        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthUI;

        UpdateBossHealthUI();
        if(enemyStats != null)
        {
            enemyStats.onHealthChanged += UpdateBossHealthUI;            
        }
        skills = SkillManager.instance;
        isInBossStage = false;
    }

    // Update is called once per frame
    void Update()
    {
        //----SKILL COOLDOWN ----

        // Dash
        if(skills.dash.dashUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SetCooldownOf(dashImage);
        }
        CheckCooldownOf(dashImage, skills.dash.cooldown);
        //---shooting arrow
        CheckCooldownOf(arrowImage, skills.shootArrow.cooldown);
        arrowText.text = skills.shootArrow.amountOfArrowLeft.ToString();

        //--parry--
        if (skills.parry.parryUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && skills.parry.CanUseSkill())
                SetCooldownOf(parryImage);
        }
        CheckCooldownOf(parryImage, skills.parry.cooldown );
        
        // thunderstrike
        if (skills.thunderStrike.unlockedThunderStrike)
        {
            if (Input.GetKeyDown(KeyCode.E) && skills.thunderStrike.CanUseSkill())
                SetCooldownOf(strikeImage);
        }
        CheckCooldownOf(strikeImage, skills.thunderStrike.cooldown);

        // --- heal potion;
        if (Input.GetKeyDown(KeyCode.F) && Inventory.instance.canUseFlask)
            SetCooldownOf(healPotionImage);
        CheckCooldownOf(healPotionImage, Inventory.instance.flaskCooldown);
        //---Boss healthbar
        if (isInBossStage)
        {
            bossUI.SetActive(true);
        }
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }
    private void UpdateBossHealthUI()
    {
        bossSlider.maxValue = enemyStats.GetMaxHealthValue();
        bossSlider.value = enemyStats.currentHealth;
    }
    public void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }
}
