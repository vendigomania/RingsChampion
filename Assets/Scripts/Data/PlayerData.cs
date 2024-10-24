using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private bool WipeAllData;


        public CharacterSkinData[] CharacterSkins;
        public BackgroundSkinData[] BackgroundSkins;

        public static PlayerData Instance { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            if (WipeAllData) PlayerPrefs.DeleteAll();

            Instance = this;
        }

        public static UnityAction OnChangeMoneyValue;
        public int Money
        {
            get => PlayerPrefs.GetInt("Money", 0);
            set
            {
                PlayerPrefs.SetInt("Money", value);
                OnChangeMoneyValue?.Invoke();
            }
        }

        public int Score
        {
            get => PlayerPrefs.GetInt("Score", 0);
            set => PlayerPrefs.SetInt("Score", value);
        }
    }
}
