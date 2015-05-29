using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public GameObject block;
 
	void Start () {
        HideBlock();
	}
	
	void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        HideBlock();
        //if (level == 13)
        //    print("Woohoo");

    }

    public void ShowBlock(){
        block.SetActive(true);

        Animator animator = block.GetComponent<Animator>();
        animator.Play("ShowBlockPanel");
    }

    public void HideBlock()
    {
        Animator animator = block.GetComponent<Animator>();
        animator.Play("HideBlockPanel");
        block.SetActive(false);
    }
}
