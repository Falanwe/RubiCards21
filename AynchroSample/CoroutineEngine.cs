using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AynchroSample
{
    public class CoroutineEngine : IDisposable
    {
        private bool _isRunning = true;
        private bool _disposedValue;
        private readonly List<IEnumerator> _enumerators = new List<IEnumerator>();

        public CoroutineEngine()
        {
            ThreadPool.QueueUserWorkItem(Run);
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void ExecuteCoroutine(IEnumerator enumerator)
        {
            _enumerators.Add(enumerator);
        }

        private void Run(object _)
        {
            while(_isRunning)
            {
                ExecuteOneStep();
                Thread.Sleep(100);
            }
        }
        
        private void ExecuteOneStep()
        {
            int index=0;
            while(index < _enumerators.Count)
            {
                if(!_enumerators[index].MoveNext())
                {
                    _enumerators.RemoveAt(index);
                }                
                else
                {
                    index++;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _isRunning = false;
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CoroutineEngine()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
