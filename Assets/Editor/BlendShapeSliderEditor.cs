using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


    //Set this editor in the inspector as the editor for BlendShapeSlider class. So when i add the BlendShapeSlider script as
    //a component of an object it runs and displays this editor
    [CustomEditor(typeof(BlendShapeSlider))] 
    public class BlendShapeSliderEditor : Editor
    {

        public enum State 
        { 
            auto, 
            manual 
        }

        public State state;
        private BlendShapeSlider blendShapeSlider;

        //Function that overrides the OnInspectorGUI() from the base Editor class that BlendShapeSliderEditor inherits
        public override void OnInspectorGUI() 
        {
            //Start of Editor GUI Layout group. Groups together fields in a horizontal layout between EditorGUILayout.BeginHorizontal
            //and EditorGUILayout.EndHorizontal
            EditorGUILayout.BeginHorizontal();
            Debug.Log("test");
            //Creates 2 buttons and when they are pressed they return true and change the enum state to auto or manual
            //and then run the GUI_Auto() or GUI_Manual() respectively
            if (GUILayout.Button("Auto")) 
                state = State.auto;
            if (GUILayout.Button("Manual")) 
                state = State.manual;

            EditorGUILayout.EndHorizontal(); //End of Editor GUI Layout group.

            //target derives from Editor class and is the current object that is being inspected, meaning the selected slider from the scene.
            blendShapeSlider = (BlendShapeSlider)target;

            switch (state)
            {
                case State.auto: //This case isn't necessary because of default case, but i still included it to be more clear when reading it
                    GUI_Auto(); 
                    break;
                case State.manual: 
                    GUI_Manual(); 
                    break;
                default: 
                    GUI_Auto(); 
                    break;
            }


        }

        //This is in case you want to manually type the name of the blendshape if there are too many in the dropdown menu to look through
        private void GUI_Manual()
        {
            //Calls the original OnInspectorGUI() from the base class (Editor) and so it displays the fields from BlendShapeSlider class,
            //which are hidden without it, because it only runs the overriden OnInspectorGUI(). 
            //It does not replace the overriden fuction, it just runs the original function along with the overriden and it displays 
            //the original below the overriden one, so you get both the fields from the overriden function in the inspector and the 
            //original function runs and displays the fields from the BlendShapeSlider class.
            base.OnInspectorGUI(); 
        }

        //This is to show a dropdown of the blendshapes to select from
        private void GUI_Auto()
        {
            //Find CharacterCustomization in the Scene
            //Get Dictionary
            //Display List of keys as options for popup

            //Get characterCustomization object from the scene
            CharacterCustomization characterCustomization = GameObject.FindObjectOfType<CharacterCustomization>();

            //Checks if it exists and displays a message in the inspector as well as an exception
            if (characterCustomization == null)
            {
                EditorGUILayout.LabelField("Please have the CharacterCustomizer in your scene!");
                throw new System.Exception("Please have the CharacterCustomizer in your scene!");
            }

            //Gets the number of blendshapes without the suffixes (Meaning max and min are treated as one)
            //from the dictionary and checks if it is filled
            //If the dictionary is empty, it parses the blendshapes into the dictionary
            if (characterCustomization.GetNumberOfEntries() <= 0)
                characterCustomization.Initialize();

            string[] blendShapeNames = characterCustomization.GetBlendShapeNames();//Get the names of the blendshapes without the suffixes

            //if they don't exist it throws an exception
            if (blendShapeNames.Length <= 0)
                throw new System.Exception("Dictionary Amount is 0 !?");

            //Used to check what the manual is set to, of order of dictionary.
            //The current selectedIndex from the current blendshape UI slider
            int blendShapeID = 0; 

            for (int i = 0; i < blendShapeNames.Length; i++)//Loops through all blendshapes
                //Checks if the blendshape name that it gets from the current blendShapeSlider, inside the inspector,
                //exists in the Blendshape Database and if it does it assigns its index to the blendShapeID variable
                if (blendShapeSlider.blendShapeName == blendShapeNames[i])
                    blendShapeID = i;

            //Creates a dropdown menu in the inspector, with BlendShapeName as a label, blendShapeID the default selected index
            //and an array of strings that have the blendShapeNames and it then returns the selected index
            blendShapeID = EditorGUILayout.Popup("BlendShapeName", blendShapeID, blendShapeNames);

            //Sets the blendShapeName field from the blendShapeSlider class(that you can only see when you switch to manual),
            //to match the new selected blendshape name from the dropdown menu in the GUI auto layout
            blendShapeSlider.blendShapeName = blendShapeNames[blendShapeID];

        }

    } 

