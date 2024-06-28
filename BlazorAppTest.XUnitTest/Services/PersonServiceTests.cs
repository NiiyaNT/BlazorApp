using BlazorAppTest.Data.Models;
using BlazorAppTest.Data.Service;
using BlazorAppTest.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using BlazorAppTest.Data.Helper;

namespace BlazorAppTest.XUnitTest.Services
{
    public class PersonServiceTests
    {
        private readonly PersonService _personService;
        private readonly AppDbContext _context;
        private readonly ITestOutputHelper _output;

        public PersonServiceTests(ITestOutputHelper output)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _personService = new PersonService(_context);
            _output = output;

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            if (!_context.Persons.Any())
            {
                var persons = new List<Person>
                {
                    new Person { Id = 1, Name = "John Doe", AccountDetails = new List<AccountDetail>
                    {
                        new AccountDetail { Number = 100, Status = "+" },
                        new AccountDetail { Number = 50, Status = "-" }
                    }},
                    new Person { Id = 2, Name = "Jane Doe", AccountDetails = new List<AccountDetail>
                    {
                        new AccountDetail { Number = 200, Status = "+" }
                    }}
                };

                _context.Persons.AddRange(persons);
                _context.SaveChanges();
            }
        }

        private void OutputResult(Result<IList<PersonViewModel>> result)
        {
            foreach (var person in result.Value)
            {
                _output.WriteLine($"Person ID: {person.Id}, Name: {person.Name}");
                foreach (var detail in person.AccountDetails)
                {
                    _output.WriteLine($"    AccountDetail Number: {detail.Number}, Status: {detail.Status}");
                }
            }
        }

        [Fact]
        public async Task GetAllPersonsAsync_ReturnsAllPersons()
        {
            // Act
            var result = await _personService.GetAllPersonsAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            OutputResult(result);
        }

        [Fact]
        public async Task GetPersonsByIncomeAsync_ReturnsOnlyIncomeAccounts()
        {
            // Act
            var result = await _personService.GetPersonsByIncomeAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.All(result.Value, p => Assert.All(p.AccountDetails, ad => Assert.Equal("+", ad.Status)));
            Assert.Equal(300, result.Value.Sum(p => p.Total));
            OutputResult(result);
        }

        [Fact]
        public async Task GetPersonsByWithdrawalsAsync_ReturnsOnlyWithdrawalAccounts()
        {
            // Act
            var result = await _personService.GetPersonsByWithdrawalsAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.All(result.Value, p => Assert.All(p.AccountDetails, ad => Assert.Equal("-", ad.Status)));
            Assert.Equal(50, result.Value.Sum(p => p.Total));
            OutputResult(result);
        }

        [Fact]
        public async Task GetPersonsByHistoryAsync_ReturnsCorrectHistory()
        {
            // Act
            var result = await _personService.GetPersonsByHistoryAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal(50, result.Value.First(p => p.Id == 1).Total);
            Assert.Equal(200, result.Value.First(p => p.Id == 2).Total);
            OutputResult(result);
        }
    }
}
