using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotizBuchClient
{
    public class Client
    {

        const string MainUrl = "https://localhost:44381/";

        public string User { get; set; }
        public string Password { get; set; }

        public string AuthHeader { get; set; }

        public void DeleteNote(int id)
        {
            Request($"notes/delete/{id}");
        }

        public void AddNote(Notes notes)
        {
            var playerInfo = new Dictionary<string, object>
                        {
                            { "Username", notes.Username},
                            { "CreationTime",  notes.CreationTime},
                            { "Title",  notes.Title},
                            { "Text",  notes.Text},
                         };

            RequestPost("notes/insert", JsonSerializer.Serialize(playerInfo));
        }


        public void EditNote(Notes notes)
        {
            var playerInfo = new Dictionary<string, object>
                        {
                            { "Id", notes.Id},
                            { "Username", notes.Username},
                            { "CreationTime",  notes.CreationTime},
                            { "Title",  notes.Title},
                            { "Text",  notes.Text},
                         };

            RequestPost("notes/edit", JsonSerializer.Serialize(playerInfo));
        }


        public Notes GetNote(int id)
        {
            return JsonSerializer.Deserialize<Notes>(Request($"notes/get/{id}"));
        }

        public List<Notes> GetAllNotes()
        {
            return JsonSerializer.Deserialize<List<Notes>>(Request("notes/getAll"));
        }


        public void CreateAccount(string username, string password)
        {
            var playerInfo = new Dictionary<string, object>
                        {
                            { "Username", username},
                            { "Password",  password}
                         };

            RequestPost("auth/CreateAccount", JsonSerializer.Serialize(playerInfo));
        }

        // Sendet eine GET Anfrage zu dem Server und wiederholt diese x mal bei Scheitern
        public string Request(string url, int retrys = 0)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                try
                {
                    client.BaseAddress = new Uri(MainUrl);
                    if(AuthHeader != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHeader);
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    // Falls kein AuthHeader vorhanden einen neuen Anfragen und die Anfrage erneut ausführen
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        AuthUser();
                        if (retrys >= 0)
                            return Request(url, retrys--);
                    }
                    return result;
                }
                catch (AggregateException e)
                {
                    Console.WriteLine("\n Exception Caught!");
                    if (retrys > 0)
                        return Request(url, retrys--);
                    else return "";
                }
            }
        }


        // Sendet eine POST Anfrage zu dem Server und wiederholt diese x mal bei Scheitern
        public string RequestPost(string url, string values, int retrys = 0)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                try
                {
                    var data = new StringContent(values, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthHeader);
                    HttpResponseMessage response = client.PostAsync(MainUrl + url, data).Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    // Falls kein AuthHeader vorhanden einen neuen Anfragen und die Anfrage erneut ausführen
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        AuthUser();
                        if(retrys >= 0)
                            return RequestPost(url, values, retrys--);
                    }
                    return result;
                }
                catch (AggregateException e)
                {
                    Console.WriteLine("\n Exception Caught!");
                    if (retrys > 0)
                        return RequestPost(url, values, retrys--);
                    else return "";
                }
            }
        }

        // Authentifiziert sich beim Server und speichert einen JWT Token bei erfolg ab
        public bool AuthUser(int retrys = 5)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                try
                {
                    var userCredentials = new Dictionary<string, object>
                    {
                        { "Username", User},
                        { "Password", Password }

                    };
                    var data = new StringContent(JsonSerializer.Serialize(userCredentials), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(MainUrl + "auth/auth", data).Result;

                    AuthHeader = response.Content.ReadAsStringAsync().Result;
                    if (AuthHeader != "")
                    {
                        return true;
                    }
                }
                catch (AggregateException e)
                {
                    Console.WriteLine("\n Exception Caught!");
                    if (retrys > 0)
                        return AuthUser(retrys--);
                    else return false;
                }
            }
            return false;
        }
    }
}
