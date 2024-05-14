using System;

namespace KamenMessage.RunTime.Interface.Message
{
    /// <summary>
    /// 消息处理
    /// </summary>
    public interface IMessageHandle
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        Type MessageType { get; }

        /// <summary>
        /// 触发
        /// </summary>
        /// <param name="msg"></param>
        void Handle(object msg);
    }

}