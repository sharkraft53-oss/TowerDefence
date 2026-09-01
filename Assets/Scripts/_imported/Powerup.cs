using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Базовые класс подбираемых объектов.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var ship = collision.transform.root.GetComponent<SpaceShip>();

            if(ship != null && Player.Instance.ActiveShip == ship)
            {
                OnPickedUp(ship);

                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Метод для обработки события подбирания шмотки кораблем игрока.
        /// </summary>
        /// <param name="playerShip"></param>
        protected abstract void OnPickedUp(SpaceShip playerShip);
    }
}