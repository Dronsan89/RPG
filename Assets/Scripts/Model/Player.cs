using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MoveController move;
    //[SerializeField] private GameObject playerView;
    //[SerializeField] private Health health;
    [SerializeField] private RangeWeapon rangeWeapon;
    //[SerializeField] private MeleeWeapon meleeWeapon;

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
    private LookAtCamera camer;
    private Quaternion offserRotateShootDirection;

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

    private void Update()
    {

    }
}
