using Acudir.Test.Data.Interfaces;
using Acudir.Test.Data.Entities;

namespace Acudir.Test.Data.Repositories
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        public PersonaRepository(string filePath) : base(filePath)
        {
        }

        public async Task<IList<Persona>> GetPersonasByProfesion(string profesion)
        {
            try
            {
                var personas = Entity.Where(p => p.Profesion.Contains(profesion, StringComparison.OrdinalIgnoreCase)).ToList();
               
                //Puedo aplicar otro tipo de lógica más compleja.
                //Cuando tengo una entidad que se relaciona con varias entiendades, es conveniente utilizar este patrón ya que el código queda más fácil de leer

                return personas;
            }
            catch (Exception ex)            
            {
                throw new Exception("Error al obtener las personas por profesión", ex);
            }
        }
    }
}
