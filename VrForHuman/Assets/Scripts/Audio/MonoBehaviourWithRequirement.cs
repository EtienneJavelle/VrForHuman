using System;
using UnityEngine;

public abstract class MonoBehaviourWithRequirement : MonoBehaviour {
    public virtual Type GetAttibute() {
        Type type = GetType();
        if(type == null) {
            Debug.LogError("Cant Get Type", this);
            return null;
        }
        RequirementAttribute attribute = Attribute.GetCustomAttribute(type, typeof(RequirementAttribute), true) as RequirementAttribute;
        if(attribute == null) {
            Debug.LogError("The attribute was not found.");
            return null;
        } else {
            Debug.Log(attribute.InspectedType);
            return attribute.InspectedType;
        }
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class RequirementAttribute : Attribute {
    public Type InspectedType => inspectedType;
    private Type inspectedType;

    public RequirementAttribute(Type inspectedType) {
        if((object)inspectedType == null) {
            Debug.LogError("Failed to load Requirement inspected type");
        }

        this.inspectedType = inspectedType;
    }
}