using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleCLI.ArgInfoBuilder;

namespace SimpleCLI
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
                if (getProperty == null)
                    throw new ArgInfoBuilderError("getProperty delegate cannot be null");

                return new SetArgInfoType<TArgs>(_flag, _friendlyName, GetPropertyName(getProperty));
            }

            private string GetPropertyName<TProperty>(Expression<Func<TArgs, TProperty>> getProperty)
            {
                if (getProperty.Body.NodeType != ExpressionType.MemberAccess)
                    throw new ArgInfoBuilderError("getProperty must select a property on your args");

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

                if (!PropertyExists(propertyName))
                    throw new ArgInfoBuilderError($"No property with name {propertyName} does not exist");

                if (!PropertyHasSetter(propertyName))
                    throw new ArgInfoBuilderError($"Property with name {propertyName} does not have a setter");

                _propertyName = propertyName;
            }

            public StringArgInfoBuilder<TArgs> String()
            {
                ValidatePropertyNameIsOfType<string>();

                return new(_flag, _friendlyName, _propertyName);
            }
            public IntArgInfoBuilder<TArgs> Int()
            {
                ValidatePropertyNameIsOfType<int>();

                return new(_flag, _friendlyName, _propertyName);
            }

            public StringCollectionArgInfoBuilder<TArgs> StringCollection()
            {
                ValidatePropertyNameIsOfType<List<string>>();

                return new(_flag, _friendlyName, _propertyName);
            }

            public IntCollectionArgInfoBuilder<TArgs> IntCollection()
            {
                ValidatePropertyNameIsOfType<List<int>>();

                return new(_flag, _friendlyName, _propertyName);
            }
            public BoolArgInfoBuilder<TArgs> Bool()
            {
                ValidatePropertyNameIsOfType<bool>();

                return new(_flag, _friendlyName, _propertyName);
            }

            private void ValidatePropertyNameIsOfType<TType>()
            {
                if (typeof(TArgs).GetRuntimeProperties().Single(x => x.Name == _propertyName).PropertyType == typeof(TType))
                    return;

                throw new ArgInfoBuilderError($"Property {_propertyName} should be of type {typeof(TType).FullName}");
            }

            private bool PropertyExists(string name)
                => typeof(TArgs).GetRuntimeProperties().SingleOrDefault(x => x.Name == name) != null;

            private bool PropertyHasSetter(string name)
                => typeof(TArgs).GetRuntimeProperties().Single(x => x.Name == name).SetMethod != null;
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
