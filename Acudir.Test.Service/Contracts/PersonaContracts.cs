using Acudir.Test.Data.Entities;
using Acudir.Test.Service.Model.Application.Common.Models;

namespace Acudir.Test.Service.Contracts
{
    public class AddPersonasResponse
    {
        public Result Result { get; set; }
    }

    public class GetPersonasResponse
    {
        public Result Result { get; set; }
        public List<Persona> Data { get; set; }
    }

    public class UpdatePersonaResponse
    {
        public Result Result { get; set; }
        public Persona Data { get; set; }
    }

    public class DeletePersonaResponse
    {
        public Result Result { get; set; }
    }
}
