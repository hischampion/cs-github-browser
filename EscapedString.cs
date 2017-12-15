using System;

/**
 * A class that lets you guarantee that your string
 * will be safe and explicity handle making it safe.
 */
public class EscapedString
{
    private String decorated;

	public EscapedString(String str)
	{
        this.decorated = str;
	}

    /**
     * Delayed string escape
     */
    public override string ToString()
    {
        return System.Security.SecurityElement.Escape(decorated);
    }
}
