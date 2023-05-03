using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.Toast.Configuration;

namespace BlazingChat.Shared
{
    public class UnitService : IUnitService
    {
    private readonly IToastService _toastService;
    private readonly HttpClient _http;
    private readonly IBananaService _bananaService;

       public UnitService(IToastService toastService, HttpClient http, IBananaService bananaService)
        {
            _toastService = toastService;
            _http = http;
            _bananaService = bananaService;
        }

       public IList<Unit> Units { get; set; } = new List<Unit>(){
           new Unit{Id=1,Title="Knight",Attack=10, Defense=10,BananaCost=100},
           new Unit{Id=2,Title="Archer",Attack=15, Defense=5,BananaCost=150},
            new Unit{Id=3,Title="Mage",Attack=20, Defense=1,BananaCost=200},
       };

        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();
        //IList<Unit> IUnitService.Units { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddUnit(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
             MyUnits.Add(new UserUnit{
                 UnitId=unit.Id,HitPoints=unit.HitPoints
             });
          Console.WriteLine($"{unit.Title} was built");
          Console.WriteLine($"Your Army size: {MyUnits.Count}");
            /* var result = await _http.PostAsJsonAsync<int>("api/userunit", unitId);
            if (true)result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
               await _bananaService.GetBananas(); */
                _toastService.ShowSuccess($"Your {unit.Title} has been built!", null);
            
        }

        public Task LoadUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task LoadUserUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task ReviveArmy()
        {
            throw new NotImplementedException();
        }
    } 
} 