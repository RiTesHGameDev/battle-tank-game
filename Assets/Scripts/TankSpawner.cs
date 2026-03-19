using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Tank
    {
        public float movementSpeed;
        public float rotationSpeed;
        public TankTypes tankType;
        public Material color;
    }
    public CameraControl cameraControl;
    private List<Transform> spawnedTanks = new List<Transform>();
    public List<Tank> tankList;
    public TankView tankView;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateTank(TankTypes tankType)
    {
        TankModel tankModel = null;

        if (tankType == TankTypes.BlueTank)
        {
            tankModel = new TankModel(tankList[0].movementSpeed,
                                      tankList[0].rotationSpeed,
                                      tankList[0].tankType,
                                      tankList[0].color);
        }
        else if (tankType == TankTypes.GreenTank)
        {
            tankModel = new TankModel(tankList[1].movementSpeed,
                                      tankList[1].rotationSpeed,
                                      tankList[1].tankType,
                                      tankList[1].color);
        }
        else if (tankType == TankTypes.RedTank)
        {
            tankModel = new TankModel(tankList[2].movementSpeed,
                                      tankList[2].rotationSpeed,
                                      tankList[2].tankType,
                                      tankList[2].color);
        }

        TankController tankController = new TankController(tankModel, tankView);

        spawnedTanks.Add(tankController.GetTankView().transform);

        cameraControl.targets = spawnedTanks.ToArray();
    }
}
