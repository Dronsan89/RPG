using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MoveController move;
    //[SerializeField] GameObject playerView;
    //[SerializeField] Vector3 fireFrom;
    //[SerializeField] Health health;
    [SerializeField] RangeWeapon rangeWeapon;
    //[SerializeField] MeleeWeapon meleeWeapon;

    public Vector3 Aim => currentInput.Aim;
    public Quaternion Rotation => move.Rotation;
    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public Transform Transform => transform;
    //public MeleeWeapon MeleeWeapon { get; set; }
    public RangeWeapon RangeWeapon { get; set; }
    //public Vector3 ShootPosition => fireFrom;
    public Quaternion ShootDirection => camer.transform.rotation*offserRotateShootDirection;
    //public Health Health => health;

    public LookAtCamera Camer => camer;

    private InputBase currentInput;
    LookAtCamera camer;
    Quaternion offserRotateShootDirection;

    public void Construct(InputBase input, LookAtCamera cam)
    {
        Loading();
        currentInput = input;
        move.Construct(cam);
        rangeWeapon.Construct(this);
        camer = cam;
        offserRotateShootDirection = Quaternion.Euler(- 5, -2, 0);
    }

    private void Loading()
    {

    }

    void Update()
    {
        //transform.position += move.MoveUpdate(currentInput);

        //playerView.transform.rotation = move.RotateUpdate(currentInput);
    }


}
