using System;
using System.Net.Http;

public class ClientFactory
{
	public ClientFactory()
	{
	}

    public HttpClient getClient()
    {
        return new HttpClient();
    }
}
