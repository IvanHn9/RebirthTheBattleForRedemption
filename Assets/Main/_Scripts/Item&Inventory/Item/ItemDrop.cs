using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDrop : MonoBehaviour,ISaveManager
{
    [SerializeField] private GameObject[] dropPrefabs;

    [SerializeField] private Sprite OpenSprite;
    public bool isDrop;
    public string id;

    [ContextMenu("Generate coin id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public void DropItem()
    {
        foreach(GameObject drop in dropPrefabs)
        {
            GameObject newDrop = Instantiate(drop, transform.position, Quaternion.identity);
            Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(5, 10));
            newDrop.GetComponent<ItemObject>().SetupItem(randomVelocity);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
                return;
            GetComponent<SpriteRenderer>().sprite = OpenSprite;
            if (isDrop) return;
            
            DropItem();
            isDrop = true;
        }
    }
    void ISaveManager.LoadData(GameData _data)
    {
        if (_data.chest.TryGetValue(id, out bool value))
        {
            isDrop = value;
        }
    }
    void ISaveManager.SaveData(ref GameData _data)
    {
        if (_data.chest.TryGetValue(id, out bool value))
        {
            _data.chest.Remove(id);
            _data.chest.Add(id, isDrop);
        }
        else
            _data.chest.Add(id, isDrop);
    }
}
