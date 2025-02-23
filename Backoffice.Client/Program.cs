using System.Text;
using System.Text.Json;
using Backoffice.Client.Models.Admin;
using Backoffice.Client.Models.Client;
using Backoffice.Client.Models.Files;


var client = new HttpClient();
await AuthorizationAdmin(client);

var clients = new List<ClientResultModel>();
var exit = false;
while (!exit)
{
    Console.WriteLine("Please chose command");
    Console.WriteLine("1. Get Clients");
    Console.WriteLine("2. Create Client");
    Console.WriteLine("3. Update Client");
    Console.WriteLine("4. Delete Client");
    Console.WriteLine("5. Search client by name");
    Console.WriteLine("6. Add document for client");
    Console.WriteLine("7. Exit");
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
            await DeleteClient(client, clients);
            break;
        case "5":
            await SearchClientByName(client);
            break;
        case "6":
            await AddDocumentForCustomer(client, clients);
            break;
        case "7":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }
    
}

async Task AddDocumentForCustomer(HttpClient client, List<ClientResultModel> clients)
{
    Console.WriteLine("For example programs download file from \"C:\\Users\\Aleksandr\\OneDrive\\Desktop\\cv\\AleksandrMkrtumyan_CV.pdf\" ");
    int index = 1;
    foreach (var cl in clients)
    {
        Console.WriteLine($"{index} - {cl.Id} - {cl.FirstName} {cl.LastName}");
        index++;
    }

    int clientIndex = 0;
    while (true)
    {
        Console.WriteLine("Please select client");
        clientIndex = Convert.ToInt32(Console.ReadLine());
        if (clientIndex == 0 || clientIndex > clients.Count)
            continue;
        break;
    }

    Guid clientId = clients[clientIndex - 1].Id;
    string filePath = @"C:\Users\Aleksandr\OneDrive\Desktop\cv\AleksandrMkrtumyan_CV.pdf";  // Замените на путь к вашему файлу
    SaveFileModel saveFileModel = new SaveFileModel();
    try
    {
        FileInfo fileInfo = new FileInfo(filePath);
        saveFileModel.FileName = fileInfo.Name;
        saveFileModel.FileData = File.ReadAllBytes(filePath);
        saveFileModel.FileId = Guid.NewGuid();
        saveFileModel.ClientId = clientId;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    } 
    var jsonContent = JsonSerializer.Serialize(saveFileModel);
    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
    var response = await client.PostAsync("http://localhost:5109/api/client/addfile", content);

}
async Task DeleteClient(HttpClient client, List<ClientResultModel> clients)
{
    int index = 1;
    foreach (var cl in clients)
    {
        Console.WriteLine($"{index} - {cl.Id} - {cl.FirstName} {cl.LastName}");
        index++;
    }

    Console.WriteLine("Please choose client to delete");
    while (true)
    {
        var clientIndex = Convert.ToInt32(Console.ReadLine());
        if (clientIndex == 0 || clientIndex > clients.Count)
            continue;
        var response = await client.DeleteAsync($"http://localhost:5109/api/client/{clients[clientIndex - 1].Id}");
        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        else
        {
            Console.WriteLine("Error occurred while deleting the client");
        }

        break;
    }
}

async Task<List<ClientResultModel>> SearchClientByName(HttpClient client)
{
    Console.WriteLine("Insert client name");
    string name = Console.ReadLine(); 
    var response = await client.GetAsync($"http://localhost:5109/api/client/getclient/{name}");
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
        if (clientIndex == 0 || clientIndex > clients.Count)
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
        try
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5109/api/client/updateclient", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Client success updated");
            }
            else
            {
                Console.WriteLine("Client failed to update");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        break;
    }
}

async Task AuthorizationAdmin(HttpClient client)
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", model.AuthenticatedAdmin.AccessToken);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
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
        FirstName = name,
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