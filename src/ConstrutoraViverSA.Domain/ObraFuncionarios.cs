namespace ConstrutoraViverSA.Domain
{
    public class ObraFuncionarios
    {
        public long ObraId { get; set; }
        public virtual Obra Obra { get; set; } 
        public long FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }   
    }
}