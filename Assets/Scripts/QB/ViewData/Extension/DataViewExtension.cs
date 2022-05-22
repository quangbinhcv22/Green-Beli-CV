using System.Linq;
using System.Reflection;

namespace QB.ViewData
{
    public static class DataViewExtension
    {
        public static object Get(this object obj, string memberPath)
        {
            return memberPath.Split('.').Aggregate(obj, (current, member) => current.GetNextMember(member));
        }

        private static object GetNextMember(this object obj, string memberName)
        {
            var member = FindMember(memberName);

            return member.MemberType switch
            {
                MemberTypes.Field => ((FieldInfo) member).GetValue(obj),
                MemberTypes.Property => ((PropertyInfo) member).GetValue(obj),
                MemberTypes.Method => ((MethodInfo) member).Invoke(obj, null),
                _ => null,
            };

            MemberInfo FindMember(string name)
            {
                foreach (var member in obj.GetType().GetMembers())
                {
                    foreach (var attribute in member.GetCustomAttributes())
                    {
                        if (attribute is DataViewAttribute dateView)
                        {
                            if (dateView.Name == name) return member;
                        }
                    }
                }

                var queryMembers = obj.GetType().GetMember(memberName);
                return queryMembers.Any()
                    ? queryMembers.First()
                    : throw new UnableAccessDataViewException($"<color=yellow>{obj.GetType()}</color>: make sure <color=yellow>{memberName}</color> exists or is accessible");
            }
        }
    }
}