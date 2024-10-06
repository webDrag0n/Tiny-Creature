using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    public PlayerManager _playerManagers;
    public PlayerObj _nowObj;
    public int _moveStateSave = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerManagers._nowObj!=null)
        {
            if(_nowObj != _playerManagers._nowObj)
            {
                _nowObj =  _playerManagers._nowObj;
                SetMoveState(_moveStateSave);
            }
        }
    }

    public void SetMoveState (int num)
    {
        Debug.Log(num);
        if(_playerManagers._nowObj==null) return;
        _moveStateSave = num;
        Animator tAnim = _playerManagers._nowObj._prefabs._anim;

        switch(num)
        {
            case 0:
            _playerManagers._nowObj._charMS = 3f;
            tAnim.SetFloat("RunNumber",0);
            break;

            case 1:
            _playerManagers._nowObj._charMS = 6f;
            tAnim.SetFloat("RunNumber",0.5f);
            break;

            case 2:
            _playerManagers._nowObj._charMS = 10f;
            tAnim.SetFloat("RunNumber",1f);
            break;
        }
    }

    public void SetAnim(int num)
    {
        if(_playerManagers._nowObj==null) return;
        Animator tAnim = _playerManagers._nowObj._prefabs._anim;

        switch(num)
        {
            case 0: //Jump 1
            tAnim.SetFloat("ActionState",0.0f);
            tAnim.SetTrigger("Action");
            break;

            case 1: //Jump 2
            tAnim.SetFloat("ActionState",0.1f);
            tAnim.SetTrigger("Action");
            break;

            case 2: //Greeting 1
            tAnim.SetFloat("ActionState",0.2f);
            tAnim.SetTrigger("Action");
            break;

            case 3: //Greeting 2
            tAnim.SetFloat("ActionState",0.3f);
            tAnim.SetTrigger("Action");
            break;

            case 4: //Greeting 3
            tAnim.SetFloat("ActionState",0.4f);
            tAnim.SetTrigger("Action");
            break;

            case 5: // Sit Down
            tAnim.SetTrigger("SitDown");
            break;

            case 6: // Sit Up
            tAnim.SetTrigger("SitUp");
            break;

            case 7: // Sleep Down
            tAnim.SetTrigger("SleepDown");
            break;

            case 8: // Sleep Up
            tAnim.SetTrigger("SleepUp");
            break;

        }
    }
}
