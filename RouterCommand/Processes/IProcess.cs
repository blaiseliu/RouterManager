using RouterCommand.ParseOptions;

namespace RouterCommand.Processes
{
    public interface IProcess<T>
        where T : ParseOptionBase
    {
        void Process(T options);
    }
}