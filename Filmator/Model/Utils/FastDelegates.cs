using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Filmator.Model.Utils {
    class FastDelegates {
        public delegate object LateBoundMethod(object target, object[] arguments);

        public static class DelegateFactory {
            public static LateBoundMethod Create(MethodInfo method) {
                var instanceParameter = Expression.Parameter(typeof(object), "target");
                var argumentsParameter = Expression.Parameter(typeof(object[]), "arguments");

                var call = Expression.Call(
                  Expression.Convert(instanceParameter, method.DeclaringType),
                  method,
                  CreateParameterExpressions(method, argumentsParameter));

                var lambda = Expression.Lambda<LateBoundMethod>(
                  Expression.Convert(call, typeof(object)),
                  instanceParameter,
                  argumentsParameter);

                return lambda.Compile();
            }

            private static Expression[] CreateParameterExpressions(MethodInfo method, Expression argumentsParameter) {
                return method.GetParameters().Select((parameter, index) =>
                  Expression.Convert(
                    Expression.ArrayIndex(argumentsParameter, Expression.Constant(index)), parameter.ParameterType)).ToArray();
            }
        }
    }
}
