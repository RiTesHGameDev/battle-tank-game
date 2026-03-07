using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeed;
    public TankModel(float _movement,float _rotation)
    {
        movementSpeed = _movement;
        rotationSpeed = _rotation;
    }

    public void SetTankController(TankController tank_Controller)
    {
        tankController = tank_Controller;
    }

}
