using System;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;


namespace ClassLibrary1
{
	[DataContract]
	public class Envelope
	{
		public async Task<Envelope> PostAsync(
				Uri uri,
				HttpMessageInvoker httpClient,
				CancellationToken cancellationToken = default)
		{
			return this;
		}
	}
}
