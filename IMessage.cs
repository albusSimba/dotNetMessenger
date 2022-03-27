namespace dotNetMessengerDemo
{
    public interface IMessage
    {
        string Name();
        void Send(string reciever, object content);
        void Receive(Letter letter);
        void PostFailure(Letter letter);
        void PostalService(PostmanService postmanService);
        void Stop();
    }
}