using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Validator : MonoBehaviour
{
    //[SerializeField] protected Animator _anim;
    //[SerializeField] protected string _animParam = "StatNum";
    protected InputField _input;
    [SerializeField] protected UnityEvent _onValidate, _onInvalidate, _onReset;
    protected int fieldValue;
    public int FieldValue { get { return fieldValue; } }



    private void Awake()
    {
        _input = GetComponent<InputField>();

    }
    public abstract bool _Validate();
    public void _Reset()
    {
        _onReset?.Invoke();
    }
    //protected void _afterValidate()
    //{
    //    _anim.SetInteger(_animParam, 1);
    //}
    //public void _resetValidator()
    //{
    //    _anim.SetInteger(_animParam, 0);
    //}
}
