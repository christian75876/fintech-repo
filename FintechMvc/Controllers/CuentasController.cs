using FintechLibrary.DTOs;
using FintechLibrary.services;
using FintechMvc.Class;
using FintechMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FintechMvc.Controllers;

public class CuentasController: Controller
{
    private readonly ApiCallerService _apiCaller;
    private readonly string _baseUrl;

    public CuentasController(ApiCallerService apiCaller, IOptions<ApiSettings> settings)
    {
        _apiCaller = apiCaller;
        _baseUrl = settings.Value.BaseUrl;
    }
    
    //Get: /cuentas
    public async Task<IActionResult> Index()
    {
        try
        {
            var cuentas = await _apiCaller.GetAsync<AccountDTO>($"{_baseUrl}/cuentas");
            return View(cuentas);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", $"Error al cargar las cuentas: {e.Message}");
            return View(new List<AccountDTO>());
        }
        
    }
    //Get: /cuentas/create
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CuentaViewModel vm)
    {
        TempData["SuccessMessage"] = "Vamos bien, procesando...";
        // if (!ModelState.IsValid)
        // {
        //     Console.WriteLine("Entro aca Mk!!");
        //     return View(vm);
        // }

         var dto = new AccountDTO
         {
             Id = 0,
             Balance = vm.Balance,
             AccountType = vm.AccountType,
             AccountNumber = vm.AccountNumber,
         };

         var request = new CuentaRequest
         {
             cuentaDto = dto,
         };
         
         Console.WriteLine($"dto: Id={dto.Id}, AccountNumber={dto.AccountNumber}, Balance={dto.Balance}, AccountType={dto.AccountType}");         try
         {
            var result = await _apiCaller.PostAsync($"{_baseUrl}/Cuentas", request);
           Console.WriteLine("mk result***********:",result);
            if(result != null) return RedirectToAction(nameof(Index));
         }
         catch (Exception e)
         {
             ModelState.AddModelError("", $"Error al cargar las cuentas: {e.Message}");
         }
         return View(vm);
    }

}