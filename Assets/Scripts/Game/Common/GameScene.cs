using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;

        private void Awake()
        {
            GameManager.Instance.Initialize();
        }

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.LoadMap();
            GameManager.Instance.LoadBuildings();
            _restartButton.onClick.AddListener(() =>
            {
                GameManager.Instance.RestartGame();
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}

