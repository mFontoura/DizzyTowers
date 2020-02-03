using UnityEngine;

namespace UI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuPrefab;

        private PauseManager _pauseManager;
        public void OpenPauseMenu()
        {
            if (ReferenceEquals(_pauseManager, null)) {
                _pauseManager = Instantiate(_pauseMenuPrefab, transform).GetComponent<PauseManager>();
            }
            else {
                _pauseManager.Open();
            }
        }
    }
}
