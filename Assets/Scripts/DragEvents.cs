namespace SimpleDragDrop
{
    public readonly struct DragStartedEvent
    {
        public readonly DraggableItem Source;
        public readonly IDragPayload Payload;

        public DragStartedEvent(DraggableItem source, IDragPayload payload)
        {
            Source = source;
            Payload = payload;
        }
    }

    public readonly struct DragEndedEvent
    {
        public readonly DraggableItem Source;
        public readonly IDragPayload Payload;

        public DragEndedEvent(DraggableItem source, IDragPayload payload)
        {
            Source = source;
            Payload = payload;
        }
    }

    public readonly struct DroppedEvent
    {
        public readonly DraggableItem Source;
        public readonly DropArea Target;
        public readonly IDragPayload Payload;

        public DroppedEvent(DraggableItem source, DropArea target, IDragPayload payload)
        {
            Source = source;
            Target = target;
            Payload = payload;
        }
    }
}