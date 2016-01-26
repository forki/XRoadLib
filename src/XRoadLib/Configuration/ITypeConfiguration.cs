﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace XRoadLib.Configuration
{
    public interface ITypeConfiguration
    {
        bool? GetPropertyIsNullable(PropertyInfo propertyInfo, bool isArrayItem);

        string GetPropertyName(PropertyInfo propertyInfo);

        XName GetTypeName(Type type);

        XRoadContentLayoutMode GetContentLayoutMode(Type type);

        IComparer<PropertyInfo> GetPropertyComparer(Type type);
    }
}