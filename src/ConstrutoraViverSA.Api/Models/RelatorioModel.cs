using ConstrutoraViverSA.Domain;
using System.Collections.Generic;

namespace ConstrutoraViverSA.Api.Models
{
    public class RelatorioModel
    {
        public List<Material> Materiais { get; set; }
        public List<Obra> Obras { get; set; }
        public List<Orcamento> Orcamentos { get; set; }
        public List<Funcionario> Funcionarios { get; set; }

        public RelatorioModel() { }
    }
}
