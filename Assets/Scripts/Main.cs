using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    //[SerializeField] Inventory inventory;
    [SerializeField] InputBase inputBase;
    [SerializeField] Player player;
    [SerializeField] private LookAtCamera cam;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadParameters();
        Init();
    }


    public void Init()
    {
        player.Construct(inputBase, cam);
        //cam.Construct(player.Transform);
        //inventory.Construct();
    }

    /// <summary>
    /// Load all parameters, position in world
    /// </summary>
    private void LoadParameters()
    {

    }
}
