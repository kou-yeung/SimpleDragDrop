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
        Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponentInParent<Canvas>();
                }
                return canvas;
            }
        }

        RectTransform canvasRectTransform;
        RectTransform CanvasRectTransform
        {
            get
            {
                if (canvasRectTransform == null && Canvas != null)
                {
                    canvasRectTransform = Canvas.transform as RectTransform;
                }
                return canvasRectTransform;
            }
        }
        protected abstract void ApplyGhost(IDragPayload payload);
        protected virtual void ClearGhost() { }

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

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
            var targetCanvas = Canvas;
            var canvasRect = CanvasRectTransform;

            if (targetCanvas == null || canvasRect == null)
            {
                rectTransform.position = screenPosition;
                return;
            }

            var camera = targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : targetCanvas.worldCamera;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
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