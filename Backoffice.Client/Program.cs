using System.Text;
using System.Text.Json;
using Backoffice.Client.Models.Admin;
using Backoffice.Client.Models.Client;

var client = new HttpClient();

var clients = new List<ClientResultModel>();

await AuthenticateAdmin(client);
bool exit = false;
while (!exit)
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
            clients = await GetClients(client);
            break;
        case "2":
            await AddClient(client);
            break;
        case "3":
            await UpdateClient(client, clients);
            break;
        case "4":
            break;
        case "5":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }
    
}

async Task UpdateClient(HttpClient client,  List<ClientResultModel> clients)
{
    int index = 1;
    foreach (var cl in clients)
    {
        Console.WriteLine($"{index} - {cl.Id} - {cl.FirstName} {cl.LastName}");
        index++;
    }
    Console.WriteLine("Please insert client number");
    while (true)
    {
        var clientIndex = Convert.ToInt32(Console.ReadLine());
        if (clientIndex == 0 || clientIndex > clients.Count - 1)
            continue;
        Console.WriteLine("Please insert client FirstName");
        var firstName = Console.ReadLine();
        Console.WriteLine("Please insert client LastName");
        var lastName = Console.ReadLine();
        UpdateClientModel model = new UpdateClientModel
        {
            Id = clients[clientIndex - 1].Id,
            FirstName = firstName,
            LastName = lastName
        };

    }
    
    
}

async Task AddClient(HttpClient client)
{
    Console.WriteLine("Please insert first name");
    var name = Console.ReadLine();
    Console.WriteLine("Please insert last name");
    var lastName = Console.ReadLine();
    var clientModel = new AddClientModel
    {
        FirnstName = name,
        LastName = lastName
    };
    var json = JsonSerializer.Serialize(clientModel);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await client.PostAsync("http://localhost:5109/api/client/createclient", content);
    if (response.IsSuccessStatusCode)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        
    }

}

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
async Task<List<ClientResultModel>> GetClients(HttpClient client)
{
    var response = await client.GetAsync("http://localhost:5109/api/client");
    if (response.IsSuccessStatusCode)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var clients = JsonSerializer.Deserialize<List<ClientResultModel>>(responseContent);

        foreach (var cl in clients)
        {
            Console.WriteLine(cl.Id +"\t" + cl.FirstName + "\t" + cl.LastName);
        }
        return clients;
    }

    return new List<ClientResultModel>();
}