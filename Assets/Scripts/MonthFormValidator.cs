
public class MonthFormValidator : Validator
{
    
    public override bool _Validate()
    {
        bool val = false;
        if (_input.text != string.Empty)
        {
            try
            {
                fieldValue = int.Parse(_input.text);
                if (fieldValue > 0 && fieldValue <= 12)
                {
                    val = true;
                }

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
