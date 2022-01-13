using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class Loader : MonoBehaviour
    {
        private void Start()
        {
            SaveGameManager.Instance.Initialize();
            GameDataManager.Instance.Initialize();
            StartCoroutine(LoadGame());
        }

        private IEnumerator LoadGame()
        {
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene("GameScene");
        }
    }
}

