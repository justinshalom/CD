
function setsession( variable, data )
{
    if (typeof (sessionStorage) !== "undefined")
    {
        try
        {
            sessionStorage.removeItem(variable);
            sessionStorage.setItem(variable, data);
        } catch ( e )
        {
            consoleit( msg );
            return false;
        }
        return true;
    } else
    {
        return false;
    }
}

function getsession(variable)
{
    if (typeof (sessionStorage) !== "undefined")
    {
        try
        {
            if (sessionStorage.getItem(variable) != null)
            {
                return sessionStorage.getItem(variable);
            }
        } catch ( e )
        {
            consoleit( msg );
            return false;
        }
    }
    return false;
}

function remsession(variable)
{
    if (typeof (sessionStorage) !== "undefined")
    {
        try
        {
            sessionStorage.removeItem(variable);
        } catch ( e )
        {
            consoleit( msg );
            return false;
        }
        return true;
    } else
    {
        return false;
    }
}