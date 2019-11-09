using System;
using System.Collections.Generic;
using System.Linq;
using NetLogDNA.LogDnaApi.Dto;

namespace NetLogDNA.Test.Builders
{
    public class MetaBuilder
    {
        public static MetaBuilder New => new MetaBuilder();
        
        private IList<string> _tags;

        private IDictionary<object, object> _properties;

        private ExceptionInfo _exceptionInfo;

        private MetaBuilder()
        {
            _exceptionInfo = new ExceptionInfo()
            {
                Message = "Rebel Attack",
                Name = typeof(Exception).Name,
                StackTrace = "..."
            };
            
            _tags = new List<string> { "defense" };
            
            _properties = new Dictionary<object, object>()
            {
                { "reason", "attack" }
            };
        }

        public MetaBuilder WithTags(params string[] tags)
        {
            _tags = tags?.ToList();
            return this;
        }
        
        public MetaBuilder WithProperties(IDictionary<object, object> properties)
        {
            _properties = properties;
            return this;
        }

        public MetaBuilder WithInnerException(Exception innerException)
        {
            _exceptionInfo ??= new ExceptionInfo();
            _exceptionInfo.InnerMessage = innerException?.Message;
            _exceptionInfo.InnerStackTrace = innerException?.StackTrace;
            _exceptionInfo.InnerMessage = innerException?.GetType().Name;
            
            return this;
        }
        
        public MetaBuilder WithException(Exception exception)
        {
            _exceptionInfo ??= new ExceptionInfo();
            _exceptionInfo.Message = exception?.Message;
            _exceptionInfo.StackTrace = exception?.StackTrace;
            _exceptionInfo.Message = exception?.GetType().Name;
            
            return this;
        }

        public MetaBuilder WithoutException()
        {
            _exceptionInfo = null;
            return this;
        }
        
        public Meta Build()
        {
            return new Meta()
            {
                Tags = _tags,
                Properties = _properties,
                Exception = _exceptionInfo
            };
        }
    }
}