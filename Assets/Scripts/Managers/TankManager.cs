using UnityEngine;
using System;

[Serializable]
public class TankManager
{
    public float movementSpeed;
    public float rotationSpeed;

    public Color playerColor;                             // The color this tank will be tinted.
    public Transform spawnPoint;                          // The position and direction the tank will have when
    public int playerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public string coloredPlayerText;    // A string that
    [HideInInspector] public GameObject instance;
    [HideInInspector] public int wins;

    private TankView tankView;              // Reference to tank's movement script.
    private TankShooting tankShooting;                // Reference to tank's shooting script.
    private GameObject canvasGameObject;
    public void Setup()
    {
        tankView = instance.GetComponent<TankView>();
        tankShooting = instance.GetComponent<TankShooting>();
        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        CreateTank();

        tankShooting.player_number = playerNumber;
        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public void CreateTank()
    {
        if (tankView == null)
        {             
            Debug.LogError($"TankView component missing on Tank instance!");
            return;
        }
        // Create Model
        TankModel model = new TankModel(
            playerNumber,
            movementSpeed,
            rotationSpeed
        );
        // Create Controller
        TankController controller = new TankController(model, tankView);

    }
    public void DisableControl()
    {
        tankView.enabled = false;
        tankShooting.enabled = false;
        canvasGameObject.SetActive(false);
    }
    public void EnableControl()
    {
        tankView.enabled = true;
        tankShooting.enabled = true;
        canvasGameObject.SetActive(true);
    }
    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;
        instance.SetActive(false);
        instance.SetActive(true);
    }
}
