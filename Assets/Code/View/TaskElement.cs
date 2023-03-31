using UnityEngine;
using TMPro;

public class TaskElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _taskName;
    [SerializeField] private TMP_Text _taskDescription;

    public void SetTaskInfo(string name, string desc)
    {
        _taskName.text = name;
        _taskDescription.text = desc;
    }
}
