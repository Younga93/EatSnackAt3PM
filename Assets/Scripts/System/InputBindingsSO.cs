using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputBindings", menuName = "ScriptableObjects/InputBindings")]
public class InputBindingsSO : ScriptableObject
{
    public string jumpBindingPath = "<Keyboard>/w";
    public string slideBindingPath = "<Keyboard>/s";
    public string attackBindingPath = "<Keyboard>/d";
}
