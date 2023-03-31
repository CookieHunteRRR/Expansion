using UnityEngine;

namespace Code.View
{
    public class TaskPanelView : MonoBehaviour, IPanelView
    {
        [SerializeField] private TaskElement _taskElement;

        public GameObject ViewObject => gameObject;

        public TaskElement TaskElement => _taskElement;
    }
}