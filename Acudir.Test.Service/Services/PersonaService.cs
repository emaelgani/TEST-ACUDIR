using Acudir.Test.Data.Entities;
using Acudir.Test.Data.Interfaces;
using Acudir.Test.Service.Contracts;
using Acudir.Test.Service.Interfaces;
using Acudir.Test.Service.Model.Application.Common.Models;

namespace Acudir.Test.Service.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepo;
        public PersonaService(IPersonaRepository personaRepo)
        {
            _personaRepo = personaRepo;
        }

        public async Task<GetPersonasResponse> GetAll()
        {
            try
            {
                var personas = await _personaRepo.GetAllAsync();

                return new GetPersonasResponse()
                {
                    Result = Result.Success(),
                    Data = personas
                };
            }
            catch (Exception ex)
            {
                return new GetPersonasResponse()
                {
                    Result = Result.ExceptionFailure(ex),
                    Data = new()
                };
            }
        }

        public async Task<AddPersonasResponse> AddRange(List<Persona> personas)
        {
            try
            {
                #region validación
                var invalidPersonas = personas.Where(p => !p.IsValid()).ToList();
                if (invalidPersonas.Any())
                {
                    return new AddPersonasResponse()
                    {
                        Result = Result.Failure("Debe ingresar todos los campos de cada persona con la edad mayor o igual que 0."),
                    };
                }

                //Valido que ninguna persona ya exista
                foreach (var persona in personas)
                {
                    if (ExistePersona(persona.NombreCompleto))
                    {
                        return new AddPersonasResponse()
                        {
                            Result = Result.Failure($"La persona con el nombre: {persona.NombreCompleto} ya se encuentra registrada."),
                        };
                    }
                }

                #endregion

                _personaRepo.AddRange(personas);

                return new AddPersonasResponse()
                {
                    Result = Result.Success(),
                };
            }
            catch (Exception ex)
            {
                return new AddPersonasResponse()
                {
                    Result = Result.ExceptionFailure(ex),
                };
            }
        }

        public async Task<UpdatePersonaResponse> Update(Persona persona)
        {
            try
            {
                #region validaciones
                if (persona is null)
                {
                    return new UpdatePersonaResponse()
                    {
                        Result = Result.Failure("Debe proporcionar la persona que desea actualizar."),
                        Data = new()
                    };
                }
                if (!persona.IsValid())
                {
                    return new UpdatePersonaResponse()
                    {
                        Result = Result.Failure("Debe proporcionar todos los campos de la persona que desea actualizar."),
                        Data = new()
                    };
                }
                #endregion

                var personaBase = _personaRepo.FindByCondition(p => p.NombreCompleto == persona.NombreCompleto).FirstOrDefault();

                if (personaBase is null)
                {
                    return new UpdatePersonaResponse()
                    {
                        Result = Result.Failure($"No se encontró la persona con el nombre: {persona.NombreCompleto}."),
                        Data = new()
                    };
                }

                #region actualizo registro
                personaBase.Profesion = persona.Profesion;
                personaBase.Telefono = persona.Telefono;
                personaBase.Domicilio = persona.Domicilio;
                personaBase.Edad = persona.Edad; //La edad ya se encuenta validada.
                #endregion

                _personaRepo.Update(personaBase);

                return new UpdatePersonaResponse()
                {
                    Result = Result.Success("Registro actualizado correctamente."),
                    Data = personaBase
                };
            }
            catch (Exception ex)
            {
                return new UpdatePersonaResponse()
                {
                    Result = Result.ExceptionFailure(ex),
                    Data = new()
                };
            }
        }

        public async Task<DeletePersonaResponse> Delete(string nombreCompleto)
        {
            try
            {
                #region validaciones
                if (string.IsNullOrEmpty(nombreCompleto))
                {
                    return new DeletePersonaResponse()
                    {
                        Result = Result.Failure("Debe proporcionar el nombre completo de la persona que desea eliminar."),
                    };
                }
                if (!ExistePersona(nombreCompleto))
                {
                    return new DeletePersonaResponse()
                    {
                        Result = Result.Failure($"No se encontró la persona con el nombre: {nombreCompleto}."),
                    };
                }
                #endregion

                var personaBase = _personaRepo.FindByCondition(p => p.NombreCompleto == nombreCompleto).FirstOrDefault();
              
                _personaRepo.Remove(personaBase!);

                return new DeletePersonaResponse()
                {
                    Result = Result.Success($"La persona con el nombre: {nombreCompleto} se ha eliminado correctamente."),
                };
            }
            catch(Exception ex)
            {
                return new DeletePersonaResponse()
                {
                    Result = Result.ExceptionFailure(ex)
                };
            }
        }

        public async Task<GetPersonasResponse> GetAllByProfesion(string profesion)
        {
            try
            {
                #region validaciones
                if (string.IsNullOrEmpty(profesion))
                {
                    return new GetPersonasResponse()
                    {
                        Result = Result.Failure("Debe proporcionar la profesión para realizar la búsqueda."),
                        Data = new List<Persona>()
                    };
                }
                #endregion

                //De esta manera se encapsula la lógica en el el patrón repository
                var personas = await _personaRepo.GetPersonasByProfesion(profesion);

                return new GetPersonasResponse()
                {
                    Result = Result.Success(),
                    Data = (List<Persona>)personas 
                };
            }
            catch (Exception ex)
            {
                return new GetPersonasResponse()
                {
                    Result = Result.ExceptionFailure(ex),
                    Data = new()
                };
            }
        }


        private bool ExistePersona(string nombreCompleto)
        {
            var personaBase = _personaRepo.FindByCondition(p => p.NombreCompleto.Equals(nombreCompleto, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return personaBase != null;
        }
    }
}
