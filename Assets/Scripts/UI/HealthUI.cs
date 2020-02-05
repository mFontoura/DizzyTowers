using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private IntVar _healthVar = null;
        [SerializeField] private TextMeshProUGUI _text = null;
    
        void Update()
        {
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            _text.text = _healthVar.GetValue().ToString();
        }
    }
}
