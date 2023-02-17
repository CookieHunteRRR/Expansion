using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Code.View
{
    public class BuildPanelView : MonoBehaviour, IPanelView
    {
        [SerializeField] private Transform _buildingsContentPanel;
        [SerializeField] private Transform _folder1;
        [SerializeField] private Transform _folder2;
        [SerializeField] private Transform _folder3;
        [SerializeField] private Transform _folder4;
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private BuildingListElement _buildElement;

        public GameObject ViewObject => gameObject;
        public Button ClosePanelButton => _closePanelButton;

        public BuildingListElement BuildElement => _buildElement;

        public Transform BuildingsContentPanel => _buildingsContentPanel;
    }
}
