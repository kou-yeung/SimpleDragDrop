using R3;
using SimpleDragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public sealed class TextDropArea : DropArea
    {
        [SerializeField] Text label;
        [SerializeField] Image enableImage;
        readonly CompositeDisposable disposables = new();

        void Awake()
        {
            // グループ全体に反応するイベント
            Group.Context.OnDragStartedAsObservable()
                .Subscribe(OnDragStarted)
                .AddTo(disposables);

            Group.Context.OnDragEndedAsObservable()
                .Subscribe(OnDragEnded)
                .AddTo(disposables);

            label.text = string.Empty;
            enableImage.enabled = false;
        }
        private void OnDragStarted(DragStartedEvent @event)
        {
            enableImage.enabled = true;
        }

        private void OnDragEnded(DragEndedEvent @event)
        {
            enableImage.enabled = false;
        }

        void OnDestroy()
        {
            disposables.Dispose();
        }

        protected override void OnDropped(IDragPayload payload)
        {
            if (payload is not TextDragPayload textDragPayload)
            {
                return;
            }
            label.text = textDragPayload.Text;
        }
    }
}