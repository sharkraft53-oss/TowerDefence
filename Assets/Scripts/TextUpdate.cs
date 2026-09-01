using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSourse { Gold, Life, Mana }
        public UpdateSourse sourse = UpdateSourse.Gold;
        private Text m_text;

        private void Start()
        {
            m_text = GetComponent<Text>();
            switch (sourse)
            {
                case UpdateSourse.Gold:
                    TDPlayer.Instance.GoldUpdateSubsctibe(UpdateText);
                    break;
                case UpdateSourse.Life: 
                    TDPlayer.Instance.LifeUpdatesSubsctibe (UpdateText);
                    break;
                case UpdateSourse.Mana: // !!!!
                    TDPlayer.Instance.ManaUpdateSubsctibe(UpdateText);
                    break;
            }
        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }
        /*
         // добавили с ментором 
        // вызываем методы отписки при уничтожении объекта TextUpdate
        private void OnDestroy()
        {
            TDPlayer.GoldUbdateUnsubsctibe(UpdateText);
            TDPlayer.LifeUbdateUnSubsctibe(UpdateText);
        }
        */
    }
}

