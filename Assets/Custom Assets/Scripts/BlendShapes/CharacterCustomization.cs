using UnityEngine;
using System.Collections.Generic;
using System.Linq;


    //Implements Singleton. Passes CharacterCustomization class to the Singleton with Singleton<CharacterCustomization> .
    //It implements the Singleton because i only want one instance of the CharacterCustomization class to exist inside the scene,
    //that i can access from anywhere
    public class CharacterCustomization : Singleton<CharacterCustomization> 
    {

        public SkinnedMeshRenderer target;
        public string suffixMax = "Max", suffixMin = "Min";

        //private constructor in order to prevent access to the constructor from any class.
        //It can only be done through the singleton
        private CharacterCustomization() { }

        private SkinnedMeshRenderer skmr;
        private Mesh mesh;

        private Dictionary<string, Blendshape> blendShapeDatabase = new Dictionary<string, Blendshape>();
        private Dictionary<string, float> savedBlendshapeValuesDatabase = new Dictionary<string, float>();


        private void Start()
        {
            //Saves target's skinnedmeshrenderer component in "skmr" variable and its mesh in "mesh" variable and parses 
            //blendshapes into "blendShapeDatabase" by giving it the blendshape name without the suffixes and a variable of
            //type "blendshape" from the class "Blendshape" that i created, which contains the positive and negative index of
            //the blendshape. Positive is for Max suffix and negative for Min suffix
            Initialize();
        }


        #region Public Functions

        public void Initialize()
        {
            skmr = target;
            mesh = skmr.sharedMesh;

            ParseBlendShapesToDictionary();
        }


        public void ChangeBlendshapeValue(string blendshapeName, float value)
        {
            if (!blendShapeDatabase.ContainsKey(blendshapeName)) 
            { 
                Debug.LogError("Blendshape " + blendshapeName + " does not exist!"); 
            }

            //Restricts value between this range. If value is within range it returns the value as is. If it is less than minimum
            //returns the minimum (-100) and if more than maximum returns max which is 100
            value = Mathf.Clamp(value, -100, 100);

            Blendshape blendshape = blendShapeDatabase[blendshapeName];
        
            //If statement to save in dictionary only the values of the blendshapes that have changed. The rest are 0 by default
            //in the inspector.
            if (!savedBlendshapeValuesDatabase.ContainsKey(blendshapeName))
                savedBlendshapeValuesDatabase.Add(blendshapeName, value);
            else
                savedBlendshapeValuesDatabase[blendshapeName] = value;

            //If slider move to the right between 0 and 100 which is the max suffix of the blendshape
            if (value >= 0)
            {
                //Since it is a positive value, first check if positive index exists(meaning if blendshape with
                //max suffix exists, or just a single blendshape with no suffixes). If not then return. No need to check
                //for negativeIndex since the value was positive.
                if (blendshape.positiveIndex == -1) 
                    return;
                //Set blendshape value for blendshape in this index. Could be blendshape with Max or no suffix.
                skmr.SetBlendShapeWeight(blendshape.positiveIndex, value);

                //If negativeIndex is -1 then the blendshape has no suffix, but if it is not -1,
                //then min suffix exists and so it sets it to 0
                if (blendshape.negativeIndex == -1) 
                    return;
                skmr.SetBlendShapeWeight(blendshape.negativeIndex, 0);
            }
            //If slider move to the left between -100 and -1 which is the min suffix of the blendshape
            else
            {
                if (blendshape.negativeIndex == -1) 
                    return;
                skmr.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                if (blendshape.positiveIndex == -1) 
                    return;
                skmr.SetBlendShapeWeight(blendshape.positiveIndex, 0);
            }

        }        

        #endregion

        #region Private Functions

        private void ParseBlendShapesToDictionary()
        {
            //Get all blendshape names.
            //Loop through all blendshapes and passes the names to a IEnumerable<String>, which then converts to a List<string>
            List<string> blendshapeNames = Enumerable.Range(0, mesh.blendShapeCount).Select(x => mesh.GetBlendShapeName(x)).ToList();

            //BlendshapeNames.Count decreases with each loop, by removing list items at the end of the loop.
            //So when it hits 0 count the loop ends.
            //The i variable also don't need to increment because at the end of the loop when i remove the current list item,
            //which is at 0 position, the next item on the list moves to the 0 index position, so the i variable is always 0.
            //It is like this because "foreach" wouldn't work so well because i want to treat both Max and Min blendshapes as one,
            //and "foreach" would loop through those separately and also some blendshapes could have max and min and some may
            //not have any suffix and just have positive values (slider goes from 0 to 100 instead of -100 to 100).
            //Normal "for(i=0;i<blendshapeNames.Count;i++)" would also not work so well for the same reasons and also max and min of
            //a specific blendshape could be in random positions and not in sequence inside the list, and even if i used an if condition
            //to check if blendshapes have max min or not to increment with +1 or +2 accordingly, i would also need to find where the 
            //max and min are inside the list and skip those positions. For example if CheekMax is in position 3 and CheekMin in position 6
            //i would need to increment +1 from 3 to 4 and then skip position 6 or if they are in sequence, for example in position 3 and 4,
            //i would need to increment +2 and Cheek had only one blendshape with no max and min just increment +1.
            //So it is better and more clean to just remove the used blendshapes from the list.
            for (int i = 0; blendshapeNames.Count > 0;)
            {
                //altSuffix saves the opposite blendshapename of the current name(if current is i.e. CheekMax, it saves CheekMin)
                //noSuffix just trims everything and leaves only the name i.e. Cheek
                string altSuffix, noSuffix; 

                string positiveName = string.Empty, negativeName = string.Empty;

                // bool to check if there is an opposite blendshape name to the current blendshape,
                // in case some blendshapes only have Max and no Min, so slider goes from 0 to 100 instead of -100 to 100
                bool exists = false;

                int positiveIndex = -1, negativeIndex = -1;

                //Removes the max and min suffixes 
                noSuffix = blendshapeNames[i].TrimEnd(suffixMax.ToCharArray()).TrimEnd(suffixMin.ToCharArray()).Trim();

                //If Suffix is Positive (Max)
                if (blendshapeNames[i].EndsWith(suffixMax))
                {
                    altSuffix = noSuffix + " " + suffixMin;

                    positiveName = blendshapeNames[i];
                    negativeName = altSuffix;

                    if (blendshapeNames.Contains(altSuffix)) 
                        exists = true;

                    positiveIndex = mesh.GetBlendShapeIndex(positiveName);

                    if (exists)
                        negativeIndex = mesh.GetBlendShapeIndex(altSuffix);
                }

                //If Suffix is Negative (Min)
                else if (blendshapeNames[i].EndsWith(suffixMin))
                {
                    altSuffix = noSuffix + " " + suffixMax;

                    negativeName = blendshapeNames[i];
                    positiveName = altSuffix;

                    if (blendshapeNames.Contains(altSuffix)) 
                        exists = true;

                    negativeIndex = mesh.GetBlendShapeIndex(negativeName);

                    if (exists)
                        positiveIndex = mesh.GetBlendShapeIndex(altSuffix);
                }

                //Doesn't have a suffix. If no min max suffixes exist but only a single blendshape then it is saved
                //in positiveName and positiveIndex
                else
                {
                    positiveIndex = mesh.GetBlendShapeIndex(blendshapeNames[i]);
                                                         
                    positiveName = noSuffix; //This is here so it will remove it (for loop condition) so it's not infinite loop.   
                }


                if (blendShapeDatabase.ContainsKey(noSuffix))
                    Debug.LogError(noSuffix + " already exists within the Database!");

                blendShapeDatabase.Add(noSuffix, new Blendshape(positiveIndex, negativeIndex));


                //Remove selected indexes from the list, in order for the loop to end
                if (positiveName != string.Empty) 
                    blendshapeNames.Remove(positiveName);
                if (negativeName != string.Empty) 
                    blendshapeNames.Remove(negativeName);

            }//End of Loop
        }

        #endregion

        //Get all registered Blendshapes names without the suffixes (The Dictionary Keys)
        public string[] GetBlendShapeNames()
        {
            return blendShapeDatabase.Keys.ToArray(); //Create array from the dictionary keys
        }

        //Get the count of all registered Blendshapes without suffixes (Meaning max and min are treated as one)
        public int GetNumberOfEntries()
        {
            return blendShapeDatabase.Count;
        }

        //Get single blendshape by name
        public Blendshape GetBlendshape(string name)
        {
            return blendShapeDatabase[name];
        }

        //Use for editor to check if the Target has been changed so needs to update accordingly
        public bool DoesTargetMatchSkmr()
        {
            return target == skmr; //if target == skmr return true else return false
        }

        public void ClearDatabase()
        {
            blendShapeDatabase.Clear();
        }



        //Function to load saved blendshapes to the character inside the GameScene
        public void LoadBlendshapesToCharacter(SkinnedMeshRenderer skmr)
        {
            Blendshape blendshape;
            float value;

            //Loop through dictionary which has the saved settings for the blendshapes. 
            foreach (KeyValuePair<string, float> item in savedBlendshapeValuesDatabase) 
            {
                value = item.Value; //Get saved blendshape float value from current item
                blendshape = blendShapeDatabase[item.Key]; //Get blendshape from blendShapeDatabase dictionary

                if (value >= 0)
                {
                    if (blendshape.positiveIndex == -1) 
                        return;
                    skmr.SetBlendShapeWeight(blendshape.positiveIndex, value);
                    if (blendshape.negativeIndex == -1) 
                        return;
                    skmr.SetBlendShapeWeight(blendshape.negativeIndex, 0);
                }

                else
                {
                    if (blendshape.negativeIndex == -1) 
                        return;
                    skmr.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                    if (blendshape.positiveIndex == -1) 
                        return;
                    skmr.SetBlendShapeWeight(blendshape.positiveIndex, 0);
                }


            }
        }
    } 

