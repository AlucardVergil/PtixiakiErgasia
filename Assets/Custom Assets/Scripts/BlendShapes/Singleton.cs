using UnityEngine;

//Generic Singleton
//The T is a generic type that is replaced with any type. In this case T is replaced with CharacterCustomization
//from CharacterCustomization class with this line: public class CharacterCustomization : Singleton<CharacterCustomization>.
//where T : MonoBehaviour is a generic constraint that says that the generic T (in this case it is the class CharacterCustomization),
//must be of type Monobehaviour. Since CharacterCustomization inherits the Singleton class and the Singleton class inherits from
//MonoBehaviour, then CharacterCustomization class is also MonoBehaviour, but the constraint still needs to exist because in order to pass
//the generic T in the component methods like .GetComponent<T>, we need to state that the T is MonoBehaviour using the constraint.
//If not then it doesn't know what the T is and it throws an error
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    //Static variables are the same in all instances. So since singleton should have only one instance, i made this variable static
    //so that it's value will be universal for the class
    private static T instance; 


    public void Awake()
    {
        //If instance doesn't exist set the instance and if it does exist destroy the game object and only keep the first instance
        if (instance == null)
        {
            instance = GetComponent<T>();            
        }
        else        
            Destroy(gameObject);
    }

    //Creates Instance property with only "get" because we don't want to "set" the instance, it is done automatically
    public static T Instance
    {
        get
        {
            if (instance == null)
                print("Instance of GameObject does not exist!");

            return instance;
        }
    }
}
