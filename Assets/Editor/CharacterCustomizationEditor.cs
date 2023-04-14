using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


    //Set this editor in the inspector as the editor for CharacterCustomization class. So when i add the CharacterCustomization script as
    //a component of an object it runs and displays this editor
    [CustomEditor(typeof(CharacterCustomization))]
    public class CharacterCustomizationEditor : Editor
    {

        private int blendshapeSelectedIndex = 0;
        private CharacterCustomization characterCustomization;

        public Canvas canvas;

        //Function that overrides the OnInspectorGUI() from the base Editor class that CharacterCustomizationEditor inherits
        public override void OnInspectorGUI()
        {
            //Calls the original OnInspectorGUI() from the base class (Editor) and so it displays the fields from CharacterCustomization
            //class, which are hidden without it, because it only runs the overriden OnInspectorGUI(). 
            //It does not replace the overriden fuction, it just runs the original function along with the overriden and it displays 
            //the original below the overriden one, so you get both the fields from the overriden function in the inspector and the 
            //original function runs and displays the fields from the CharacterCustomization class.
            base.OnInspectorGUI();

            //Three spaces between the base.OnInspectorGUI() fields and the overriden OnInspectorGUI() fields
            EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

            //target derives from Editor class and is the current object that is being inspected
            characterCustomization = (CharacterCustomization)target;

            //If no target, then don't show anything except the below message in the inspector in bold letters
            if (characterCustomization.target == null)
            {
                EditorGUILayout.LabelField("PLEASE SET A TARGET WITH BLENDSHAPES!", EditorStyles.boldLabel);
                return;
            }

            EditorGUILayout.LabelField("CREATE SLIDER", EditorStyles.boldLabel);


            //If target has been changed then clear blendshapes database in order to update to new target with the next if statement below
            if (characterCustomization.DoesTargetMatchSkmr())
                characterCustomization.ClearDatabase();

            //Initialize Blendshapes and get from database
            if (characterCustomization.GetNumberOfEntries() <= 0)
                characterCustomization.Initialize();

            string[] blendShapeNames = characterCustomization.GetBlendShapeNames();

            //Check if there were any blendshapes on the target Object
            if (blendShapeNames.Length <= 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("NO BLENDSHAPES DETECTED ON THIS TARGET!", EditorStyles.boldLabel);
                characterCustomization.ClearDatabase();
                return;
            }

            //Creates a dropdown menu in the inspector, with BlendShapeName as a label, shapeBlendSelectedIndex the default 
            //selected index and an array of strings that have the blendShapeNames and it then returns the selected index
            blendshapeSelectedIndex = EditorGUILayout.Popup("BlendShapeName", blendshapeSelectedIndex, blendShapeNames);

            //Manual Canvas selector. Creates object field in inspector to add manually the canvas if you want, in case there are many
            canvas = EditorGUILayout.ObjectField("Manual Canvas Selection:", canvas, typeof(Canvas), true) as Canvas;

            if (GUILayout.Button("Create Slider"))
            {
                //Auto Find one if canvas is null
                if (canvas == null)
                {
                    canvas = GameObject.FindObjectOfType<Canvas>();

                    //If canvas doesn't exist, throw exception
                    if (canvas == null)
                    {
                        throw new System.Exception("Please add a canvas into your scene!");
                    }

                }
                

                //Instantiate Slider from root Resource folder within path Resources/"Blendshape Slider"
                GameObject sliderPrefab = Instantiate(Resources.Load("Blendshape Slider", typeof(GameObject))) as GameObject;

                //Get preset component from prefab slider 
                BlendShapeSlider BShapeSlider = sliderPrefab.GetComponent<BlendShapeSlider>();
                //Fill in the name of the selected Blendshape Name. Returns array with the names and you get the name from the  selected index
                BShapeSlider.blendShapeName = characterCustomization.GetBlendShapeNames()[blendshapeSelectedIndex]; 
                //Parent slider to canvas
                BShapeSlider.transform.parent = canvas.transform;
                //Set slider game object name
                BShapeSlider.name = "Slider " + BShapeSlider.blendShapeName;
                //Change the Label text for the blendshape from the component in the children object of the slider
                BShapeSlider.transform.GetComponentInChildren<Text>().text = BShapeSlider.blendShapeName;   
                //Set default dimensions for the slider
                BShapeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(240f, 30f);

                //Get Blendshape wrapper
                Blendshape blendShape = characterCustomization.GetBlendshape(BShapeSlider.blendShapeName);

                //Slider Component Properties
                Slider slider = BShapeSlider.GetComponent<Slider>();

                //Set slider's min and max value based on the blendshape
                if (blendShape.negativeIndex == -1)                
                    slider.minValue = 0f;
                else if (blendShape.positiveIndex == -1)
                    slider.maxValue = 0f;
                else
                    throw new System.Exception("Blendshape doesn't exist!");

                //Set default slider value to 0
                slider.value = 0f;
            }

        }
    } 

