using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public TankModel()
    {
    }

    public void SetTankController(TankController tank_Controller)
    {
        tankController = tank_Controller;
    }

}
