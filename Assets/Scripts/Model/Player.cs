using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MoveController move;
    //[SerializeField] private Health health;
    [SerializeField] private RangeWeapon rangeWeapon;
    //[SerializeField] private MeleeWeapon meleeWeapon;
    [SerializeField] private Animator animator;

    public Vector3 Aim => currentInput.Aim;
    public Quaternion Rotation => move.Rotation;
    public Vector3 Position { get => transform.position; set => transform.position = value; }

    public Transform Transform => transform;
    //public MeleeWeapon MeleeWeapon { get; set; }
    public RangeWeapon RangeWeapon { get; set; }
    //public Vector3 ShootPosition => fireFrom;
    public Quaternion ShootDirection => camer.transform.rotation*offsetRotateShootDirection;
    //public Health Health => health;

    public LookAtCamera Camer => camer;

    private InputBase currentInput;
    private LookAtCamera camer;
    private Quaternion offsetRotateShootDirection;

    public void Construct(InputBase input, LookAtCamera cam)
    {
        Loading();
        currentInput = input;
        move.Construct(cam, this);
        rangeWeapon.Construct(this);
        camer = cam;
        offsetRotateShootDirection = Quaternion.Euler(- 5, -2, 0);
    }

    private void Loading()
    {

    }

    private void FixedUpdate()
    {
        animator.SetFloat("speed", move.Velocity.magnitude);
    }

    public void ShootAnimation()
    {
        animator.SetTrigger("shoot");
    }
}
