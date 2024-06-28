
using Microsoft.AspNetCore.Components;
using MudBlazor;
using BlazorAppTest.Client.Model;
using BlazorAppTest.Data.Models;
using BlazorAppTest.Data.Service;
using BlazorAppTest.Data.Helper;
using BlazorAppTest.Client.Enum;

namespace BlazorAppTest.Client.Pages
{

    public partial class Home : ComponentBase
    {
        [Inject]
        private IPersonService PersonService { get; set; }
        private MudForm form;
        private List<PersonViewModel> persons;
        private PersonAccountViewModel personAccountViewModel = new PersonAccountViewModel();
        private FilterType selectedFilter = FilterType.TotalRecords;
        private readonly Dictionary<FilterType, Func<Task<Result<IList<PersonViewModel>>>>> filterActions;

        public Home()
        {
            filterActions = new Dictionary<FilterType, Func<Task<Result<IList<PersonViewModel>>>>>
        {
            { FilterType.TotalRecords, () => PersonService.GetAllPersonsAsync() },
            { FilterType.Income, () => PersonService.GetPersonsByIncomeAsync() },
            { FilterType.Withdrawals, () => PersonService.GetPersonsByWithdrawalsAsync() },
            { FilterType.History, () => PersonService.GetPersonsByHistoryAsync() }
        };
        }

        protected override async Task OnInitializedAsync()
        {
            await ApplyFilter();
        }

        protected async Task AddPersonWithAccountDetail()
        {
            await form.Validate();
            if (form.IsValid)
            {
                var result = await PersonService.AddPersonWithAccountDetailAsync(
                    personAccountViewModel.Id,
                    personAccountViewModel.Name,
                    personAccountViewModel.Number,
                    personAccountViewModel.Status
                );

                if (result.IsSuccess)
                {
                    await ApplyFilter();
                    await form.ResetAsync();
                }
                else
                {
                    Console.Error.WriteLine(result.Error);
                }
            }
        }

        private async Task ApplyFilter()
        {
            if (filterActions.TryGetValue(selectedFilter, out var action))
            {
                var result = await action();
                if (result.IsSuccess)
                {
                    persons = result.Value.ToList();
                }
                else
                {
                    Console.Error.WriteLine(result.Error);
                }
            }
        }

        private async Task OnFilterChanged(FilterType newFilter)
        {
            selectedFilter = newFilter;
            await ApplyFilter();
        }
    }
}
