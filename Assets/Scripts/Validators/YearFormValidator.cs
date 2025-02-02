public class YearFormValidator : Validator
{
    
    public override bool _Validate()
    {
        bool val = false;
        if (_input.text.Length == 4)
        {
            try
            {
                fieldValue = int.Parse(_input.text);

                val = true;


            }
            catch
            {
                fieldValue = -1;

            }
        }


        if (val)
        {
            _onValidate?.Invoke();
        }
        else
        {
            _onInvalidate?.Invoke();
        }
        return val;
    }
}
