using Acudir.Test.Data.Entities;
using Acudir.Test.Service.Contracts;

namespace Acudir.Test.Service.Interfaces
{
    public interface IPersonaService
    {

        public Task<GetPersonasResponse> GetAll();
        public Task<AddPersonasResponse> AddRange(List<Persona> personas);
        public Task<UpdatePersonaResponse> Update(Persona persona);
        public Task<DeletePersonaResponse> Delete(string nombreCompleto);
        public Task<GetPersonasResponse> GetAllByProfesion(string profesion);
    }
}
