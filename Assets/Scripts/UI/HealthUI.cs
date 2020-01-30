using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private IntVar _healthVar;
        [SerializeField] private TextMeshProUGUI _text;
    
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
