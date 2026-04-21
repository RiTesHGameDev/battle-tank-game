using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class TankSpawner : MonoBehaviour
//{
//    [System.Serializable]
//    public class Tank
//    {
//        public float movementSpeed;
//        public float rotationSpeed;
//        public TankTypes tankType;
//        public Material color;
//    }
//    public CameraControl cameraControl;
//    private List<Transform> spawnedTanks = new List<Transform>();
//    public List<Tank> tankList;
//    public TankView tankView;
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    public void CreateTank(TankTypes tankType)
//    {
//        TankModel tankModel = null;

//        if (tankType == TankTypes.BlueTank)
//        {
//            tankModel = new TankModel(tankList[0].movementSpeed,
//                                      tankList[0].rotationSpeed,
//                                      tankList[0].tankType,
//                                      tankList[0].color);
//        }
//        else if (tankType == TankTypes.GreenTank)
//        {
//            tankModel = new TankModel(tankList[1].movementSpeed,
//                                      tankList[1].rotationSpeed,
//                                      tankList[1].tankType,
//                                      tankList[1].color);
//        }
//        else if (tankType == TankTypes.RedTank)
//        {
//            tankModel = new TankModel(tankList[2].movementSpeed,
//                                      tankList[2].rotationSpeed,
//                                      tankList[2].tankType,
//                                      tankList[2].color);
//        }

//        TankController tankController = new TankController(tankModel, tankView);

//        spawnedTanks.Add(tankController.GetTankView().transform);

//        cameraControl.targets = spawnedTanks.ToArray();
//    }
//}

public class TankSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TankData
    {
        public TankTypes tankType;
        public float movementSpeed;
        public float rotationSpeed;
        public Material color;
    }

    [Header("Setup")]
    public CameraControl cameraControl;
    public TankView tankPrefab;
    public List<TankData> tankList;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private List<Transform> spawnedTanks = new List<Transform>();

    public void CreateTank(TankTypes tankType)
    {
        TankData data = GetTankData(tankType);

        if (data == null)
        {
            Debug.LogError($"No TankData found for {tankType}");
            return;
        }

        // Create Model
        //TankModel model = new TankModel(
        //    data.movementSpeed,
        //    data.rotationSpeed,
        //    data.color
        //);

        // Get spawn position
        Transform spawnPoint = GetSpawnPoint();

        TankView view = Instantiate(
            tankPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Create Controller
        //TankController controller = new TankController(model, view);

        // Track for camera
        spawnedTanks.Add(view.transform);
        cameraControl.targets = spawnedTanks.ToArray();
    }
    private TankData GetTankData(TankTypes type)
    {
        foreach (var tank in tankList)
        {
            if (tank.tankType == type)
                return tank;
        }
        return null;
    }
    private int spawnIndex = 0;

    private Transform GetSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return transform;
        }

        Transform point = spawnPoints[spawnIndex];
        spawnIndex = (spawnIndex + 1) % spawnPoints.Length;

        return point;
    }
}