using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Vector3 Velocity { get; set; }
    public float Slowdown { get; set; }
    public Quaternion Rotation => transform.rotation;
    public float SpeedMove { get; set; } = 1;

    private float angleHorizontal;
    private float angleVertical;
    private float mouseSens = 5;
    private float stopFactor = 8;
    private float offsetRotationCamX = 8;
    private LookAtCamera _camera;
    private Player _player;

    private readonly string STR_VERTICAL = "Vertical";
    private readonly string STR_HORIZONTAL = "Horizontal";
    private readonly string STR_MOUSE_X = "Mouse X";
    private readonly string STR_MOUSE_Y = "Mouse Y";

    public void Construct(LookAtCamera camera, Player player)
    {
        _camera = camera;
        _player = player;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        angleHorizontal += Input.GetAxis(STR_MOUSE_X) * mouseSens;
        angleVertical += Input.GetAxis(STR_MOUSE_Y) * mouseSens;
        angleVertical = Mathf.Clamp(angleVertical, -40, 20);

        transform.rotation = Quaternion.Euler(0, angleHorizontal, 0);
        Velocity = (transform.forward * Input.GetAxis(STR_VERTICAL) + transform.right * Input.GetAxis(STR_HORIZONTAL)) * SpeedMove / stopFactor;
        transform.position += Velocity;

        _camera.transform.position = _player.transform.position;
        _camera.transform.localRotation = Quaternion.Euler(-angleVertical + offsetRotationCamX, angleHorizontal, 0);
    }
}
