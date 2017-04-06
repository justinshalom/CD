
function setcache( variable, data )
{
    if ( typeof ( Storage ) !== "undefined" )
    {
        try
        {
            localStorage.removeItem( variable );
            localStorage.setItem( variable, data );
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

function getcache( variable )
{
    if ( typeof ( Storage ) !== "undefined" )
    {
        try
        {
            if ( localStorage.getItem( variable ) != null )
            {
                return localStorage.getItem( variable );
            }
        } catch ( e )
        {
            consoleit( msg );
            return false;
        }
    }
    return false;
}

function remcache( variable )
{
    if ( typeof ( Storage ) !== "undefined" )
    {
        try
        {
            localStorage.removeItem( variable );
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