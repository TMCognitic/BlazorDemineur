using BlazorDemineur.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorDemineur.Pages
{
    public partial class Plateau
    {
        private readonly List<Case> _cases = [];

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private void OnFirstClick(Case c)
        {            
            for (int i = 0; i < 10; i++)
            {
                int index = Random.Shared.Next(_cases.Count);

                while ((Math.Abs(_cases[index].X - c.X) < 2 && Math.Abs(_cases[index].Y - c.Y) < 2) || _cases[index].Value == CaseValue.Bomb)
                {
                    index = Random.Shared.Next(_cases.Count);
                }

                Console.WriteLine($"une nouvelle bombe est définie en {_cases[index].X}:{_cases[index].Y}");
                _cases[index].SetAsBomb();
            }

            StateHasChanged();
        }

        private void OnNewCaseCreated(Case c)
        {
            _cases.Add(c);
        }

        private async Task OnCaseExplosion(Case c)
        {
            await JSRuntime.InvokeAsync<object>("alert", new[] { "Boom t'es mort!!" });
        }
    }
}
