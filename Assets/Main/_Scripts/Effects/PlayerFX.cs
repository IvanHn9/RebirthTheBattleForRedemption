//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFX : EntityFx
{


    //[Header("Screen shake FX")]
    //[SerializeField] private float shakeMultiplier;
    //public Vector3 shakeSwordImpact;
    //public Vector3 shakeHighDamage;
    //private CinemachineImpulseSource screenShake;

    [Header("After image fx")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLooseRate;
    [SerializeField] private float afterImageCooldown;
    private float afterImageCooldownTimer;
    [Space]
    [SerializeField] private ParticleSystem dustFx;

    [Header("Eccho Fx")]
    [SerializeField] private GameObject ecchoImage;
    [SerializeField] private float timeBtwSpawns;
    [SerializeField] private float startTimeBtwSpawns;

    [Header("Healing Fx")]
    [SerializeField] private GameObject healingPrefab;
    [SerializeField] private GameObject pickupFxPrefab;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        //screenShake = GetComponent<CinemachineImpulseSource>();
        
    }

    private void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;
        if(player.dashState.isDashing)
        {
            SpawnEccho();
        }
    }
    //TODO: screen shake
    //public void ScreenShake(Vector3 _shakePower)
    //{
    //    screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
    //    screenShake.GenerateImpulse();
    //}


    public void CreateAfterImage()
    {
        if (afterImageCooldownTimer < 0)
        {
            afterImageCooldownTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLooseRate, spriteRenderer.sprite);
        }
    }


    //public void PlayDustFX()
    //{
    //    if (dustFx != null)
    //        dustFx.Play();
    //}
    public void SpawnEccho()
    {
       
        if (timeBtwSpawns <=0)
        {
            ecchoImage.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
            GameObject instance = Instantiate(ecchoImage,transform.position,transform.rotation);
            Destroy(instance,3);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns-= Time.deltaTime;
        }
    }
    public void HealingEffect()
    {
       GameObject heal = Instantiate(healingPrefab,transform.position,transform.rotation);
       Destroy(heal.gameObject, 0.35f);
    }
    public void PickupFx(Transform item)
    {
        GameObject pickupFx = Instantiate(pickupFxPrefab, item.position, transform.rotation);
        Destroy(pickupFx.gameObject, 0.35f);
    }
}
