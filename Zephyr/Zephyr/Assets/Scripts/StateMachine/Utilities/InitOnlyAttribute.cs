using System;
using UnityEngine;

namespace Zephyr.StateMachine
{
	[AttributeUsage(AttributeTargets.Field)]
	public class InitOnlyAttribute : PropertyAttribute { }
}
