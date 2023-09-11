using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlocksGrid : MonoBehaviour
{
    private readonly List<ContactPoint> m_contactPoints = new List<ContactPoint>();
    private Rigidbody m_rigidbody;
    private float m_lastSpawnTime;
    private uint m_count;
    [SerializeField] 
    private GameObject BlockPrefab;
    [SerializeField] 
    private uint Vertical;
    [SerializeField] 
    private uint Horizontal;
    [SerializeField]
    private int BlocksCount;
    [SerializeField]
    private float TimeBetweenSpawn;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1));
        SpawnBlock();
    }

    private void Update()
    {
        if (m_count >= BlocksCount || Time.time < m_lastSpawnTime + TimeBetweenSpawn)
            return;

        SpawnBlock();
    }

    private void SpawnBlock()
    {
        var newPosition = new Vector3Int(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        var block = Instantiate(BlockPrefab, transform);
        block.transform.localPosition = newPosition;
        m_lastSpawnTime = Time.time;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        var contact = other.GetContact(0);
        Destroy(contact.thisCollider.attachedRigidbody == m_rigidbody
            ? contact.thisCollider.gameObject
            : contact.otherCollider.gameObject);
    }
}
