namespace EasyFramework
{
    public interface IEventHandle
    {
        void ProcessEvent(ushort id,params object[] objs);
    }
}
