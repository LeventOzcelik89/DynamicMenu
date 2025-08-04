using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace DynamicMenu.Web.Helper
{
    internal class EnumerableSelectorAggregateFunctionExpressionBuilder : AggregateFunctionExpressionBuilderBase
    {
        [CompilerGenerated]
        private sealed class _003CGetMethodArgumentsTypes_003Ed__6 : IEnumerable<Type>, IEnumerable, IEnumerator<Type>, IEnumerator, IDisposable
        {
            private int _003C_003E1__state;

            private Type _003C_003E2__current;

            private int _003C_003El__initialThreadId;

            public EnumerableSelectorAggregateFunctionExpressionBuilder _003C_003E4__this;

            private LambdaExpression memberSelectorExpression;

            public LambdaExpression _003C_003E3__memberSelectorExpression;

            Type IEnumerator<Type>.Current
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
            public _003CGetMethodArgumentsTypes_003Ed__6(int _003C_003E1__state)
            {
                this._003C_003E1__state = _003C_003E1__state;
                _003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                _003C_003E1__state = -2;
            }

            private bool MoveNext()
            {
                int num = _003C_003E1__state;
                EnumerableSelectorAggregateFunctionExpressionBuilder enumerableSelectorAggregateFunctionExpressionBuilder = _003C_003E4__this;
                switch (num)
                {
                    default:
                        return false;
                    case 0:
                        _003C_003E1__state = -1;
                        _003C_003E2__current = enumerableSelectorAggregateFunctionExpressionBuilder.ItemType;
                        _003C_003E1__state = 1;
                        return true;
                    case 1:
                        _003C_003E1__state = -1;
                        if (!memberSelectorExpression.Body.Type.IsNumericType())
                        {
                            _003C_003E2__current = memberSelectorExpression.Body.Type;
                            _003C_003E1__state = 2;
                            return true;
                        }

                        break;
                    case 2:
                        _003C_003E1__state = -1;
                        break;
                }

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
            IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
            {
                _003CGetMethodArgumentsTypes_003Ed__6 _003CGetMethodArgumentsTypes_003Ed__;
                if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
                {
                    _003C_003E1__state = 0;
                    _003CGetMethodArgumentsTypes_003Ed__ = this;
                }
                else
                {
                    _003CGetMethodArgumentsTypes_003Ed__ = new _003CGetMethodArgumentsTypes_003Ed__6(0)
                    {
                        _003C_003E4__this = _003C_003E4__this
                    };
                }

                _003CGetMethodArgumentsTypes_003Ed__.memberSelectorExpression = _003C_003E3__memberSelectorExpression;
                return _003CGetMethodArgumentsTypes_003Ed__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<Type>)this).GetEnumerator();
            }
        }

        protected new EnumerableSelectorAggregateFunction Function => (EnumerableSelectorAggregateFunction)base.Function;

        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Provided enumerableExpression's System.Linq.Expressions.Expression.Type is not
        //     System.Collections.Generic.IEnumerable`1
        public EnumerableSelectorAggregateFunctionExpressionBuilder(Expression enumerableExpression, EnumerableSelectorAggregateFunction function)
            : base(enumerableExpression, function)
        {
        }

        public override Expression CreateAggregateExpression()
        {
            LambdaExpression memberSelectorExpression = CreateMemberSelectorExpression();
            return CreateMethodCallExpression(memberSelectorExpression);
        }

        private LambdaExpression CreateMemberSelectorExpression()
        {
            MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = ExpressionBuilderFactory.MemberAccess(base.ItemType, null, Function.SourceField);
            memberAccessExpressionBuilderBase.Options.CopyFrom(base.Options);
            Expression memberExpression = memberAccessExpressionBuilderBase.CreateMemberAccessExpression();
            memberExpression = ConvertMemberAccessExpression(memberExpression);
            return Expression.Lambda(memberExpression, memberAccessExpressionBuilderBase.ParameterExpression);
        }

        private Expression CreateMethodCallExpression(LambdaExpression memberSelectorExpression)
        {
            IEnumerable<Type> methodArgumentsTypes = GetMethodArgumentsTypes(memberSelectorExpression);
            return Expression.Call(Function.ExtensionMethodsType, Function.AggregateMethodName, methodArgumentsTypes.ToArray(), base.EnumerableExpression, memberSelectorExpression);
        }

        [IteratorStateMachine(typeof(_003CGetMethodArgumentsTypes_003Ed__6))]
        private IEnumerable<Type> GetMethodArgumentsTypes(LambdaExpression memberSelectorExpression)
        {
            //yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
            return new _003CGetMethodArgumentsTypes_003Ed__6(-2)
            {
                _003C_003E4__this = this,
                _003C_003E3__memberSelectorExpression = memberSelectorExpression
            };
        }

        private Expression ConvertMemberAccessExpression(Expression memberExpression)
        {
            if (base.ItemType.IsDynamicObject() && Function.MemberType != null)
            {
                memberExpression = Expression.Convert(memberExpression, Function.MemberType);
            }

            if (base.ItemType == typeof(DataRowView) && Function.MemberType != null)
            {
                UnaryExpression ifFalse = Expression.Convert(memberExpression, Function.MemberType);
                ConstantExpression right = Expression.Constant(null, memberExpression.Type);
                memberExpression = Expression.Condition(Expression.Equal(memberExpression, right), Expression.Default(Function.MemberType), ifFalse);
            }

            if (ShouldConvertTypeToInteger(memberExpression.Type.GetNonNullableType()))
            {
                memberExpression = ConvertMemberExpressionToInteger(memberExpression);
            }

            return memberExpression;
        }

        private static Expression ConvertMemberExpressionToInteger(Expression expression)
        {
            Type type = (expression.Type.IsNullableType() ? typeof(int?) : typeof(int));
            return Expression.Convert(expression, type);
        }

        private static bool ShouldConvertTypeToInteger(Type type)
        {
            if (!(type == typeof(sbyte)) && !(type == typeof(short)) && !(type == typeof(byte)))
            {
                return type == typeof(ushort);
            }

            return true;
        }
    }
}
