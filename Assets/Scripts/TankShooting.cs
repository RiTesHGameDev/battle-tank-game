using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int player_number = 1;
    public Rigidbody shell;
    public Transform fireTransform;
    public Slider aimSlider;
    public AudioSource shootingAudio;
    public AudioClip chargingfClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;

    private string fireButton;
    private float currentLauchForce;
    private float chargeSpeed;
    private bool fired;

    private void OnEnable()
    {
        currentLauchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }

    private void Start()
    {
        fireButton = "Fire" + player_number;

        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }

    private void Update()
    {
        aimSlider.value = minLaunchForce;

        if(currentLauchForce >= maxLaunchForce && !fired)
        {
            currentLauchForce = maxLaunchForce;
            Fire();

        }
        else if(Input.GetButtonDown(fireButton))
        {
            fired = false;
            currentLauchForce = minLaunchForce;

            shootingAudio.clip = chargingfClip;
            shootingAudio.Play();
        }
        else if(Input.GetButton(fireButton) && !fired)
        {
            currentLauchForce += chargeSpeed * Time.deltaTime;
            aimSlider.value = currentLauchForce;
        }
        else if(Input.GetButtonUp(fireButton) && !fired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        fired = true;

        Rigidbody shellIntance = Instantiate(shell,fireTransform.position,fireTransform.rotation) as Rigidbody;

        shellIntance.velocity = currentLauchForce * fireTransform.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLauchForce = minLaunchForce;
    }
}
