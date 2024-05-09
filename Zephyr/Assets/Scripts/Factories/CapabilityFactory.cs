using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CapabilityFactory<TCapability> : ScriptableObject where TCapability : ICapabilities
{

}
