using BlazorAppTest.Data.Helper;
using BlazorAppTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppTest.Data.Service
{
    /// <summary>
    /// Implementación de <see cref="IPersonService"/> que maneja las operaciones relacionadas con la obtencion de datos.
    /// </summary>
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PersonService"/>.
        /// </summary>
        /// <param name="context">El contexto de la base de datos de la aplicación.</param>
        public PersonService(AppDbContext context)
        {
            _context = context;
        }
        /// <inheritdoc/>
        public async Task<Result<IList<PersonViewModel>>> GetAllPersonsAsync()
        {
            try
            {
                var persons = await _context.Persons.Include(p => p.AccountDetails).ToListAsync();
                var personViewModels = persons.Select(p => new PersonViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AccountDetails = p.AccountDetails.Select(ad => new AccountDetailViewModel
                    {
                        Id = ad.Id,
                        Number = ad.Number,
                        Status = ad.Status,
                        DisplayNumber = ad.Status == "+" ? $"+{ad.Number}" : $"-{ad.Number}"
                    }).ToList(),
                    Total = p.AccountDetails.Count
                }).ToList();
                return Result<IList<PersonViewModel>>.Success(personViewModels);
            }
            catch (Exception ex)
            {
                return Result<IList<PersonViewModel>>.Failure(BlazorAppTest.Resource.Resource.ErrorAlObtenerPersonasExMessage);
            }
        }
        /// <inheritdoc/>
        public async Task<Result<IList<PersonViewModel>>> GetPersonsByIncomeAsync()
        {
            try
            {
                var persons = await _context.Persons.Include(p => p.AccountDetails).ToListAsync();
                var personViewModels = persons.Select(p => new PersonViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AccountDetails = p.AccountDetails
                        .Where(ad => ad.Status == "+")
                        .Select(ad => new AccountDetailViewModel
                        {
                            Id = ad.Id,
                            Number = ad.Number,
                            Status = ad.Status,
                            DisplayNumber = $"+{ad.Number}"
                        }).ToList(),
                    Total = p.AccountDetails.Where(ad => ad.Status == "+").Sum(ad => ad.Number)
                }).ToList();
                return Result<IList<PersonViewModel>>.Success(personViewModels);
            }
            catch (Exception ex)
            {
                return Result<IList<PersonViewModel>>.Failure(BlazorAppTest.Resource.Resource.ErrorAlObtenerPersonasConIngresosExMessage);
            }
        }
        /// <inheritdoc/>
        public async Task<Result<IList<PersonViewModel>>> GetPersonsByWithdrawalsAsync()
        {
            try
            {
                var persons = await _context.Persons.Include(p => p.AccountDetails).ToListAsync();
                var personViewModels = persons.Select(p => new PersonViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AccountDetails = p.AccountDetails
                        .Where(ad => ad.Status == "-")
                        .Select(ad => new AccountDetailViewModel
                        {
                            Id = ad.Id,
                            Number = ad.Number,
                            Status = ad.Status,
                            DisplayNumber = $"-{ad.Number}"
                        }).ToList(),
                    Total = p.AccountDetails.Where(ad => ad.Status == "-").Sum(ad => ad.Number)
                }).ToList();
                return Result<IList<PersonViewModel>>.Success(personViewModels);
            }
            catch (Exception ex)
            {
                return Result<IList<PersonViewModel>>.Failure(BlazorAppTest.Resource.Resource.ErrorAlObtenerPersonasConRetirosExMessage);
            }
        }
        /// <inheritdoc/>
        public async Task<Result<IList<PersonViewModel>>> GetPersonsByHistoryAsync()
        {
            try
            {
                var persons = await _context.Persons.Include(p => p.AccountDetails).ToListAsync();
                var personViewModels = persons.Select(p => new PersonViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    AccountDetails = p.AccountDetails.Select(ad => new AccountDetailViewModel
                    {
                        Id = ad.Id,
                        Number = ad.Number,
                        Status = ad.Status,
                        DisplayNumber = ad.Status == "+" ? $"+{ad.Number}" : $"-{ad.Number}"
                    }).ToList(),
                    Total = p.AccountDetails.Sum(ad => ad.Status == "+" ? ad.Number : -ad.Number)
                }).ToList();
                return Result<IList<PersonViewModel>>.Success(personViewModels);
            }
            catch (Exception ex)
            {
                return Result<IList<PersonViewModel>>.Failure(BlazorAppTest.Resource.Resource.ErrorAlObtenerElHistorialDePersonasExMessage);
            }
        }
        /// <inheritdoc/>
        public async Task<Result<Person>> AddPersonWithAccountDetailAsync(int? id, string name, int number, string status)
        {
            Person person = null;

            if (id == null || id == 0)
            {
                person = new Person { Name = name };
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
            }
            else
            {
                person = await _context.Persons.FindAsync(id);

                if (person == null)
                {
                    person = new Person { Name = name };
                    _context.Persons.Add(person);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (person.Name != name)
                    {
                        return Result<Person>.Failure(BlazorAppTest.Resource.Resource.ElIDProporcionadoNoCoincideConElNombreExistente);
                    }
                }
            }

            var accountDetail = new AccountDetail
            {
                Number = number,
                Status = status,
                DateAdded = DateTime.Now,
                PersonId = person.Id
            };

            _context.AccountDetails.Add(accountDetail);
            await _context.SaveChangesAsync();

            return Result<Person>.Success(person);
        }
    }
}


