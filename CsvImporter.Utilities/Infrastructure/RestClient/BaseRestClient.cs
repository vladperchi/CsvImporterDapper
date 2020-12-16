using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Utilities.Infrastructure.RestClient
{
	public class BaseRestClient
	{
		protected readonly string BaseUri;
		public BaseRestClient(string baseUri)
		{
			BaseUri = baseUri;
		}
		protected async Task Post<TIn>(TIn entity, string requestUri, IDictionary<string, string> additionalHeaders = null)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseUri);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					if (additionalHeaders != null)
					{
						foreach (var additionalHeader in additionalHeaders)
						{
							client.DefaultRequestHeaders.Add(additionalHeader.Key, additionalHeader.Value);
						}
					}
					using (var request = await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")))
					{
						if (request.IsSuccessStatusCode)
						{
							return;
						}
						else
						{
							var response = (JObject)JsonConvert.DeserializeObject(await request.Content.ReadAsStringAsync());
							throw new HttpRequestException(response.Value<string>("message"));
						}
					}
				}
			}
			catch (HttpRequestException hrex)
			{
				throw hrex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected async Task<TOut> Post<TOut, TIn>(TIn entity, string requestUri, IDictionary<string, string> additionalHeaders = null)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					httpClient.Timeout = TimeSpan.FromMinutes(60);
					httpClient.BaseAddress = new Uri(BaseUri);
					httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					if (additionalHeaders != null)
					{
						foreach (var additionalHeader in additionalHeaders)
							httpClient.DefaultRequestHeaders.Add(additionalHeader.Key, additionalHeader.Value);
					}
					using (var response = await httpClient.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")))
					{
						response.EnsureSuccessStatusCode();
						var responseBody = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<TOut>(responseBody);
					}
				}
			}
			catch (ApplicationException aex)
			{
				throw aex;
			}
			catch (HttpRequestException hrex)
			{

				throw hrex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected async Task<TOut> Get<TOut>(string requestUri, IDictionary<string, string> aditionalHeaders = null)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					httpClient.BaseAddress = new Uri(BaseUri);
					httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					if (aditionalHeaders != null)
					{
						foreach (var aditionalHeader in aditionalHeaders)
							httpClient.DefaultRequestHeaders.Add(aditionalHeader.Key, aditionalHeader.Value);
					}

					using (var response = await httpClient.GetAsync(requestUri))
					{
						response.EnsureSuccessStatusCode();

						var responseBody = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<TOut>(responseBody);
					}
				}
			}
			catch (HttpRequestException hrex)
			{
				throw hrex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected async Task<TOut> Post<TOut>(string requestUri, IDictionary<string, string> additionalHeaders = null)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseUri);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					if (additionalHeaders != null)
					{
						foreach (var addiotionalHeader in additionalHeaders)
						{
							client.DefaultRequestHeaders.Add(addiotionalHeader.Key, addiotionalHeader.Value);
						}
					}
					using (var response = await client.PostAsync(requestUri, null))
					{
						response.EnsureSuccessStatusCode();
						var responseBody = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<TOut>(responseBody);
					}
				}
			}
			catch (HttpRequestException hrex)
			{
				throw hrex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
