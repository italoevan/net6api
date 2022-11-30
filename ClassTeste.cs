namespace api
{
    public class ClassTeste
    {

        public int Valor { get; private set; }
        public List<int> Tester { get; private set; } = new List<int>() { 2,2};


        public ClassTeste(int valor)
        {
            this.Valor = valor;
        }
    }
}
