using UnityEngine;

public class TankController
{
    private TankModel model;
    private TankView view;

    public bool enabled { get; internal set; }

    public TankController(TankModel model, TankView view)
    {
        this.model = model;
        this.view = view;

        view.SetController(this);
        //view.ApplyColor(model.color);
    }

    public void Move(float input)
    {
        Vector3 move = view.transform.forward * input * model.movementSpeed;
        view.ApplyMovement(move);
    }

    public void Rotate(float input)
    {
        float turn = input * model.rotationSpeed;
        view.ApplyRotation(turn);
    }

    public TankModel GetModel()
    {
        return model;
    }
}