using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [CreateAssetMenu(fileName ="SkinData", menuName = "Data/CharacterSkinData")]
    public class CharacterSkinData : ScriptableObject
    {
        [SerializeField] private int m_id;
        [SerializeField] private Sprite[] m_skinElements;

        public static UnityAction OnChange;

        public int Id => m_id;
        public Sprite[] SkinElements => m_skinElements;

        public bool IsPurchased
        {
            get => PlayerPrefs.GetInt($"CharacterSkin{m_id}IsPurchased", 0) == 1 || m_id == 0;
            set
            {
                PlayerPrefs.SetInt($"CharacterSkin{m_id}IsPurchased", value ? 1 : 0);
                OnChange?.Invoke();
            }
        }

        public bool IsSelected
        {
            get => PlayerPrefs.GetInt($"SelectedCharacterSkinId", 0) == m_id;
            set
            {
                PlayerPrefs.SetInt($"SelectedCharacterSkinId", value ? m_id : 0);
                OnChange?.Invoke();
            }
        }
    }
}
