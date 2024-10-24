using UnityEngine;

namespace UI.Settings
{
    public class ToggleStatus : MonoBehaviour
    {
        [SerializeField] private GameObject on;
        [SerializeField] private GameObject off;

        public void SetStatus(bool _on)
        {
            on.SetActive(_on);
            off.SetActive(!_on);
        }
    }
}
