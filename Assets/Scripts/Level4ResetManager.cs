using UnityEngine;

public class Level4ResetManager : MonoBehaviour
{
    public static Level4ResetManager Instance;

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
        currentLevelObjects = GameObject.Find("Level4Respawnables");
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
        EnemyMovement[] enemies = FindObjectsByType<EnemyMovement>();

        foreach (EnemyMovement enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        SpawnLevelObjects();
        player.GetComponent<PlayerMovement>().Level4TreasureCount=0;

        CharacterController cc =
            player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.position = playerRespawnPoint.position;

        if (cc != null)
            cc.enabled = true;
    }
}