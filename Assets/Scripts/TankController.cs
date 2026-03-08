using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;

    private Rigidbody rb;

    public TankController(TankModel tank_model,TankView tank_view)
    {
        tankModel = tank_model;
        tankView = GameObject.Instantiate<TankView>(tank_view);
        rb = tankView.GetRigidbody();

        tankModel.SetTankController(this);
        tankView.SetTankController(this);

        tankView.ChangeColor(tankModel.color);
    }

    public void Move(float movement,float movementSpeed)
    {
        Vector3 move = tankView.transform.forward * movement * movementSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + move);
    }

    public void Rotate(float rotate,float rotateSpeed)
    {
        float turn = rotate * rotateSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);

        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public TankModel GetTankModel()
    {
        return tankModel;
    }
    public TankView GetTankView()
    {
        return tankView;
    }
}
