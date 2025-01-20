using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RAY_Core
{
    public sealed class PoolingArray<T> where T : class
    {
        private T[] values;

        private Func<T> initEvent;
        private Func<T, T> popEvent;
        private Action<T> pushEvent;
        private Action<T> disposeEvent;

        private int capacity;
        private int countFreeElements;

        private bool isInited;
        private bool isDisposed;

        public int Capacity => capacity;
        public int CountFreeElements => countFreeElements;

        public bool IsInited => isInited;
        public bool IsDisposed => isDisposed;

        public PoolingArray(int capacity, Func<T> initEvent, Func<T, T> popEvent, Action<T> pushEvent, Action<T> disposeEvent)
        {
            this.capacity = capacity;
            this.initEvent = initEvent;
            this.popEvent = popEvent;
            this.pushEvent = pushEvent;
            this.disposeEvent = disposeEvent;
        }

        public void Push(T value)
        {
            if (countFreeElements < capacity && isInited)
            {
                for (int i = 0; i < capacity; i++)
                {
                    ref T element = ref values[i];

                    if (element == default)
                    {
                        element = value;

                        countFreeElements++;

                        pushEvent.Invoke(element);

                        return;
                    }
                }
            }
        }
        public bool Pop(out T value)
        {
            if (countFreeElements > 0)
            {
                for (int i = 0; i < capacity; i++)
                {
                    T element = values[i];

                    if (element != default)
                    {
                        value = element;

                        popEvent.Invoke(value);

                        return true;
                    }
                }
            }

            value = default;

            return false;
        }
        public void OnInit()
        {
            if (!isInited)
            {
                values = new T[capacity];

                for (int i = 0; i < capacity; i++)
                {
                    values[i] = initEvent.Invoke();

                    countFreeElements++;
                }

                isInited = true;
                isDisposed = false;
            }
        }
        public void OnDispose()
        {
            if (!isDisposed && isInited)
            {
                for (int i = 0; i < capacity; i++)
                {
                    var element = values[i];

                    if (element != default)
                    {
                        disposeEvent.Invoke(element);
                    }
                }

                values = default;

                initEvent = default;
                popEvent = default;
                pushEvent = default;
                disposeEvent = default;

                isDisposed = true;
                isInited = false;
            }
        }
    }
}
