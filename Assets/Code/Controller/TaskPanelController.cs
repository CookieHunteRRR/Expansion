using Code.Interfaces;
using Code.Configs;
using Code.View;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Controller
{
    internal class TaskPanelController : IInitialize, IExecute
    {
        private UIData _config;
        private TaskPanelView _taskPanelView;
        private Transform _taskPanelButton;

        private bool _isTimerOn; // необходима для контроля работы Execute, чтобы таймер не работал когда панель неактивна
        private float _minimizeTimer;

        public TaskPanelController(UIData config)
        {
            _config = config;
            
            _taskPanelButton = Object.Instantiate(_config.TaskPanelButtonView, _config.Canvas.transform);
            _minimizeTimer = 0f;
            _isTimerOn = false;
        }

        public void Initialize()
        {
            var taskPanel = Object.Instantiate(_config.TaskPanelView, _config.Canvas.transform);
            _taskPanelView = taskPanel.GetComponent<TaskPanelView>();

            Button taskPanelBtn = _taskPanelButton.gameObject.GetComponent<Button>();
            taskPanelBtn.onClick.AddListener(() => ActivateTaskPanel(_taskPanelView, true));

            ActivateTaskPanel(_taskPanelView, true);

            AddTasksToTaskPanel();
        }
        
        public void Execute()
        {
            if (!_isTimerOn) return;

            _minimizeTimer += Time.deltaTime;
            if (_minimizeTimer > _config.SecondsUntilTaskPanelClosed)
            {
                ActivateTaskPanel(_taskPanelView, false);
            }
        }

        private void AddTasksToTaskPanel()
        {
            var task1 = Object.Instantiate(_taskPanelView.TaskElement, _taskPanelView.gameObject.transform);
            task1.gameObject.SetActive(true);
            task1.SetTaskInfo(_config.FirstTaskName, 
            _config.FirstTaskDesc);

            var task2 = Object.Instantiate(_taskPanelView.TaskElement, _taskPanelView.gameObject.transform);
            task2.gameObject.SetActive(true);
            task2.SetTaskInfo(_config.SecondTaskName, 
            _config.SecondTaskDesc);
        }
        
        private void ActivateTaskPanel(IPanelView panel, bool value)
        {
            panel.ViewObject.SetActive(value);
            _taskPanelButton.gameObject.SetActive(!value);

            if (value)
                StartTimer();
            else
                EndTimer();
        }

        // Запускает отсчет до скрытия панели и включения кнопки
        private void StartTimer()
        {
            Debug.Log("Запускаем таймер");
            _minimizeTimer = 0f;
            _isTimerOn = true;
        }

        // Останавливает таймер
        private void EndTimer()
        {
            Debug.Log("Останавливаем таймер");
            _isTimerOn = false;
        }
    }
}


