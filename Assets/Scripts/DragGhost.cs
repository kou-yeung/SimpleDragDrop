using UnityEngine;

namespace SimpleDragDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public abstract class DragGhost : MonoBehaviour
    {
        RectTransform rectTransform;
        CanvasGroup canvasGroup;

        Canvas canvas;
        RectTransform canvasRectTransform;

        protected abstract void ApplyGhost(IDragPayload payload);
        protected virtual void ClearGhost() { }

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

            canvas = GetComponentInParent<Canvas>();
            canvasRectTransform = canvas.transform as RectTransform;

            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        public void Show(IDragPayload payload, Vector2 screenPosition)
        {
            gameObject.SetActive(true);
            canvasGroup.blocksRaycasts = false;

            ApplyGhost(payload);
            Move(screenPosition);
        }

        public void Move(Vector2 screenPosition)
        {
            var camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : canvas.worldCamera;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,
                screenPosition,
                camera,
                out var localPosition);

            rectTransform.localPosition = localPosition;
        }

        public void Hide()
        {
            ClearGhost();
            gameObject.SetActive(false);
        }
    }
}