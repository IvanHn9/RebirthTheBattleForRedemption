using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootArrow_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float arrowGravity;
    [SerializeField] private int amountOfArrow;
    [SerializeField] private float freezeTimeDuration;

    public int amountOfArrowLeft;
    public bool bowUnlocked { get; private set; }
    [SerializeField] private UI_SkillTreeSlot bowUnlockButton;
    [SerializeField] private GameObject arrowUI;
    [Header("peirce info")]
    [SerializeField] private UI_SkillTreeSlot pierceUnlockButton;
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;
    public bool pierceArrowUnlocked;

    private Vector2 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        SetUpGravity();
        GenereateDots();

        amountOfArrowLeft = amountOfArrow;
        bowUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBow);
        pierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockPiercingArrow);
    }

    public override void Update()
    {
        base.Update();
        if (player.stateMachine.currentState == player.aimState)
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
            }
            
        }
        if(CanUseSkill() == true)
        {
            if (amountOfArrowLeft == amountOfArrow) return;
            amountOfArrowLeft++;
        }
    }
    private void SetUpGravity()
    {
        
    }
    public void CreateArrow()
    {
        GameObject newArrow = Instantiate(arrowPrefab, player.transform.Find("ShootPos").position, transform.rotation);
        Arrow_Controller newArrowScript = newArrow.GetComponent<Arrow_Controller>();
        newArrowScript.targetLayerName = "Enemy";
        if(pierceArrowUnlocked)
            newArrowScript.SetupPierce(pierceAmount);
        newArrowScript.SetupArrow(finalDir, arrowGravity, player.stats, freezeTimeDuration);
        DotsActive(false);
        amountOfArrowLeft--;
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockBow();
        UnlockPiercingArrow();
       
    }
    private void UnlockBow()
    {
        if (bowUnlockButton.unlocked)
        {
            bowUnlocked = true;
            arrowUI.SetActive(true);
        }
        else
            arrowUI.SetActive(false);
    }
    private void UnlockPiercingArrow()
    {
        if (pierceUnlockButton.unlocked)
            pierceArrowUnlocked = true;
    }
    //TODO: check unlock
    #region Aim region
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
    private void GenereateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.Find("ShootPos").position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.Find("ShootPos").position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * arrowGravity) * (t * t);

        return position;
    }
    #endregion
}
