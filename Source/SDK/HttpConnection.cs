using System;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.IO;
using System.Globalization;
using PayPal.Log;

namespace PayPal.Api
{
    /// <summary>
    /// Stores details related to an HTTP request.
    /// </summary>
    public class RequestDetails
    {
        /// <summary>
        /// Gets or sets the URL for the request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method verb used for the request.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the headers used in the request.
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets the request body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the number of retry attempts for sending an HTTP request.
        /// </summary>
        public int RetryAttempts { get; set; }

        /// <summary>
        /// Resets the state of this object and clears its properties.
        /// </summary>
        public void Reset()
        {
            this.Url = string.Empty;
            this.Headers = null;
            this.Body = string.Empty;
            this.RetryAttempts = 0;
        }
    }

    /// <summary>
    /// Stores details related to an HTTP response.
    /// </summary>
    public class ResponseDetails
    {
        /// <summary>
        /// Gets or sets the headers used in the response.
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets the response body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the response HTTP status code.
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets an exception related to the response.
        /// </summary>
        public ConnectionException Exception { get; set; }

        /// <summary>
        /// Resets the state of this object and clears its properties.
        /// </summary>
        public void Reset()
        {
            this.Headers = null;
            this.Body = string.Empty;
            this.StatusCode = null;
            this.Exception = null;
        }
    }

    /// <summary>
    /// Helper class for sending HTTP requests.
    /// </summary>
    internal class HttpConnection
    {
        private static Logger logger = Logger.GetLogger(typeof(HttpConnection));
        private Dictionary<string, string> config;

        /// <summary>
        /// Gets the HTTP request details.
        /// </summary>
        public RequestDetails RequestDetails { get; private set; }

        /// <summary>
        /// Gets the HTTP response details.
        /// </summary>
        public ResponseDetails ResponseDetails { get; private set; }

        /// <summary>
        /// Initializes a new instance of <seealso cref="HttpConnection"/> using the given config.
        /// </summary>
        /// <param name="config">The config to use when making HTTP requests.</param>
        public HttpConnection(Dictionary<string, string> config)
        {
            this.config = config;
            this.RequestDetails = new RequestDetails();
            this.ResponseDetails = new ResponseDetails();
        }

        /// <summary>
        /// Copying existing HttpWebRequest parameters to newly created HttpWebRequest, can't reuse the same HttpWebRequest for retries.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="config"></param>
        /// <param name="url"></param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest CopyRequest(HttpWebRequest httpRequest, Dictionary<string, string> config, string url)
        {
            ConnectionManager connMngr = ConnectionManager.Instance;

            HttpWebRequest newHttpRequest = connMngr.GetConnection(config, url);
            newHttpRequest.Method = httpRequest.Method;
            newHttpRequest.Accept = httpRequest.Accept;
            newHttpRequest.ContentType = httpRequest.ContentType;
            if (httpRequest.ContentLength > 0)
            {
                newHttpRequest.ContentLength = httpRequest.ContentLength;
            }
            newHttpRequest.UserAgent = httpRequest.UserAgent;
            newHttpRequest.ClientCertificates = httpRequest.ClientCertificates;
            newHttpRequest = CopyHttpWebRequestHeaders(httpRequest, newHttpRequest);
            return newHttpRequest;
        }

        /// <summary>
        /// Copying existing HttpWebRequest headers into newly created HttpWebRequest
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="newHttpRequest"></param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest CopyHttpWebRequestHeaders(HttpWebRequest httpRequest, HttpWebRequest newHttpRequest)
        {
            string[] allKeys = httpRequest.Headers.AllKeys;
            foreach (string key in allKeys)
            {
                switch (key.ToLower(CultureInfo.InvariantCulture))
                {
                    case "accept":
                    case "connection":
                    case "content-length":
                    case "content-type":
                    case "date":
                    case "expect":
                    case "host":
                    case "if-modified-since":
                    case "range":
                    case "referer":
                    case "transfer-encoding":
                    case "user-agent":
                    case "proxy-connection":
                        break;
                    default:
                        newHttpRequest.Headers[key] = httpRequest.Headers[key];
                        break;
                }
            }
            return newHttpRequest;
        }

        /// <summary>
        /// Executing API calls
        /// </summary>
        /// <param name="payLoad"></param>
        /// <param name="httpRequest"></param>
        /// <returns>A string containing the response from the remote host.</returns>
        public string Execute(string payLoad, HttpWebRequest httpRequest)
        {
            // Reset the request & response details
            this.RequestDetails.Reset();
            this.ResponseDetails.Reset();

            // Store the request details
            this.RequestDetails.Body = payLoad;
            this.RequestDetails.Headers = httpRequest.Headers;
            this.RequestDetails.Url = httpRequest.RequestUri.AbsoluteUri;
            this.RequestDetails.Method = httpRequest.Method;

            try
            {
                do
                {
                  try
                  {
                      switch (httpRequest.Method)
                      {
                          case "POST":
                          case "PUT":
                          case "PATCH":
                              using (StreamWriter writerStream = new StreamWriter(httpRequest.GetRequestStream()))
                              {
                                  writerStream.Write(payLoad);
                                  writerStream.Flush();
                                  writerStream.Close();

                                  if (ConfigManager.IsLiveModeEnabled(config))
                                  {
                                      logger.Debug("Request details are hidden in live mode.");
                                  }
                                  else
                                  {
                                      logger.Debug(payLoad);
                                  }
                              }
                              break;

                          default:
                              break;
                      }

                      using (WebResponse responseWeb = httpRequest.GetResponse())
                      {
                          // Store the response information
                          this.ResponseDetails.Headers = responseWeb.Headers;
                          if(responseWeb is HttpWebResponse)
                          {
                              this.ResponseDetails.StatusCode = ((HttpWebResponse)responseWeb).StatusCode;
                          }

                          using (StreamReader readerStream = new StreamReader(responseWeb.GetResponseStream()))
                          {
                              this.ResponseDetails.Body = readerStream.ReadToEnd().Trim();

                              if (ConfigManager.IsLiveModeEnabled(config))
                              {
                                  logger.Debug("Response details are hidden in live mode.");
                              }
                              else
                              {
                                  logger.Debug("Service response: ");
                                  logger.Debug(this.ResponseDetails.Body);
                              }
                              return this.ResponseDetails.Body;
                          }
                      }
                  }
                  catch (WebException ex)
                  {
                      // If provided, get and log the response from the remote host.
                      var response = string.Empty;
                      if (ex.Response != null)
                      {
                          using (var readerStream = new StreamReader(ex.Response.GetResponseStream()))
                          {
                              response = readerStream.ReadToEnd().Trim();
                              logger.Error("Error response:");
                              logger.Error(response);
                          }
                      }
                      logger.Error(ex.Message);

                      ConnectionException rethrowEx = null;

                      // Protocol errors indicate the remote host received the
                      // request, but responded with an error (usually a 4xx or
                      // 5xx error).
                      if (ex.Status == WebExceptionStatus.ProtocolError)
                      {
                          var httpWebResponse = (HttpWebResponse)ex.Response;

                          // If the HTTP status code is flagged as one where we
                          // should continue retrying, then ignore the exception
                          // and continue with the retry attempt.
                          if(httpWebResponse.StatusCode == HttpStatusCode.GatewayTimeout ||
                             httpWebResponse.StatusCode == HttpStatusCode.RequestTimeout ||
                             httpWebResponse.StatusCode == HttpStatusCode.BadGateway)
                          {
                              continue;
                          }

                          rethrowEx = new HttpException(ex.Message, response, httpWebResponse.StatusCode, ex.Status, httpWebResponse.Headers, httpRequest);
                      }
                      else if(ex.Status == WebExceptionStatus.ReceiveFailure ||
                              ex.Status == WebExceptionStatus.ConnectFailure ||
                              ex.Status == WebExceptionStatus.KeepAliveFailure)
                      {
                          logger.Debug("There was a problem connecting to the server: " + ex.Status.ToString());
                          continue;
                      }
                      else if (ex.Status == WebExceptionStatus.Timeout)
                      {
                          // For connection timeout errors, include the connection timeout value that was used.
                          var message = string.Format("{0} (HTTP request timeout was set to {1}ms)", ex.Message, httpRequest.Timeout);
                          rethrowEx = new ConnectionException(message, response, ex.Status, httpRequest);
                      }
                      else
                      {
                          // Non-protocol errors indicate something happened with the underlying connection to the server.
                          rethrowEx = new ConnectionException("Invalid HTTP response: " + ex.Message, response, ex.Status, httpRequest);
                      }

                      if(ex.Response != null && ex.Response is HttpWebResponse)
                      {
                          var httpWebResponse = ex.Response as HttpWebResponse;
                          this.ResponseDetails.StatusCode = httpWebResponse.StatusCode;
                          this.ResponseDetails.Headers = httpWebResponse.Headers;
                      }

                      this.ResponseDetails.Exception = rethrowEx;
                      throw rethrowEx;
                  }
            }
            catch (PayPalException)
            {
                // Rethrow any PayPalExceptions since they already contain the
                // details of the exception.
                throw;
            }
            catch (System.Exception ex)
            {
                // Repackage any other exceptions to give a bit more context to
                // the caller.
                throw new PayPalException("Exception in PayPal.HttpConnection.Execute(): " + ex.Message, ex);
            }

            // If we've gotten this far, it means all attempts at sending the
            // request resulted in a failed attempt.
            throw new PayPalException("Exception in PayPal.HttpConnection.Execute(). Check log for more details.");
        }
    }
}
