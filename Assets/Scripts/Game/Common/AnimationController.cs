using Game.Models;
using TMPro;
using UnityEngine;

namespace Game.Common
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _floatingTextPrefab;

        public void FloatingTextAnimation(ResourceType resource, int amount, Vector3 position)
        {
            var floatingText = Instantiate(_floatingTextPrefab, transform);

            if (amount > 0)
            {
                floatingText.GetComponent<TextMeshProUGUI>().text = $"+{amount}";
            }
            else if (amount < 0)
            {
                floatingText.GetComponent<TextMeshProUGUI>().text = $"{amount}";
            }
            var colorGold = new Color(1f, 0.9f, 0f);
            var colorGem = new Color(0.7f, 0.9f, 1f);
            var gradientColor = new Color(0.5f, 0.5f, 0.5f);
            switch (resource)
            {
                case ResourceType.Gold:
                    floatingText.transform.position = position;
                    floatingText.GetComponent<TextMeshProUGUI>().color = colorGold;
                    floatingText.GetComponent<TextMeshProUGUI>().colorGradient = new VertexGradient(colorGold, colorGold, gradientColor, gradientColor);
                    break;
                case ResourceType.Gem:
                    floatingText.transform.position = position;
                    floatingText.GetComponent<TextMeshProUGUI>().color = colorGem;
                    floatingText.GetComponent<TextMeshProUGUI>().colorGradient = new VertexGradient(colorGem, colorGem, gradientColor, gradientColor);
                    break;
            }
            LeanTween.moveY(floatingText, position.y + 50, 0.8f).setEaseInOutQuad().setOnComplete(() => {
                Destroy(floatingText);
            });
        }
    }
}

