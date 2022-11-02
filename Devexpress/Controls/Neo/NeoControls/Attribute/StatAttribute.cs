// created: 2022/07/13 16:44
// author:  rush
// email:   yacper@gmail.com
// 
// purpose:
// modifiers:代表用于显示的statistic

using System;
using System.ComponentModel.DataAnnotations;

namespace Neo.Api.Attributes;

[AttributeUsage(AttributeTargets.Property , AllowMultiple = true)] 
public class StatAttribute : Attribute
{
}

   