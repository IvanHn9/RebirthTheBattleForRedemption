
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler ,ISaveManager
{
    private UI ui;
    private Image skillImage;

    [SerializeField] private int skillCost;
    [SerializeField] private string skillName;
    [SerializeField] private string skillHotKey;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;


    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;

    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
        
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;
        if (unlocked)
            skillImage.color = Color.white;
    }

    public void UnlockSkillSlot()
    {
        if (unlocked)
        {
            AudioManager.instance.PlaySFX(7, null);
            return;
        }
        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                AudioManager.instance.PlaySFX(7, null);
                return;
            }
        }


        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                AudioManager.instance.PlaySFX(7, null);
                return;
            }
        }
        if (PlayerManager.instance.HaveEnoughMoney(skillCost) == false)
        {
            AudioManager.instance.PlaySFX(7, null);
            return;
        }
        unlocked = true;
        skillImage.color = Color.white;
        AudioManager.instance.PlaySFX(23,null);
    }
    //public void CreatePopUpText(string _text,Color color)
    //{
    //    float randomX = Random.Range(-1, 1);
    //    float randomY = Random.Range(1.5f, 3);
        
    //    Vector3 positionOffset = FindAnyObjectByType<UI>().GetComponent<Transform>().position + new Vector3(randomX, randomY, 0);
        // --- Popup text
        //GameObject newText = Instantiate(popupTextFX,positionOffset,Quaternion.identity);
        
        
        //newText.GetComponent<TextMeshPro>().text = _text;
        //newText.GetComponent<TextMeshPro>().color = color;
        //newText.GetComponent<TextMeshPro>().fontSize = 18;
   // }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDescription,skillName,skillCost,skillHotKey);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
            _data.skillTree.Add(skillName, unlocked);
    }
}
