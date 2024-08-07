using System;
using System.Threading;
using UnityEngine;

namespace CodeBase.Logic
{
    //https://habr.com/ru/companies/skbkontur/articles/661097/comments/#comment_24276093
    
    static class UniqueId
    {
        static readonly ThreadLocal<ThreadState> State = new(() => new(Guid.NewGuid()));
        
        public static Guid GetNewGuid() => State.Value.NewGuid();
        public static string NewId() => State.Value.NewGuid().ToString();

        class ThreadState
        {
            private readonly byte[] _buffer;
            private ulong _next;

            public ThreadState(Guid seed)
            {
                _buffer = seed.ToByteArray();
                _next = BitConverter.ToUInt64(_buffer, 8);
            }

            public Guid NewGuid()
            {
                var span = _buffer.AsSpan();
                bool r = BitConverter.TryWriteBytes(span[8..], unchecked(_next++));
                return new Guid(span);
            }
        }
    }
}