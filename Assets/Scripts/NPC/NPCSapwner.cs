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
        bool spawnCivilian = Random.value < 0.5f;

        if (spawnCivilian)
        {
            Civilian npc = PoolManager.Instance.Get<Civilian>(spawn.position, spawn.rotation);
        }
        else
        {
            Alien npc = PoolManager.Instance.Get<Alien>(spawn.position, spawn.rotation);
        }
    }
}
