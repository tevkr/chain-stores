namespace ProductService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}