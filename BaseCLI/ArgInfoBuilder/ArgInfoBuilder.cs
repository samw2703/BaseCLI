using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BaseCLI
{
    public class ArgInfoBuilder<TArgs> where TArgs : new()
    {
        private readonly List<ArgInfo<TArgs>> _argInfos = new List<ArgInfo<TArgs>>();

        public SetArgInfoType<TArgs> Add(string flag, string friendlyName)
        {
            if (string.IsNullOrEmpty(flag))
                throw new ArgInfoBuilderError("Flag cannot be null or empty");

            if (DuplicateFlag(flag))
                throw new ArgInfoBuilderError($"Duplicate ArgInfos with flag {flag} registered");

            return new SetArgInfoType<TArgs>(flag, friendlyName, this);
        }

        internal void Add(ArgInfo<TArgs> argInfo) => _argInfos.Add(argInfo);

        public ArgInfoCollection<TArgs> Build() => new ArgInfoCollection<TArgs>(_argInfos);

        private bool DuplicateFlag(string flag) => _argInfos.Any(x => x.Flag == flag);
    }

    public class SetArgInfoType<TArgs> where TArgs : new()
    {
        private readonly string _flag;
        private readonly string _friendlyName;
        private readonly ArgInfoBuilder<TArgs> _builder;

        internal SetArgInfoType(string flag, string friendlyName, ArgInfoBuilder<TArgs> builder)
        {
            _flag = flag;
            _friendlyName = friendlyName;
            _builder = builder;
        }

        public ArgInfoBuilder<TArgs> ForString(Expression<Func<TArgs, string>> getProperty)
            => For(getProperty, propertyName => new StringArgInfo<TArgs>(_flag, _friendlyName, propertyName));

        public ArgInfoBuilder<TArgs> ForMandatoryString(Expression<Func<TArgs, string>> getProperty)
            => For(getProperty, propertyName => new StringArgInfo<TArgs>(_flag, _friendlyName, propertyName, true));

        public ArgInfoBuilder<TArgs> ForInt(Expression<Func<TArgs, int>> getProperty)
            => For(getProperty, propertyName => new IntArgInfo<TArgs>(_flag, _friendlyName, propertyName));

        public ArgInfoBuilder<TArgs> ForMandatoryInt(Expression<Func<TArgs, int>> getProperty)
            => For(getProperty, propertyName => new IntArgInfo<TArgs>(_flag, _friendlyName, propertyName, true));

        public ArgInfoBuilder<TArgs> ForStringCollection(Expression<Func<TArgs, List<string>>> getProperty)
            => For(getProperty, propertyName => new StringCollectionArgInfo<TArgs>(_flag, _friendlyName, propertyName));

        public ArgInfoBuilder<TArgs> ForMandatoryStringCollection(Expression<Func<TArgs, List<string>>> getProperty)
            => For(getProperty, propertyName => new StringCollectionArgInfo<TArgs>(_flag, _friendlyName, propertyName, true));

        public ArgInfoBuilder<TArgs> ForIntCollection(Expression<Func<TArgs, List<int>>> getProperty)
            => For(getProperty, propertyName => new IntCollectionArgInfo<TArgs>(_flag, _friendlyName, propertyName));

        public ArgInfoBuilder<TArgs> ForMandatoryIntCollection(Expression<Func<TArgs, List<int>>> getProperty)
            => For(getProperty, propertyName => new IntCollectionArgInfo<TArgs>(_flag, _friendlyName, propertyName, true));

        public ArgInfoBuilder<TArgs> ForBool(Expression<Func<TArgs, bool>> getProperty)
            => For(getProperty, propertyName => new BoolArgInfo<TArgs>(_flag, _friendlyName, propertyName));


        private ArgInfoBuilder<TArgs> For<T>(Expression<Func<TArgs, T>> getProperty, Func<string, ArgInfo<TArgs>> createArgInfo)
        {
            if (getProperty == null)
                throw new ArgInfoBuilderError("getProperty delegate cannot be null");

            var propertyName = GetPropertyName(getProperty);
            if (!PropertyHasSetter(propertyName))
                throw new ArgInfoBuilderError($"Property with name {propertyName} does not have a setter");

            _builder.Add(createArgInfo(propertyName));

            return _builder;
        }

        private string GetPropertyName<TProperty>(Expression<Func<TArgs, TProperty>> getProperty)
        {
            if (getProperty.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgInfoBuilderError("getProperty must select a property on your args");

            return ((MemberExpression)getProperty.Body).Member.Name;
        }

        private bool PropertyHasSetter(string name)
            => typeof(TArgs).GetRuntimeProperties().Single(x => x.Name == name).SetMethod != null;
    }
}