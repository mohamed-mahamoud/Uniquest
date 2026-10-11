using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Tooltip("Objet vide placé sur la tilemap là où le joueur doit apparaître")]
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("PlayerSpawner : aucun spawnPoint assigné !", this);
            return;
        }

        
        Vector3 pos = spawnPoint.position;
        pos.z = 0f;
        transform.position = pos;
    }
}