using System;
using KamenMessage.RunTime.Interface.Message;

namespace KamenMessage.RunTime.Basic.Message
{
    public class MessageHandle<T> : IMessageHandle, IDisposable
    {
        private readonly Action<T> mCallback;

        private readonly Action<IMessageHandle> mONDispose;

        public Type MessageType => typeof(T);

        public MessageHandle(Action<T> callback, Action<IMessageHandle> onDispose)
        {
            mCallback = callback;
            mONDispose = onDispose;
        }

        public void Handle(object msg)
        {
            mCallback?.Invoke((T)msg);
        }

        public void Dispose()
        {
            mONDispose?.Invoke(this);
        }
    }
}