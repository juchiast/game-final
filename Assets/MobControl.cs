using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Action
{
    private int _direction; // -1 0 1
    private int _numStep; // number of step to do this action
    public Action()
    {
        _direction = Random.Range(-1, 1);
        _numStep = Random.Range(15, 45);
    }

    public bool Update(GameObject target)
    {
        if (_numStep <= 0) return false;
        _numStep -= 1;
        var temp = target.transform.position;
        temp += 0.1f * _direction * Vector3.right;
        if (temp.x < -4) temp.x = -4;
        if (temp.x > 4) temp.x = 4;
        target.transform.position = temp;
        return true;
    }    
}

public class MobControl : MonoBehaviour
{
    public float speed = 0.1f;

    private Action[] _actions;

    private int _currentAction;
    // Start is called before the first frame update
    void Start()
    {
        _actions = new Action[30];
        _currentAction = 0;
        for (int i = 0; i < _actions.Length; i++)
        {
            _actions[i] = new Action();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.back * speed;
        if (_currentAction < _actions.Length)
        {
            if (!_actions[_currentAction].Update(gameObject))
            {
                _currentAction += 1;
            }
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(this);
    }
}
