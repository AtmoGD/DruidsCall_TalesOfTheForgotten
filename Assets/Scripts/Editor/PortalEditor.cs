using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(Portal))]
public class PortalEditor : SavePointEditor
{
}

#endif