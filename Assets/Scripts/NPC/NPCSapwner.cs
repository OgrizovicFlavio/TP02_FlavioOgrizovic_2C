using UnityEngine;

public class NPCSapwner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 4f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNPC();
            timer = 0f;
        }
    }

    private void SpawnNPC()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        NPCController npc = PoolManager.Instance.Get<NPCController>();

        npc.transform.position = spawn.position;
        npc.transform.rotation = spawn.rotation;
    }
}
