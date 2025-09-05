using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlaceDomino))]
public class PlacerEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        // Draw the default inspector fields
        DrawDefaultInspector();

        EditorGUILayout.LabelField("Place Dominoes", EditorStyles.boldLabel);
        if (GUILayout.Button("Generate Dominoes")) 
        {
            PlaceDomino generator = (PlaceDomino)target;
            generator.GenerateDominoes();
        } 

        if (GUILayout.Button("Clear Dominoes"))
        {
            PlaceDomino generator = (PlaceDomino)target;
            // Clear all dominoes by destroying them
            List<GameObject> toBeDeleted = new List<GameObject>();
            for (int i = generator.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = generator.transform.GetChild(i).gameObject;
                if (child != null && (child.name.Contains("Domino") || child.name.Contains("Starter"))) // Assuming dominoes are named with "Domino"
                {
                    toBeDeleted.Add(child);
                }
            }
            for (int i = 0; i < toBeDeleted.Count; i++)
            {
                DestroyImmediate(toBeDeleted[i]);
            }
        }
    }
}
