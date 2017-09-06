using System;

namespace FacebookTimelineSnapshot.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FacebookRequestPropertyAttribute : Attribute
    {
        public string Name { get; }

        public FacebookRequestPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}
