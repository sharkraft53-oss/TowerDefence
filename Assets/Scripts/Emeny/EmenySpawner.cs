using TowerDefense;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class EmenySpawner : Spawner
    {
        [SerializeField] private Enemy m_EmenyPrefabs; // ссылка на префаб врага
        [SerializeField] private Path m_path; // ссылка на дорогу 
        [SerializeField] private EnemyAsset[] m_EnemyAsset; // ссылка на настройки

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_EmenyPrefabs);
            e.Use(m_EnemyAsset[Random.Range(0, m_EnemyAsset.Length)]);
            e.GetComponent<TDPatrolController>().SetPath(m_path);
            return e.gameObject;
        }
    }
}