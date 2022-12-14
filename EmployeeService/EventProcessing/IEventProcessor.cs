namespace EmployeeService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}