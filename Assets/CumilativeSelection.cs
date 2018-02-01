using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
[System.Serializable]
public class RandomObject
{
    public GameObject randomObj;
    public int priority;

}

public class CumilativeSelection : MonoBehaviour
{

    public List<RandomObject> objectList;
    System.Random rnd = new System.Random();

    public Text output;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        output.text = GameObject.Find("1").transform.childCount + " " + GameObject.Find("2").transform.childCount + " " + GameObject.Find("3").transform.childCount + " " + GameObject.Find("4").transform.childCount;
	}

    // This logic is straught forward but might not be currect always
    // 1. You pick a value from 1 to 10 (or may be 1 to 100 whatever)
    // 2. Probability of having 1 is 1/10, 2 is 1/10 and so on (Uniform Destribution) .So the
    //    Probability of having 1 to 4 is (1/10 + 1/10 + 1/10 + 1/10) = 0.4 or 40%
    // 3. Now you can spawn on a particular object with a 40% of probability.
    void EasyRandom() 
    {
       
        int val = rnd.Next(1, 10);
        if (val < 10)
        {
            //100% probability
        }

        else if (val < 6)
        {
            //50% probabilty
            // 1/10 + 1/10 + 1/10 + 1/10 + 1/10 + 1/10 = 5/10 = 0.5
        }

        else if (val < 2)
        {
            //10% probabilty
            //1/10 = 0.1
        }
    }


    // This logic is called the Cumilative Destribution Function. More accurate and a bit
    // complex. To learn more go to link: http://www.vcskicks.com/random-element.php This 
    // guy had made the explanation really easy. My logic is kind of similar to him with a few 
    // variation.

    GameObject GetObjectWithMaxProb()
    {
        int totalWeight = objectList.Sum(t => t.priority);
        print(totalWeight);
        int randomNumber = rnd.Next(0, totalWeight);

        GameObject myGameObject = null;
        foreach (RandomObject item in objectList)
        {
            if(randomNumber < item.priority)
            {
                myGameObject = item.randomObj;
                break;
            }
            randomNumber -= item.priority;
        }
        return myGameObject;
    }


    public void CloneObject()
    {
        string names = GetObjectWithMaxProb().name;
        GameObject clone = Instantiate(GetObjectWithMaxProb());
        clone.transform.SetParent(GameObject.Find(names).transform);
        clone.name = names;

    }
}
