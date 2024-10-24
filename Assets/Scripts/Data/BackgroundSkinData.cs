using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [CreateAssetMenu(fileName = "SkinData", menuName = "Data/BackgroundSkinData")]
    public class BackgroundSkinData : ScriptableObject
    {
        [SerializeField] private int m_id;
        [SerializeField] private Sprite m_skinElement;

        public static UnityAction OnChange;

        public int Id => m_id;
        public Sprite SkinElement => m_skinElement;

        public bool IsPurchased
        {
            get => PlayerPrefs.GetInt($"BackgroundSkin{m_id}IsPurchased", 0) == 1 || m_id == 0;
            set
            {
                PlayerPrefs.SetInt($"BackgroundSkin{m_id}IsPurchased", value ? 1 : 0);
                OnChange?.Invoke();
            }
        }

        public bool IsSelected
        {
            get => PlayerPrefs.GetInt($"SelectedBackgroundSkinId", 0) == m_id;
            set
            {
                PlayerPrefs.SetInt($"SelectedBackgroundSkinId", value ? m_id : 0);
                OnChange?.Invoke();
            }
        }
    }
}
