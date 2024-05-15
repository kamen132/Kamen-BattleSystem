using System;
using System.Collections.Generic;
using KamenMessage.RunTime.Basic.Message;
using KamenMessage.RunTime.Interface.Message;

namespace KamenMessage.RunTime.Basic.Model
{
    public abstract class ManualModel : IModel
    {
        private readonly List<IDisposable> mEntrustDisposables = new List<IDisposable>();
        protected MessageService MessageService => Message.MessageService.Instance;
        
        ~ManualModel()
        {
            EntrustDisposablesClear();
        }

        protected void EntrustDisposable(IDisposable disposable)
        {
            mEntrustDisposables.Add(disposable);
        }

        protected IDisposable Register<T>(Action<T> callback) where T : MessageModel
        {
            IDisposable disposable = MessageService.Instance.Register(callback);
            EntrustDisposable(disposable);
            return disposable;
        }

        public void EntrustDisposablesClear()
        {
            foreach (IDisposable entrustDisposable in mEntrustDisposables)
            {
                entrustDisposable.Dispose();
            }
            mEntrustDisposables.Clear();
        }
    }
}