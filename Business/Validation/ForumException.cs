using System;
using System.Runtime.Serialization;

namespace Business.Validation
{
    [Serializable]
    public class ForumException:Exception
    {
        public ForumException()
        {
        }

        public ForumException(string message)
            : base(message)
        {
        }

        public ForumException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected ForumException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
