using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int goldCost = 15; // изначальная цена в монетах 
        public Sprite sprite; // спрайт самой башни
        public Sprite GUISprite; // выбор спрайта интерфейса 
        public TurretProperties TurretProperties;
        [SerializeField] private UpgradeAsset requiedUpgrade;
        [SerializeField] private int requiedUpgradeLevel;
        public bool IsAvailable() => !requiedUpgrade
            || requiedUpgradeLevel <= Upgrades.GetUpgradeLevel(requiedUpgrade);
        public TowerAsset[] m_UpgradesTo;
    }
}
