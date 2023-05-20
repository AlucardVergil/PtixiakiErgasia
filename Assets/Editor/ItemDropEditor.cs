using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;


//Set this editor in the inspector as the editor for ItemDrop class. So when i add the ItemDrop script as
//a component of an object it runs and displays this editor
[CustomEditor(typeof(ItemDrop))] 
public class ItemDropEditor : Editor
{
    private ItemDrop itemDrop;  

    //Function that overrides the OnInspectorGUI() from the base Editor class that ItemDropEditor inherits
    public override void OnInspectorGUI()
    {
        //target derives from Editor class and is the current object that is being inspected, meaning the selected ItemDrop object
        itemDrop = (ItemDrop)target;

        base.OnInspectorGUI();

        //Three spaces between the base.OnInspectorGUI() fields and the overriden OnInspectorGUI() fields
        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

        switch (itemDrop.itemType)
        {
            case ItemDrop.ItemType.Resource: 
                GUI_Resource();
                break;
            case ItemDrop.ItemType.Consumable:
                GUI_Consumable();
                break;
            case ItemDrop.ItemType.Equipment: 
                GUI_Equipment();
                break;
            case ItemDrop.ItemType.Craftable:
                GUI_Craftable();
                break;
        }

    }


    private void GUI_Consumable()
    {
        EditorGUILayout.LabelField("Consumable Specific Options", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        string[] effects = new string[]
        {
            "Restores Health",
            "Restores Hunger",
            "Restores Thirst"
        };

        itemDrop.effectsDropdownIndex = EditorGUILayout.Popup("Consumable Effect", itemDrop.effectsDropdownIndex, effects);
        itemDrop.effectsValue = EditorGUILayout.IntField("Restoration Effect Value", itemDrop.effectsValue);

    }



    private void GUI_Resource()
    {
        EditorGUILayout.LabelField("Resource Specific Options", EditorStyles.boldLabel);
        EditorGUILayout.Space();
    }


    private void GUI_Equipment()
    {
        EditorGUILayout.LabelField("Equipment Specific Options", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        itemDrop.attackPower = EditorGUILayout.IntField("Attack Power", itemDrop.attackPower);
        itemDrop.defensePower = EditorGUILayout.IntField("Defense Power", itemDrop.defensePower);
    }


    private void GUI_Craftable()
    {
        EditorGUILayout.LabelField("Craftable Specific Options", EditorStyles.boldLabel);
        EditorGUILayout.Space();
    }



} 

