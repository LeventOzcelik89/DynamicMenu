using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DynamicMenu.Web.Helper
{
    internal class DataTableWrapper : IEnumerable<DataRowView>, IEnumerable
    {
        [CompilerGenerated]
        private sealed class _003CGetEnumerator_003Ed__5 : IEnumerator<DataRowView>, IEnumerator, IDisposable
        {
            private int _003C_003E1__state;

            private DataRowView _003C_003E2__current;

            public DataTableWrapper _003C_003E4__this;

            private IEnumerator _003C_003E7__wrap1;

            DataRowView IEnumerator<DataRowView>.Current
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
            public _003CGetEnumerator_003Ed__5(int _003C_003E1__state)
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
                    DataTableWrapper dataTableWrapper = _003C_003E4__this;
                    switch (num)
                    {
                        default:
                            return false;
                        case 0:
                            _003C_003E1__state = -1;
                            if (dataTableWrapper.Table == null)
                            {
                                return false;
                            }

                            _003C_003E7__wrap1 = dataTableWrapper.Table.Rows.GetEnumerator();
                            _003C_003E1__state = -3;
                            break;
                        case 1:
                            _003C_003E1__state = -3;
                            break;
                    }

                    if (_003C_003E7__wrap1.MoveNext())
                    {
                        DataRow row = (DataRow)_003C_003E7__wrap1.Current;
                        _003C_003E2__current = dataTableWrapper.Table.DefaultView[dataTableWrapper.Table.Rows.IndexOf(row)];
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

        public DataTable Table { get; private set; }

        internal DataTableWrapper(DataTable dataTable)
        {
            Table = dataTable;
        }

        [IteratorStateMachine(typeof(_003CGetEnumerator_003Ed__5))]
        public IEnumerator<DataRowView> GetEnumerator()
        {
            //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
            return new _003CGetEnumerator_003Ed__5(0)
            {
                _003C_003E4__this = this
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
