namespace Library.Core.Processing
{
    public interface IState
    {
        public (IState, string) ProcessMessage(UserId id, UserData data, string msg);
    }
}
