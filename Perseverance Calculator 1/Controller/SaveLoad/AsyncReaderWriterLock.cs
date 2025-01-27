using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Documents;

namespace Perseverance_Calculator_1.Controller.SaveLoad
{
    //public class Test
    //{
        
    //    async void testx()
    //    {
    //        AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();
    //        Task t = null;
    //        using (var releaser = await m_lock.WriterLockAsync())
    //        {
    //            // code #1
    //            //t = SomethingAsync();
    //        }
    //        await t;
    //        using (var releaser = await m_lock.WriterLockAsync())
    //        {
    //            // code #2
    //        }
    //        //private readonly AsyncReaderWriterLock m_lock = new AsyncReaderWriterLock();
    //        //using(var releaser = await m_lock.ReaderLockAsync())
    //        //{
    //        //     // protected code here
    //        //}
    //    }
    //}
    public class AsyncReaderWriterLock
    {

        public struct Releaser : IDisposable
        {
            private readonly AsyncReaderWriterLock m_toRelease;
            private readonly bool m_writer;

            internal Releaser(AsyncReaderWriterLock toRelease, bool writer)
            {
                m_toRelease = toRelease;
                m_writer = writer;
            }

            public void Dispose()
            {
                if (m_toRelease != null)
                {
                    if (m_writer) m_toRelease.WriterRelease();
                    else m_toRelease.ReaderRelease();
                }
            }
        }


        private readonly Task<Releaser> m_readerReleaser;
        private readonly Task<Releaser> m_writerReleaser;
        //public AsyncReaderWriterLock();
        public AsyncReaderWriterLock()
        {
            m_readerReleaser = Task.FromResult(new Releaser(this, false));
            m_writerReleaser = Task.FromResult(new Releaser(this, true));
        }



        private readonly Queue<TaskCompletionSource<Releaser>> m_waitingWriters =
            new Queue<TaskCompletionSource<Releaser>>();
        private TaskCompletionSource<Releaser> m_waitingReader =
            new TaskCompletionSource<Releaser>();
        private int m_readersWaiting;

        private int m_status;


        public Task<Releaser> ReaderLockAsync()
        {
            lock (m_waitingWriters)
            {
                if (m_status >= 0 && m_waitingWriters.Count == 0)
                {
                    ++m_status;
                    return m_readerReleaser;
                }
                else
                {
                    ++m_readersWaiting;
                    return m_waitingReader.Task.ContinueWith(t => t.Result);
                }
            }
        }
        public Task<Releaser> WriterLockAsync()
        {
            lock (m_waitingWriters)
            {
                if (m_status == 0)
                {
                    m_status = -1;
                    return m_writerReleaser;
                }
                else
                {
                    var waiter = new TaskCompletionSource<Releaser>();
                    m_waitingWriters.Enqueue(waiter);
                    return waiter.Task;
                }
            }
        }
        private void ReaderRelease()
        {
            TaskCompletionSource<Releaser> toWake = null;

            lock (m_waitingWriters)
            {
                --m_status;
                if (m_status == 0 && m_waitingWriters.Count > 0)
                {
                    m_status = -1;
                    toWake = m_waitingWriters.Dequeue();
                }
            }

            if (toWake != null)
                toWake.SetResult(new Releaser(this, true));
        }

        private void WriterRelease()
        {
            TaskCompletionSource<Releaser> toWake = null;
            bool toWakeIsWriter = false;

            lock (m_waitingWriters)
            {
                if (m_waitingWriters.Count > 0)
                {
                    toWake = m_waitingWriters.Dequeue();
                    toWakeIsWriter = true;
                }
                else if (m_readersWaiting > 0)
                {
                    toWake = m_waitingReader;
                    m_status = m_readersWaiting;
                    m_readersWaiting = 0;
                    m_waitingReader = new TaskCompletionSource<Releaser>();
                }
                else m_status = 0;
            }

            if (toWake != null)
                toWake.SetResult(new Releaser(this, toWakeIsWriter));
        }


    }

}
