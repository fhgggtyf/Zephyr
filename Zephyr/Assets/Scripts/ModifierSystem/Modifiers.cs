using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers<TModifierType, TValueType> where TModifierType : Modifier<TValueType>
{
    private readonly List<TModifierType> modifierList = new List<TModifierType>();

    /*
     * Runs through the modifierList and applies each modifier to the input value. Note that the output of the first modifier is used as the input of the next
     * modifier. This is not a smart system but works for our use case. Better systems might allow modifiers to be sorted first based on some property.
     */
    public TValueType ApplyAllModifiers(TValueType initialValue)
    {
        var modifiedValue = initialValue;

        foreach (var modifier in modifierList)
        {
            modifiedValue = modifier.ModifyValue(modifiedValue);
        }

        return modifiedValue;
    }

    public void AddModifier(TModifierType modifier) => modifierList.Add(modifier);

    public void RemoveModifier(TModifierType modifier) => modifierList.Remove(modifier);
}