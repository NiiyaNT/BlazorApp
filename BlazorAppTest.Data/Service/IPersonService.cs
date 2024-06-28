using BlazorAppTest.Data.Helper;
using BlazorAppTest.Data.Models;


namespace BlazorAppTest.Data.Service
{
    /// <summary>
    /// Define la interface para la obtencion de datos.
    /// </summary>
    public interface IPersonService
    {   
        /// <summary>
        /// Recupera  de la base de datos.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea contiene una lista de personas y pagos.</returns>
        Task<Result<IList<PersonViewModel>>> GetAllPersonsAsync();
        /// <summary>
        /// Recupera todas las personas con detalles de ingresos de la base de datos.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea contiene una lista de personas con detalles de ingresos.</returns>
        Task<Result<IList<PersonViewModel>>> GetPersonsByIncomeAsync();
        /// <summary>
        /// Recupera todas las personas con detalles de retiros de la base de datos.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea contiene una lista de personas con detalles de retiros.</returns>
        Task<Result<IList<PersonViewModel>>> GetPersonsByWithdrawalsAsync();
        /// <summary>
        /// Recupera el historial de todas las personas de la base de datos.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea contiene una lista de personas con su historial de transacciones.</returns>
        Task<Result<IList<PersonViewModel>>> GetPersonsByHistoryAsync();
        /// <summary>
        /// Agrega una persona con detalles de cuenta a la base de datos.
        /// </summary>
        /// <param name="id">El ID de la persona.</param>
        /// <param name="name">El nombre de la persona.</param>
        /// <param name="number">El número de la cuenta.</param>
        /// <param name="status">El estado de la cuenta.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea contiene la persona agregada.</returns>
        Task<Result<Person>> AddPersonWithAccountDetailAsync(int? id, string name, int number, string status);
    }
}
