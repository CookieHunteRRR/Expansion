using Code.Configs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.View
{
    public class ResourceIconElementView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _resourceCount;
        [SerializeField] private ResourcesType _type;

        public void Init(Sprite icon, int startCount)
        {
            SetIcon(icon, startCount);
        }

        public ResourcesType Type => _type;
        
        private void SetIcon(Sprite icon, int startCount)
        {
            _icon.sprite = icon;
            _resourceCount.text = startCount.ToString();
        }

        private void ShowResourceCount(int count)
        {
            _resourceCount.text = count.ToString();
        }
    }
}
