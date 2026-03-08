using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;
    private float originalPitch;
    private float movement;
    private float rotation;

    public AudioSource movementAudio;
    public AudioClip engineIdling;
    public AudioClip engineDriving;
    public float pitchRange = 0.2f;

    public Rigidbody rb;
    public MeshRenderer[] childs; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        originalPitch = movementAudio.pitch;
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
    }
    void Update()
    {

        if (movement != 0)
            tankController.Move(movement, tankController.GetTankModel().movementSpeed);

        if (rotation != 0)
            tankController.Rotate(rotation, tankController.GetTankModel().rotationSpeed);

        EngineAudio();

    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
    }

    private void EngineAudio()
    {
        if(Math.Abs(movement) < 0.1f && Math.Abs(rotation) < 0.1f)
        {
            if(movementAudio.clip == engineDriving)
            {
                movementAudio.clip = engineIdling;
                movementAudio.pitch = UnityEngine.Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            if (movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.pitch = UnityEngine.Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
    }

    public void SetTankController(TankController tank_Controller)
    {
        tankController = tank_Controller;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void ChangeColor(Material color)
    {
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].material = color;
        }
    }
}
