using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private BoxCollider2D cd;
    private void OnValidate()
    {
        gameObject.name = "Item - " + itemData.itemName;
    }
    private void Start()
    {
        cd = GetComponent<BoxCollider2D>();
        cd.enabled = false;
    }
    public void SetupItem(Vector2 _velocity)
    {
        rb.velocity = _velocity;
        StartCoroutine(Delay());
    }
    public void PickupItem()
    {
        AudioManager.instance.PlaySFX(18, transform);
        Inventory.instance.AddItem(itemData);
        Inventory.instance.EquipItem(itemData);
        PlayerManager.instance.player.fx.PickupFx(transform);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStats>().isDead)
                return;
            PickupItem();
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        cd.enabled = true;
    }
}
