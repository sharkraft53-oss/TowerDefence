using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        public static Action<float> OnWavePrepare;

        [Serializable]
        public class Squad
        {
            public EnemyAsset asset;
            public int count;
        }
        [Serializable]
        private class PathGroup 
        {
            public Squad[] squads;
        }
        [SerializeField] private PathGroup[] groups; // массив групп

        [SerializeField] private float prepareTime = 10f;
        [SerializeField] private EnemyWave next;
        public float GetRemainingTime() { return prepareTime - Time.time; }

        private void Awake()
        {
            enabled = false;
        }

        private event Action OnWaveReady;
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepareTime);
            prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        private void Update()
        {
            if(Time.time >= prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads() // перечесление обьектов 
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i); // yield return возможность возвращать значения не сразу,
                }                                                                        // а по одному функций может быть сколько угодно работает только если есть IEnumerable<>
            }                                                                               // и возвращать сколько угодно 
        }

        internal EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies; // убираем подписку
            if(next) next.Prepare(spawnEnemies);
            return next;
        }
    }
}