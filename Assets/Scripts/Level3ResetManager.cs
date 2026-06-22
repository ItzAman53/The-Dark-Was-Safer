using UnityEngine;

public class Level3ResetManager : MonoBehaviour
{
    public static Level3ResetManager Instance;

    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform levelSpawnPoint;

    [SerializeField] private Transform playerRespawnPoint;

    private GameObject currentLevelObjects;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentLevelObjects = GameObject.Find("Level3Respawnables");
    }

    private void SpawnLevelObjects()
    {
        currentLevelObjects =
            Instantiate(levelPrefab,
                        levelSpawnPoint.position,
                        levelSpawnPoint.rotation);
    }

    public void ResetLevel(Transform player)
    {
        if (currentLevelObjects != null)
        {
            Destroy(currentLevelObjects);
        }

        SpawnLevelObjects();

        CharacterController cc =
            player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.position = playerRespawnPoint.position;

        if (cc != null)
            cc.enabled = true;
    }
}