using Photon.Pun;
using UnityEngine;

public sealed class Spawner : MonoBehaviourPun
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            CreateEnemy(this, i);
        }
    }
    
    public void CreateEnemy(Spawner spawner, int position)
    { 
        var enemy  = PhotonNetwork.InstantiateRoomObject(_enemyPrefab.name, _spawnPoints[position].position, Quaternion.identity);
        var sp = enemy.GetComponent<EnemyBase>();
        sp.SetSpawnInfo(spawner, position);
    }
}

public abstract class EnemyBase : MonoBehaviourPun
{
    protected int IndexPosition;
    protected Spawner Spawner;

    public void SetSpawnInfo(Spawner spawner, int index)
    {
        IndexPosition = index;
        Spawner = spawner;

    }
}