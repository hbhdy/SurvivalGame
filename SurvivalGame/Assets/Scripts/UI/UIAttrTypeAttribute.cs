using System;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.All)]
public class UIAttrTypeAttribute : Attribute
{
    public string ui_resource_name { get; private set; } = string.Empty;

    public UIAttrTypeAttribute(string resource_name)
    {
        ui_resource_name = resource_name;
    }
}

public class UIAttrUtil
{
    static T GetTypeAttribute<T>(Type type, string enumString)
    {
        System.Reflection.MemberInfo[] infos = type.GetMember(enumString);
        if (infos.Length == 0)
            return default;

        object[] attributes = infos[0].GetCustomAttributes(typeof(T), false);
        if (attributes.Length == 0)
            return default;

        return (T)attributes[0];
    }

    public static string GetUIAttributeResourceName<T>(T t)
    {
        UIAttrTypeAttribute customAttr = GetTypeAttribute<UIAttrTypeAttribute>(t.GetType(), t.ToString());
        return customAttr == null ? string.Empty : customAttr.ui_resource_name;
    }
}
