using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public Sprite closedChest;
    public Sprite openedChest;
    public GameObject lootPrefab;
    public AudioSource audioSource;
    public AudioClip openClip;

    [Tooltip("Spawn offset")]
    public Vector3 lootSpawnOffset;

    private bool isOpened = false;
    private bool isPlayerNearby = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedChest;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void OpenChest()
    {
        if (isOpened) return;

        isOpened = true;
        spriteRenderer.sprite = openedChest;
        audioSource.PlayOneShot(openClip);
        SpawnLoot();
    }

    private void SpawnLoot()
    {
        if (lootPrefab != null)
        {
            Vector3 spawnPosition = transform.position + lootSpawnOffset;
            Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Loot spawned at " + spawnPosition);
        }
    }
}
