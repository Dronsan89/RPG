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
        cam.GetComponent<LookAtCamera>().Construct(player.Transform);
        //inventory.Construct();

        //planets.Construct(account.Missions);
        //loadout.Construct(inventory, account.ItemLoadouts, account.Soldiers, account.SelectedSoldier);
        //CurrentAccount = account;
    }

    /// <summary>
    /// Load all parameters, position in world
    /// </summary>
    private void LoadParameters()
    {

    }
}