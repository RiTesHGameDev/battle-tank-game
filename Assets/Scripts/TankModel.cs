using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeed;
    public TankTypes TankTypes;
    public Material color;
    public TankModel(float _movement,float _rotation, TankTypes _tank, Material _color)
    {
        movementSpeed = _movement;
        rotationSpeed = _rotation;
        TankTypes = _tank;
        this.color = _color;
    }

    public void SetTankController(TankController tank_Controller)
    {
        tankController = tank_Controller;
    }

}
