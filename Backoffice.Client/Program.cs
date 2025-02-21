using System.Text;
using System.Text.Json;
using Backoffice.Client;

var client = new HttpClient();


await AuthenticateAdmin(client);

#region Client

while (true)
{
    Console.WriteLine("Please chose command");
    Console.WriteLine("1. Get Clients");
    Console.WriteLine("2. Create Client");
    Console.WriteLine("3. Update Client");
    Console.WriteLine("4. Delete Client");
    Console.WriteLine("5. Exit");
    var command = Console.ReadLine();
    switch (command)
    {
        case "1":
            await GetClients(client);
            break;
        case "2":
            break;
        case "3":
            break;
        case "4":
            break;
        case "5":
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }
    
}

#endregion Client

async Task AuthenticateAdmin(HttpClient client)
{
    Console.WriteLine("Authentication");
    string username = "admin";
    string password = "admin";  

    var adminModel = new AdminModel()
    {
        Username = username,
        Password = password
    };
    try
    {
        var jsonContent = JsonSerializer.Serialize(adminModel);
        // var v = await client.GetAsync("http://localhost:5109/api/auth");
        var content = new StringContent(jsonContent,Encoding.UTF8, "application/json");
        var responce = await client.PostAsync("http://localhost:5109/api/auth/authenticate", content);

        if (responce.IsSuccessStatusCode)
        {
            var responseContent = await responce.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<ResponseModel>(responseContent);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    
    }
}
async Task GetClients(HttpClient client)
{
    var response = await client.GetAsync("http://localhost:5109/api/client");
    
}