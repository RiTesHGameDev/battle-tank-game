using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController controller;
    private Rigidbody rb;

    private float movement;
    private float rotation;

    [Header("Audio")]
    public AudioSource movementAudio;
    public AudioClip engineIdling;
    public AudioClip engineDriving;
    public float pitchRange = 0.2f;
    private float originalPitch;
    private bool isMovingAudio;

    [Header("Visual")]
    public MeshRenderer[] meshParts;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError("Rigidbody missing on Tank!");

        originalPitch = movementAudio.pitch;
    }

    private void Update()
    {
        if (controller == null) return;

        movement = Input.GetAxis("Vertical"+controller.GetModel().playerNumber);
        rotation = Input.GetAxis("Horizontal"+controller.GetModel().playerNumber);

        HandleAudio();
    }

    private void FixedUpdate()
    {
        if (controller == null) return;

        float adjustedRotation = rotation;

        if (movement < 0)
            adjustedRotation = -rotation;

        controller.Move(movement);
        controller.Rotate(adjustedRotation);
    }

    public void ApplyMovement(Vector3 move)
    {
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    public void ApplyRotation(float turn)
    {
        Quaternion turnRotation = Quaternion.Euler(0f, turn * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    // --- Audio ---
    private void HandleAudio()
    {
        bool isMoving = Math.Abs(movement) >= 0.1f || Math.Abs(rotation) >= 0.1f;

        if (isMoving && !isMovingAudio)
        {
            PlayAudio(engineDriving);
            isMovingAudio = true;
        }
        else if (!isMoving && isMovingAudio)
        {
            PlayAudio(engineIdling);
            isMovingAudio = false;
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        movementAudio.clip = clip;
        movementAudio.pitch = UnityEngine.Random.Range(
            originalPitch - pitchRange,
            originalPitch + pitchRange
        );
        movementAudio.Play();
    }

    // --- Setup ---
    public void SetController(TankController ctrl)
    {
        controller = ctrl;
    }

    public void ApplyColor(Material mat)
    {
        foreach (MeshRenderer part in meshParts)
        {
            part.sharedMaterial = mat;
        }
    }
}
