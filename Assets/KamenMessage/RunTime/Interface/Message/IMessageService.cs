﻿using System;
using KamenMessage.RunTime.Basic.Message;

namespace KamenMessage.RunTime.Interface.Message
{
    /// <summary>
    /// 消息服务接口
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="callback">消息回调</param>
        /// <typeparam name="T">消息类型</typeparam>
        /// <returns></returns>
        IDisposable Register<T>(Action<T> callback) where T : MessageModel;
        
        
        void Register(Type type, Action<object> callback);

        /// <summary>
        /// 触发消息
        /// </summary>
        /// <param name="msg"></param>
        /// <typeparam name="T"></typeparam>
        void Dispatch<T>(T msg) where T : MessageModel;
    }
}