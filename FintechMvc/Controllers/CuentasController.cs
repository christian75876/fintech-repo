using FintechLibrary.DTOs;
using FintechLibrary.services;
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
         if (!ModelState.IsValid) return View(vm);

         var dto = new AccountDTO
         {
             Balance = vm.Balance,
             AccountType = vm.AccountType,
             AccountNumber = vm.AccountNumber,
         };
         
         try
         {
            var result = await _apiCaller.PostAsync($"{_baseUrl}/cuentas", dto);
            if(result != null) return RedirectToAction(nameof(Index));
         }
         catch (Exception e)
         {
             ModelState.AddModelError("", $"Error al cargar las cuentas: {e.Message}");
         }
         return View(vm);
    }

}