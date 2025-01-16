using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FintechLibrary.services
{
    public class ApiCallerService
    {
        private readonly HttpClient _httpClient;

        public ApiCallerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var datos = await response.Content.ReadFromJsonAsync<List<T>>();
                if (datos == null || !datos.Any())
                {
                    throw new Exception("La respuesta del servidor fue vacía o no contenía datos.");
                }
                return datos;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al realizar GET en {endpoint}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }


        public async Task<T> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data);
                response.EnsureSuccessStatusCode(); 
                var datos = await response.Content.ReadFromJsonAsync<T>();
                return datos == null ? throw new Exception("La respuesta del servidor fue vacia o no contenia datos") : datos;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al realizar POST en {endpoint}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }
        
        public async Task<T> PutAsync<T>(string endpoint, T data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpoint, data);
                response.EnsureSuccessStatusCode();
                var datos = await response.Content.ReadFromJsonAsync<T>();
                return datos == null ? throw new Exception("La respuesta del servidor fue vacía o no contenía datos") : datos;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al realizar PUT en {endpoint}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }

        public async Task DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al realizar DELETE en {endpoint}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }

        
    }
}
