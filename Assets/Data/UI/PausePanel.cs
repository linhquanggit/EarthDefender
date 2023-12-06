using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    //private GameController gameController;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        //gameController = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void BtnHome_Pressed()
    {
        if (playerController != null)
        {
            playerController.DestroyPlayer();
        }
        GameController.Instance.Home();
    }

    public void BtnContinue_Pressed()
    {
        GameController.Instance.Continue();
    }

}
