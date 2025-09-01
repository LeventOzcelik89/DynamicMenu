using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DynamicMenu.Web.Helper
{
    internal static class MemberAccessTokenizer
    {
        [CompilerGenerated]
        private sealed class _003CExtractIndexerArguments_003Ed__3 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
        {
            private int _003C_003E1__state;

            private string _003C_003E2__current;

            private int _003C_003El__initialThreadId;

            private string member;

            public string _003C_003E3__member;

            private string[] _003C_003E7__wrap1;

            private int _003C_003E7__wrap2;

            string IEnumerator<string>.Current
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
            public _003CExtractIndexerArguments_003Ed__3(int _003C_003E1__state)
            {
                this._003C_003E1__state = _003C_003E1__state;
                _003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                _003C_003E7__wrap1 = null;
                _003C_003E1__state = -2;
            }

            private bool MoveNext()
            {
                switch (_003C_003E1__state)
                {
                    default:
                        return false;
                    case 0:
                        {
                            _003C_003E1__state = -1;
                            string text = member.TrimEnd(']');
                            _003C_003E7__wrap1 = text.Split(',');
                            _003C_003E7__wrap2 = 0;
                            break;
                        }
                    case 1:
                        _003C_003E1__state = -1;
                        _003C_003E7__wrap2++;
                        break;
                }

                if (_003C_003E7__wrap2 < _003C_003E7__wrap1.Length)
                {
                    string text2 = _003C_003E7__wrap1[_003C_003E7__wrap2];
                    _003C_003E2__current = text2;
                    _003C_003E1__state = 1;
                    return true;
                }

                _003C_003E7__wrap1 = null;
                return false;
            }

            bool IEnumerator.MoveNext()
            {
                //ILSpy generated this explicit interface implementation from .override directive in MoveNext
                return this.MoveNext();
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator<string> IEnumerable<string>.GetEnumerator()
            {
                _003CExtractIndexerArguments_003Ed__3 _003CExtractIndexerArguments_003Ed__;
                if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
                {
                    _003C_003E1__state = 0;
                    _003CExtractIndexerArguments_003Ed__ = this;
                }
                else
                {
                    _003CExtractIndexerArguments_003Ed__ = new _003CExtractIndexerArguments_003Ed__3(0);
                }

                _003CExtractIndexerArguments_003Ed__.member = _003C_003E3__member;
                return _003CExtractIndexerArguments_003Ed__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<string>)this).GetEnumerator();
            }
        }

        [CompilerGenerated]
        private sealed class _003CGetTokens_003Ed__0 : IEnumerable<IMemberAccessToken>, IEnumerable, IEnumerator<IMemberAccessToken>, IEnumerator, IDisposable
        {
            private int _003C_003E1__state;

            private IMemberAccessToken _003C_003E2__current;

            private int _003C_003El__initialThreadId;

            private string memberPath;

            public string _003C_003E3__memberPath;

            private string[] _003C_003E7__wrap1;

            private int _003C_003E7__wrap2;

            IMemberAccessToken IEnumerator<IMemberAccessToken>.Current
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
            public _003CGetTokens_003Ed__0(int _003C_003E1__state)
            {
                this._003C_003E1__state = _003C_003E1__state;
                _003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                _003C_003E7__wrap1 = null;
                _003C_003E1__state = -2;
            }

            private bool MoveNext()
            {
                switch (_003C_003E1__state)
                {
                    default:
                        return false;
                    case 0:
                        {
                            _003C_003E1__state = -1;
                            string[] array = memberPath.Split(new char[2] { '.', '[' }, StringSplitOptions.RemoveEmptyEntries);
                            _003C_003E7__wrap1 = array;
                            _003C_003E7__wrap2 = 0;
                            break;
                        }
                    case 1:
                        _003C_003E1__state = -1;
                        goto IL_009c;
                    case 2:
                        {
                            _003C_003E1__state = -1;
                            goto IL_009c;
                        }

                    IL_009c:
                        _003C_003E7__wrap2++;
                        break;
                }

                if (_003C_003E7__wrap2 < _003C_003E7__wrap1.Length)
                {
                    string text = _003C_003E7__wrap1[_003C_003E7__wrap2];
                    if (TryParseIndexerToken(text, out var token))
                    {
                        _003C_003E2__current = token;
                        _003C_003E1__state = 1;
                        return true;
                    }

                    _003C_003E2__current = new PropertyToken(text);
                    _003C_003E1__state = 2;
                    return true;
                }

                _003C_003E7__wrap1 = null;
                return false;
            }

            bool IEnumerator.MoveNext()
            {
                //ILSpy generated this explicit interface implementation from .override directive in MoveNext
                return this.MoveNext();
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            IEnumerator<IMemberAccessToken> IEnumerable<IMemberAccessToken>.GetEnumerator()
            {
                _003CGetTokens_003Ed__0 _003CGetTokens_003Ed__;
                if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
                {
                    _003C_003E1__state = 0;
                    _003CGetTokens_003Ed__ = this;
                }
                else
                {
                    _003CGetTokens_003Ed__ = new _003CGetTokens_003Ed__0(0);
                }

                _003CGetTokens_003Ed__.memberPath = _003C_003E3__memberPath;
                return _003CGetTokens_003Ed__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<IMemberAccessToken>)this).GetEnumerator();
            }
        }

        [IteratorStateMachine(typeof(_003CGetTokens_003Ed__0))]
        public static IEnumerable<IMemberAccessToken> GetTokens(string memberPath)
        {
            //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
            return new _003CGetTokens_003Ed__0(-2)
            {
                _003C_003E3__memberPath = memberPath
            };
        }

        private static bool TryParseIndexerToken(string member, out IndexerToken token)
        {
            token = null;
            if (!IsValidIndexer(member))
            {
                return false;
            }

            List<object> list = new List<object>();
            list.AddRange(from a in ExtractIndexerArguments(member)
                          select ConvertIndexerArgument(a));
            token = new IndexerToken(list);
            return true;
        }

        private static bool IsValidIndexer(string member)
        {
            return member.EndsWith("]", StringComparison.Ordinal);
        }

        [IteratorStateMachine(typeof(_003CExtractIndexerArguments_003Ed__3))]
        private static IEnumerable<string> ExtractIndexerArguments(string member)
        {
            //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
            return new _003CExtractIndexerArguments_003Ed__3(-2)
            {
                _003C_003E3__member = member
            };
        }

        private static object ConvertIndexerArgument(string argument)
        {
            if (int.TryParse(argument, out var result))
            {
                return result;
            }

            if (argument.StartsWith("\"", StringComparison.Ordinal))
            {
                return argument.Trim('"');
            }

            if (argument.StartsWith("'", StringComparison.Ordinal))
            {
                string text = argument.Trim('\'');
                if (text.Length == 1)
                {
                    return text[0];
                }

                return text;
            }

            return argument;
        }
    }
}
