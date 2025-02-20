using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backoffice.Client;

Console.WriteLine("Please enter username");
string username = "admin";
Console.WriteLine("Please enter password");
string password = "admin";  

var adminModel = new AdminModel
{
    Username = username,
    Password = password
};
var client = new HttpClient();
var jsonContent = JsonSerializer.Serialize(adminModel);
var v = await client.GetAsync("http://localhost:5109/api/auth/authenticate");
var content = new StringContent(jsonContent);
var responce = await client.PostAsync("http://localhost:5109/api/auth/authenticate", content);

string s = "Hello World!";

Console.WriteLine(s);