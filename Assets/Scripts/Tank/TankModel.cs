using UnityEngine;

[System.Serializable]
public class TankModel
{
    public int playerNumber;
    public float movementSpeed;
    public float rotationSpeed;

    public TankModel(int playerNumber, float move, float rotate)
    {
        this.playerNumber = playerNumber;
        movementSpeed = move;
        rotationSpeed = rotate;

    }
}