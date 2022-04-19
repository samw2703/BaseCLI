using System;
using System.Linq.Expressions;
using SimpleCLI.Command;

namespace SimpleCLI.ArgInfoBuilder
{
    public static class ArgInfo
    {
        public static SetPropertyName<TArgs> Create<TArgs>(string flag, string friendlyName) where TArgs : new()
            => new(flag, friendlyName);

        public class SetPropertyName<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;

            internal SetPropertyName(string flag, string friendlyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
            }

            public SetArgInfoType<TArgs> For<TProperty>(Expression<Func<TArgs, TProperty>> getProperty)
            {
                return new SetArgInfoType<TArgs>(_flag, _friendlyName, GetPropertyName(getProperty));
            }

            private string GetPropertyName<TProperty>(Expression<Func<TArgs, TProperty>> getProperty)
            {
                if (getProperty.Body.NodeType != ExpressionType.MemberAccess)
                    throw new Exception("will test this at some point");

                return ((MemberExpression)getProperty.Body).Member.Name;
            }
        }

        public class SetArgInfoType<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;
            private readonly string _propertyName;

            internal SetArgInfoType(string flag, string friendlyName, string propertyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
                _propertyName = propertyName;
            }

            public StringArgInfoBuilder<TArgs> String() => new(_flag, _friendlyName, _propertyName);
            public IntArgInfoBuilder<TArgs> Int() => new(_flag, _friendlyName, _propertyName);
            public StringCollectionArgInfoBuilder<TArgs> StringCollection() => new(_flag, _friendlyName, _propertyName);
            public IntCollectionArgInfoBuilder<TArgs> IntCollection() => new(_flag, _friendlyName, _propertyName);
            public BoolArgInfoBuilder<TArgs> Bool() => new(_flag, _friendlyName, _propertyName);
        }

        public class StringArgInfoBuilder<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;
            private readonly string _propertyName;

            internal StringArgInfoBuilder(string flag, string friendlyName, string propertyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
                _propertyName = propertyName;
            }

            public StringArgInfo<TArgs> Build() => new(_flag, _friendlyName, _propertyName);
            public StringArgInfo<TArgs> BuildMandatory() => new(_flag, _friendlyName, _propertyName, true);
        }

        public class IntArgInfoBuilder<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;
            private readonly string _propertyName;

            internal IntArgInfoBuilder(string flag, string friendlyName, string propertyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
                _propertyName = propertyName;
            }

            public IntArgInfo<TArgs> Build() => new(_flag, _friendlyName, _propertyName);
            public IntArgInfo<TArgs> BuildMandatory() => new(_flag, _friendlyName, _propertyName, true);
        }

        public class StringCollectionArgInfoBuilder<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;
            private readonly string _propertyName;

            internal StringCollectionArgInfoBuilder(string flag, string friendlyName, string propertyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
                _propertyName = propertyName;
            }

            public StringCollectionArgInfo<TArgs> Build() => new(_flag, _friendlyName, _propertyName);
            public StringCollectionArgInfo<TArgs> BuildMandatory() => new(_flag, _friendlyName, _propertyName, true);
        }

        public class IntCollectionArgInfoBuilder<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;
            private readonly string _propertyName;

            internal IntCollectionArgInfoBuilder(string flag, string friendlyName, string propertyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
                _propertyName = propertyName;
            }

            public IntCollectionArgInfo<TArgs> Build() => new(_flag, _friendlyName, _propertyName);
            public IntCollectionArgInfo<TArgs> BuildMandatory() => new(_flag, _friendlyName, _propertyName, true);
        }

        public class BoolArgInfoBuilder<TArgs> where TArgs : new()
        {
            private readonly string _flag;
            private readonly string _friendlyName;
            private readonly string _propertyName;

            internal BoolArgInfoBuilder(string flag, string friendlyName, string propertyName)
            {
                _flag = flag;
                _friendlyName = friendlyName;
                _propertyName = propertyName;
            }

            public BoolArgInfo<TArgs> Build() => new(_flag, _friendlyName, _propertyName);
        }
    }
}
