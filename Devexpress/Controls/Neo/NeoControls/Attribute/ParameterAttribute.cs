/********************************************************************
    created:	2022/6/13 15:45:24
    author:		rush
    email:		yacper@gmail.com	
	
    purpose:
    modifiers:	
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Api.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)] // 只能用于property
public class ParameterAttribute : Attribute
{
}