using UnityEngine;

public class InputDesktop : InputBase
{
    void Update()
    {
        #region Desktop
        Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Aim = Input.mousePosition;
        //Rotation = 
        #endregion
    }
}
