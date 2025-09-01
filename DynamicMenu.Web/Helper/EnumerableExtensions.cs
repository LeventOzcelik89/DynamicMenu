using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DynamicMenu.Web.Helper
{
    public static class EnumerableExtensions
    {
        private class GenericEnumerable<T> : IEnumerable<T>, IEnumerable
        {
            [CompilerGenerated]
            private sealed class _003CSystem_002DCollections_002DGeneric_002DIEnumerable_003CT_003E_002DGetEnumerator_003Ed__3 : IEnumerator<T>, IEnumerator, IDisposable
            {
                private int _003C_003E1__state;

                private T _003C_003E2__current;

                public GenericEnumerable<T> _003C_003E4__this;

                private IEnumerator _003C_003E7__wrap1;

                T IEnumerator<T>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return _003C_003E2__current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return _003C_003E2__current;
                    }
                }

                [DebuggerHidden]
                public _003CSystem_002DCollections_002DGeneric_002DIEnumerable_003CT_003E_002DGetEnumerator_003Ed__3(int _003C_003E1__state)
                {
                    this._003C_003E1__state = _003C_003E1__state;
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = _003C_003E1__state;
                    if (num == -3 || num == 1)
                    {
                        try
                        {
                        }
                        finally
                        {
                            _003C_003Em__Finally1();
                        }
                    }

                    _003C_003E7__wrap1 = null;
                    _003C_003E1__state = -2;
                }

                private bool MoveNext()
                {
                    try
                    {
                        int num = _003C_003E1__state;
                        GenericEnumerable<T> genericEnumerable = _003C_003E4__this;
                        switch (num)
                        {
                            default:
                                return false;
                            case 0:
                                _003C_003E1__state = -1;
                                _003C_003E7__wrap1 = genericEnumerable.source.GetEnumerator();
                                _003C_003E1__state = -3;
                                break;
                            case 1:
                                _003C_003E1__state = -3;
                                break;
                        }

                        if (_003C_003E7__wrap1.MoveNext())
                        {
                            T val = (T)_003C_003E7__wrap1.Current;
                            _003C_003E2__current = val;
                            _003C_003E1__state = 1;
                            return true;
                        }

                        _003C_003Em__Finally1();
                        _003C_003E7__wrap1 = null;
                        return false;
                    }
                    catch
                    {
                        //try-fault
                        ((IDisposable)this).Dispose();
                        throw;
                    }
                }

                bool IEnumerator.MoveNext()
                {
                    //ILSpy generated this explicit interface implementation from .override directive in MoveNext
                    return this.MoveNext();
                }

                private void _003C_003Em__Finally1()
                {
                    _003C_003E1__state = -1;
                    if (_003C_003E7__wrap1 is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }
            }

            private readonly IEnumerable source;

            //
            // Summary:
            //     Initializes a new instance of the Kendo.Mvc.Extensions.EnumerableExtensions.GenericEnumerable`1
            //     class.
            //
            // Parameters:
            //   source:
            //     The source.
            public GenericEnumerable(IEnumerable source)
            {
                this.source = source;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return source.GetEnumerator();
            }

            [IteratorStateMachine(typeof(GenericEnumerable<>._003CSystem_002DCollections_002DGeneric_002DIEnumerable_003CT_003E_002DGetEnumerator_003Ed__3))]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
                return new _003CSystem_002DCollections_002DGeneric_002DIEnumerable_003CT_003E_002DGetEnumerator_003Ed__3(0)
                {
                    _003C_003E4__this = this
                };
            }
        }

        private static class DefaultReadOnlyCollection<T>
        {
            private static ReadOnlyCollection<T> defaultCollection;

            internal static ReadOnlyCollection<T> Empty
            {
                get
                {
                    if (defaultCollection == null)
                    {
                        defaultCollection = new ReadOnlyCollection<T>(new T[0]);
                    }

                    return defaultCollection;
                }
            }
        }

        [CompilerGenerated]
        private sealed class _003CSelectRecursive_003Ed__6<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IEnumerator, IDisposable
        {
            private int _003C_003E1__state;

            private TSource _003C_003E2__current;

            private int _003C_003El__initialThreadId;

            private IEnumerable<TSource> source;

            public IEnumerable<TSource> _003C_003E3__source;

            private Func<TSource, IEnumerable<TSource>> recursiveSelector;

            public Func<TSource, IEnumerable<TSource>> _003C_003E3__recursiveSelector;

            private Stack<IEnumerator<TSource>> _003Cstack_003E5__2;

            private TSource _003Ccurrent_003E5__3;

            TSource IEnumerator<TSource>.Current
            {
                [DebuggerHidden]
                get
                {
                    return _003C_003E2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return _003C_003E2__current;
                }
            }

            [DebuggerHidden]
            public _003CSelectRecursive_003Ed__6(int _003C_003E1__state)
            {
                this._003C_003E1__state = _003C_003E1__state;
                _003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = _003C_003E1__state;
                if (num == -3 || num == 1)
                {
                    try
                    {
                    }
                    finally
                    {
                        _003C_003Em__Finally1();
                    }
                }

                _003Cstack_003E5__2 = null;
                _003Ccurrent_003E5__3 = default(TSource);
                _003C_003E1__state = -2;
            }

            private bool MoveNext()
            {
                try
                {
                    switch (_003C_003E1__state)
                    {
                        default:
                            return false;
                        case 0:
                            _003C_003E1__state = -1;
                            _003Cstack_003E5__2 = new Stack<IEnumerator<TSource>>();
                            _003Cstack_003E5__2.Push(source.GetEnumerator());
                            _003C_003E1__state = -3;
                            break;
                        case 1:
                            {
                                _003C_003E1__state = -3;
                                IEnumerable<TSource> enumerable = recursiveSelector(_003Ccurrent_003E5__3);
                                if (enumerable != null)
                                {
                                    _003Cstack_003E5__2.Push(enumerable.GetEnumerator());
                                }

                                _003Ccurrent_003E5__3 = default(TSource);
                                break;
                            }
                    }

                    while (_003Cstack_003E5__2.Count > 0)
                    {
                        if (_003Cstack_003E5__2.Peek().MoveNext())
                        {
                            _003Ccurrent_003E5__3 = _003Cstack_003E5__2.Peek().Current;
                            _003C_003E2__current = _003Ccurrent_003E5__3;
                            _003C_003E1__state = 1;
                            return true;
                        }

                        _003Cstack_003E5__2.Pop().Dispose();
                    }

                    _003C_003Em__Finally1();
                    return false;
                }
                catch
                {
                    //try-fault
                    ((IDisposable)this).Dispose();
                    throw;
                }
            }

            bool IEnumerator.MoveNext()
            {
                //ILSpy generated this explicit interface implementation from .override directive in MoveNext
                return this.MoveNext();
            }

            private void _003C_003Em__Finally1()
            {
                _003C_003E1__state = -1;
                while (_003Cstack_003E5__2.Count > 0)
                {
                    _003Cstack_003E5__2.Pop().Dispose();
                }
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator()
            {
                _003CSelectRecursive_003Ed__6<TSource> _003CSelectRecursive_003Ed__;
                if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
                {
                    _003C_003E1__state = 0;
                    _003CSelectRecursive_003Ed__ = this;
                }
                else
                {
                    _003CSelectRecursive_003Ed__ = new _003CSelectRecursive_003Ed__6<TSource>(0);
                }

                _003CSelectRecursive_003Ed__.source = _003C_003E3__source;
                _003CSelectRecursive_003Ed__.recursiveSelector = _003C_003E3__recursiveSelector;
                return _003CSelectRecursive_003Ed__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<TSource>)this).GetEnumerator();
            }
        }

        [CompilerGenerated]
        private sealed class _003CZipIterator_003Ed__8<TFirst, TSecond, TResult> : IEnumerable<TResult>, IEnumerable, IEnumerator<TResult>, IEnumerator, IDisposable
        {
            private int _003C_003E1__state;

            private TResult _003C_003E2__current;

            private int _003C_003El__initialThreadId;

            private IEnumerable<TFirst> first;

            public IEnumerable<TFirst> _003C_003E3__first;

            private IEnumerable<TSecond> second;

            public IEnumerable<TSecond> _003C_003E3__second;

            private Func<TFirst, TSecond, TResult> resultSelector;

            public Func<TFirst, TSecond, TResult> _003C_003E3__resultSelector;

            private IEnumerator<TFirst> _003Ce1_003E5__2;

            private IEnumerator<TSecond> _003Ce2_003E5__3;

            TResult IEnumerator<TResult>.Current
            {
                [DebuggerHidden]
                get
                {
                    return _003C_003E2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return _003C_003E2__current;
                }
            }

            [DebuggerHidden]
            public _003CZipIterator_003Ed__8(int _003C_003E1__state)
            {
                this._003C_003E1__state = _003C_003E1__state;
                _003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = _003C_003E1__state;
                if ((uint)(num - -4) <= 1u || num == 1)
                {
                    try
                    {
                        if (num == -4 || num == 1)
                        {
                            try
                            {
                            }
                            finally
                            {
                                _003C_003Em__Finally2();
                            }
                        }
                    }
                    finally
                    {
                        _003C_003Em__Finally1();
                    }
                }

                _003Ce1_003E5__2 = null;
                _003Ce2_003E5__3 = null;
                _003C_003E1__state = -2;
            }

            private bool MoveNext()
            {
                try
                {
                    switch (_003C_003E1__state)
                    {
                        default:
                            return false;
                        case 0:
                            _003C_003E1__state = -1;
                            _003Ce1_003E5__2 = first.GetEnumerator();
                            _003C_003E1__state = -3;
                            _003Ce2_003E5__3 = second.GetEnumerator();
                            _003C_003E1__state = -4;
                            break;
                        case 1:
                            _003C_003E1__state = -4;
                            break;
                    }

                    if (_003Ce1_003E5__2.MoveNext() && _003Ce2_003E5__3.MoveNext())
                    {
                        _003C_003E2__current = resultSelector(_003Ce1_003E5__2.Current, _003Ce2_003E5__3.Current);
                        _003C_003E1__state = 1;
                        return true;
                    }

                    _003C_003Em__Finally2();
                    _003Ce2_003E5__3 = null;
                    _003C_003Em__Finally1();
                    _003Ce1_003E5__2 = null;
                    return false;
                }
                catch
                {
                    //try-fault
                    ((IDisposable)this).Dispose();
                    throw;
                }
            }

            bool IEnumerator.MoveNext()
            {
                //ILSpy generated this explicit interface implementation from .override directive in MoveNext
                return this.MoveNext();
            }

            private void _003C_003Em__Finally1()
            {
                _003C_003E1__state = -1;
                if (_003Ce1_003E5__2 != null)
                {
                    _003Ce1_003E5__2.Dispose();
                }
            }

            private void _003C_003Em__Finally2()
            {
                _003C_003E1__state = -3;
                if (_003Ce2_003E5__3 != null)
                {
                    _003Ce2_003E5__3.Dispose();
                }
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator()
            {
                _003CZipIterator_003Ed__8<TFirst, TSecond, TResult> _003CZipIterator_003Ed__;
                if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
                {
                    _003C_003E1__state = 0;
                    _003CZipIterator_003Ed__ = this;
                }
                else
                {
                    _003CZipIterator_003Ed__ = new _003CZipIterator_003Ed__8<TFirst, TSecond, TResult>(0);
                }

                _003CZipIterator_003Ed__.first = _003C_003E3__first;
                _003CZipIterator_003Ed__.second = _003C_003E3__second;
                _003CZipIterator_003Ed__.resultSelector = _003C_003E3__resultSelector;
                return _003CZipIterator_003Ed__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<TResult>)this).GetEnumerator();
            }
        }

        public static void Each<T>(this IEnumerable<T> instance, Action<T, int> action)
        {
            int num = 0;
            foreach (T item in instance)
            {
                action(item, num++);
            }
        }

        //
        // Summary:
        //     Executes the provided delegate for each item.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   action:
        //     The action to be applied.
        //
        // Type parameters:
        //   T:
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            foreach (T item in instance)
            {
                action(item);
            }
        }

        public static IEnumerable AsGenericEnumerable(this IEnumerable source)
        {
            Type type = typeof(object);
            if (source.GetType().GetGenericTypeDefinition() != typeof(IEnumerable<>))
            {
                return source;
            }

            IEnumerator enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    type = enumerator.Current.GetType();
                    try
                    {
                        enumerator.Reset();
                    }
                    catch
                    {
                    }

                    break;
                }
            }

            Type type2 = typeof(GenericEnumerable<>).MakeGenericType(type);
            object[] args = new object[1] { source };
            return (IEnumerable)Activator.CreateInstance(type2, args);
        }

        public static int IndexOf(this IEnumerable source, object item)
        {
            int num = 0;
            foreach (object item2 in source)
            {
                if (object.Equals(item2, item))
                {
                    return num;
                }

                num++;
            }

            return -1;
        }

        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is out of range.
        internal static object ElementAt(this IEnumerable source, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (source is IList { Count: > 0 } list)
            {
                return list[index];
            }

            foreach (object item in source)
            {
                if (index == 0)
                {
                    return item;
                }

                index--;
            }

            return null;
        }

        [IteratorStateMachine(typeof(_003CSelectRecursive_003Ed__6<>))]
        public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> recursiveSelector)
        {
            //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
            return new _003CSelectRecursive_003Ed__6<TSource>(-2)
            {
                _003C_003E3__source = source,
                _003C_003E3__recursiveSelector = recursiveSelector
            };
        }

        internal static IEnumerable<TResult> Consolidate<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }

            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            if (resultSelector == null)
            {
                throw new ArgumentNullException("resultSelector");
            }

            return ZipIterator(first, second, resultSelector);
        }

        [IteratorStateMachine(typeof(_003CZipIterator_003Ed__8<,,>))]
        private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
            return new _003CZipIterator_003Ed__8<TFirst, TSecond, TResult>(-2)
            {
                _003C_003E3__first = first,
                _003C_003E3__second = second,
                _003C_003E3__resultSelector = resultSelector
            };
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                return DefaultReadOnlyCollection<T>.Empty;
            }

            if (sequence is ReadOnlyCollection<T> result)
            {
                return result;
            }

            return new ReadOnlyCollection<T>(sequence.ToArray());
        }

        internal static void SerializeData(this IEnumerable data, IDictionary<string, object> json, string schemaData, string schemaTotal)
        {
            if (string.IsNullOrEmpty(schemaData))
            {
                json["data"] = data;
                return;
            }

            json["data"] = new Dictionary<string, object>
        {
            { schemaData, data },
            {
                schemaTotal,
                data.AsQueryable().Count()
            }
        };
        }

        internal static bool Any(this IEnumerable data)
        {
            IEnumerator enumerator = data.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    _ = enumerator.Current;
                    return true;
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            return false;
        }
    }
}
