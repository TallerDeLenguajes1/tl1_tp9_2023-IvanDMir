﻿using Bitcoin;
using System.Text.Json;
internal class Program {

    public static async Task<BTC?> GetRoot() {
        var URL = "https://api.coindesk.com/v1/bpi/currentprice.json";
        using(HttpClient cliente = new HttpClient()) {
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage respuesta = await cliente.GetAsync(URL);
            if(respuesta.IsSuccessStatusCode) {
                var respondeBody = await respuesta.Content.ReadAsStringAsync();
                var TRY = JsonSerializer.Deserialize<BTC>(respondeBody);
                return TRY;
            }
            else {
                return null;
            }
        }
    }

    private static void PrintValues(BTC root) {
        Console.WriteLine("-----PRECIOS DE MONEDAS-----");
        Console.WriteLine("EUR: $" + root.bpi.EUR.rate_float);
        Console.WriteLine("GBP: $" + root.bpi.GBP.rate_float);
        Console.WriteLine("USD: $" + root.bpi.USD.rate_float);
    }

    private static void Main(string[] args) {
        int Copcion;
        BTC? root = GetRoot().Result;
        if(root == null) {
            Console.WriteLine("No se pudo procesar el request");
            System.Environment.Exit(1);
        }
        PrintValues(root);
        Console.WriteLine("\nSeleccionar una moneda para mostrar su informacion\n1-EUR\n2-GBP\n3-USD");
        string? Opcion = Console.ReadLine();
        while(!int.TryParse(Opcion, out Copcion)) {
            Console.WriteLine("\nIngresar una opcion valida");
            Opcion = Console.ReadLine();
        }
        Bitcoin.Type tipo= (Bitcoin.Type)Copcion;
        Console.WriteLine("\n-----INFORMACION-----");
        switch(tipo) {
            case Bitcoin.Type.EUR:
                Console.WriteLine("Codigo: " + root.bpi.EUR.code);
                Console.WriteLine("Simbolo: " + root.bpi.EUR.symbol);
                Console.WriteLine("Frecuencia: " + root.bpi.EUR.rate);
                Console.WriteLine("Descripcion: " + root.bpi.EUR.description);
                Console.WriteLine("Precio: " + root.bpi.EUR.rate_float);
                break;
            case Bitcoin.Type.GBP:
                Console.WriteLine("Codigo: " + root.bpi.GBP.code);
                Console.WriteLine("Simbolo: " + root.bpi.GBP.symbol);
                Console.WriteLine("Frecuencia: " + root.bpi.GBP.rate);
                Console.WriteLine("Descripcion: " + root.bpi.GBP.description);
                Console.WriteLine("Precio: " + root.bpi.GBP.rate_float);
                break;
            case Bitcoin.Type.USD:
                Console.WriteLine("Codigo: " + root.bpi.USD.code);
                Console.WriteLine("Simbolo: " + root.bpi.USD.symbol);
                Console.WriteLine("Frecuencia: " + root.bpi.USD.rate);
                Console.WriteLine("Descripcion: " + root.bpi.USD.description);
                Console.WriteLine("Precio: " + root.bpi.USD.rate_float);
                break;
            default:
                Console.WriteLine("\nNo se encontro la moneda\n");
                break;            
        }
    }
}