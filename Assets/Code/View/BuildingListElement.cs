using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    public class BuildingListElement : MonoBehaviour
    {
        [SerializeField] private Button _build;
        [SerializeField] private TMP_Text _buildingName;
        [SerializeField] private TMP_Text _buildingInfo;
        [SerializeField] private Transform _resPanel;
        [SerializeField] private Transform _resConsumablePanel;

        public Button Build => _build;

        public void SetBuildingInfo(string header, string info)
        {
            _buildingName.text = header;
            _buildingInfo.text = info;
        }

        public void AddRes(Transform resIcon)
        {
            resIcon.SetParent(_resPanel);
        }

        public void AddColonistEnergy(Transform resIcon)
        {
            resIcon.SetParent(_resConsumablePanel);
        }
    }
}
