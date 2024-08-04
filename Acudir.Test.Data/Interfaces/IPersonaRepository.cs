using Acudir.Test.Data.Entities;

namespace Acudir.Test.Data.Interfaces
{
    public interface IPersonaRepository : IGenericRepository<Persona>
    {
        /* Puedo definir métodos específicos para esta entidad (no genéricos), como por ejemplo:
         * 
         *
        */
        Task<IList<Persona>> GetPersonasByProfesion(string profesion);
    }
}
