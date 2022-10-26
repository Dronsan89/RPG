using UnityEngine;

public class MoveController : MonoBehaviour
{
    //[SerializeField] private float speedMove;
    //[SerializeField] private float speedRotate;

    public Vector3 Velocity { get; set; }
    public float Slowdown { get; set; }
    public Quaternion Rotation { get; set; }

    //public float SpeedRotate => speedRotate;
    //private Quaternion originRotate;
    //private Quaternion originRotateCam;
    private float angleHorizontal;
    private float angleVertical;
    private float mouseSens = 5;
    private float stopFactor = 8;
    private LookAtCamera camer;

    private readonly string STR_VERTICAL = "Vertical";
    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_MOUSE_X = "Mouse X";
    private readonly string STR_MOUSE_Y = "Mouse Y";

    public void Construct(LookAtCamera cam)
    {
        //cam = Camera.main;
        //cam.GetComponent<LookAtCamera>().Construct(transform);
        camer = cam;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        angleHorizontal += Input.GetAxis(STR_MOUSE_X) * mouseSens;
        //Debug.Log("mouse X: " + angleHorizontal);
        angleVertical += Input.GetAxis(STR_MOUSE_Y) * mouseSens;
        angleVertical = Mathf.Clamp(angleVertical, -40, 20);

        camer.transform.localRotation = Quaternion.Euler(-angleVertical, 0, 0);
        transform.rotation = Quaternion.Euler(0, angleHorizontal, 0);
        Rotation = transform.rotation;

        transform.position += transform.forward * Input.GetAxis(STR_VERTICAL) / stopFactor;

        transform.position += transform.right * Input.GetAxis(STR_HORIZONTAL) / stopFactor;
    }
}
