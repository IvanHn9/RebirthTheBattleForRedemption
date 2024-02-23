using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour,ISaveManager
{
    private Animator anim;
    public string id;
    public bool isPickedUp;
    private void Awake()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        if (isPickedUp)
            gameObject.SetActive(false);

    }
    [ContextMenu("Generate coin id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("pickup");
            Destroy(gameObject, 0.25f);
            PlayerManager.instance.currency++;
            isPickedUp = true;
        }
    }

    public void LoadData(GameData _data)
    {
        if (_data.coin.TryGetValue( id, out bool value))
        {
            isPickedUp = value;
        }
    }

    public void SaveData(ref GameData _data)
    {

        if (_data.coin.TryGetValue(id, out bool value))
        {
            _data.coin.Remove(id);
            _data.coin.Add(id,isPickedUp);
        }
        else
            _data.coin.Add(id, isPickedUp);
    }
}
