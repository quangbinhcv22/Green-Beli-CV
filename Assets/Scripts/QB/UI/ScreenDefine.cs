using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace QB.UI
{
    public sealed class ScreenDefine : MonoBehaviour
    {
#if UNITY_EDITOR
        

        public static List<string> ScreenIds;
        public static Type ScreenEnumType;

        public MonoScript screenEnum;


        private void OnValidate()
        {
            ScreenEnumType = Type.GetType(screenEnum.GetEnumTypeName());

            if (ScreenEnumType is null) return;
            ScreenIds = Enum.GetNames(ScreenEnumType).ToList();
        }
    }


    internal static class MonoScriptExtension
    {
        private static class CSharpSyntax
        {
            internal const string Namespace = "namespace";
            internal const string MemberAccessOperator = ".";
            internal const string Enum = "enum";
        }

        public static string GetEnumTypeName(this MonoScript monoScript)
        {
            string monoScriptContent;

            try
            {
                monoScriptContent = monoScript.ToString();
            }
            catch (UnassignedReferenceException)
            {
                return string.Empty;
            }

            var splitContent = monoScriptContent.Split().ToList();

            const int addIndexNext = 1;
            var namespaceName = splitContent[splitContent.FindIndex(content => content == CSharpSyntax.Namespace) + addIndexNext];
            var enumName = splitContent[splitContent.FindIndex(content => content == CSharpSyntax.Enum) + addIndexNext];

            return $"{(splitContent.Contains(CSharpSyntax.Namespace) ? namespaceName + CSharpSyntax.MemberAccessOperator : string.Empty)}{enumName}";
        }
#endif
    }
}