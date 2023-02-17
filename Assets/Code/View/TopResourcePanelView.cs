using Code.Configs;
using UnityEngine;

namespace Code.View
{
    public class TopResourcePanelView : MonoBehaviour
    {
        [SerializeField] private Transform _topPanel;
        [SerializeField] private Transform _topFrame;
        [SerializeField] private Transform _firstGroup;
        [SerializeField] private Transform _secondGroup;
        [SerializeField] private Transform _thirdGroup;
        [SerializeField] private Transform _fourthGroup;

        private UIData _config;
        private RectTransform _rectTransformPanel;
        private RectTransform _rectTransformFrame;

        public void Init(UIData config)
        {
            _config = config;
            _rectTransformPanel = _topPanel.GetComponent<RectTransform>();
            _rectTransformFrame = _topFrame.GetComponent<RectTransform>();
        }

        public void AddElement(ResourceIconElementView elementView)
        {
            elementView.transform.SetParent(_topPanel);
            ZoomInPanelSize();
        }

        public void ZoomInPanelSize()
        {
            _rectTransformPanel.sizeDelta = new Vector2(_rectTransformPanel.sizeDelta.x + _config.ResIconTopStep,
                _rectTransformPanel.sizeDelta.y);
            _rectTransformFrame.sizeDelta = new Vector2(_rectTransformFrame.sizeDelta.x + _config.ResIconTopStep,
                _rectTransformFrame.sizeDelta.y);
        }        
        
        public void ZoomOutPanelSize()
        {
            _rectTransformPanel.sizeDelta = new Vector2(_rectTransformPanel.sizeDelta.x - _config.ResIconTopStep,
                _rectTransformPanel.sizeDelta.y);
            _rectTransformFrame.sizeDelta = new Vector2(_rectTransformFrame.sizeDelta.x - _config.ResIconTopStep,
                _rectTransformFrame.sizeDelta.y);
        }
    }
}
