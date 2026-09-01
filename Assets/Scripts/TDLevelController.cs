using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>     // ������ ��������� �� ������� ��� � ��� ����� ���������� ������� � ����� ������ ��������� ��������
                                                        // ����� �������� ����� �������� ������ ��� ���� ����� ������ ����� �� ������������ ������ �����
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                if (m_ReferenceTime <= Time.time)
                {
                    levelScore -= 1;
                }
                MapCompletion.SaveEpisodeResult(levelScore);
            });

            void LifeScoreChange(int _)  // ��������� ������ ������������� ������������ ������������ ��� ������� ������� ����� �� ������������ 

            {
                levelScore -= 1;
                TDPlayer.Instance.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.Instance.OnLifeUpdate += LifeScoreChange;
        }
        /*
        ����������� ����� ������� ������
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += EndLevel;
        }
        private void EndLevel()
        {
            StopLevelActivity();
            LevelResultController.Instance.Show(false);
        }
        */
        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            }

            void DisableAll<T>() where T : MonoBehaviour // ������ ��� ������� ������ ���� �������������� ���� ����
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
            /*
            foreach(var spawner in FindObjectsOfType<Spawner>())
            {
                spawner.enabled = false;
            }
            foreach (var projectile in FindObjectsOfType<Projectile>())
            {
                projectile.enabled = false;
            }
            foreach (var tower in FindObjectsOfType<Tower>())
            {
                tower.enabled = false;
            }
            */
        }
    }
}
