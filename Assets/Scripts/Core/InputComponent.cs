using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 GetMove()
    {
        return new Vector3(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"), 0f);
    }
    public bool GetJump()
    {
        return Input.GetButtonDown("Jump");
    }
    public bool GetFire()
    {
        return Input.GetButtonDown("Fire1");
    }
    public bool GetClick()
    {
        return Input.GetButtonDown("Fire2");
    }
}
