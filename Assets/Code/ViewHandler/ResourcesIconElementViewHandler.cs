using UnityEngine;

namespace Code.View
{
    public class ResourcesIconElementViewHandler
    {

        public ResourcesIconElementViewHandler(ResourceIconElementView resTopElement, TopResourcePanelView topResourcePanelView, Sprite icon, int startCount)
        {
            var newElement = Object.Instantiate(resTopElement, topResourcePanelView.transform);
            var resElement = newElement.GetComponent<ResourceIconElementView>();
            resElement.Init(icon, startCount);
            topResourcePanelView.AddElement(resElement);

            if (startCount == 0)
            {
                resElement.gameObject.SetActive(false);
                topResourcePanelView.ZoomOutPanelSize();
            }
        }
    }
}
