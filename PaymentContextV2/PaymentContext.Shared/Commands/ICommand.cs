namespace PaymentContext.Shared.Commands
{

    /*Criação de um command de entrada*/
    public interface ICommand
    {
        /*Tecnica fail, fast, valida -
        seria vc colocar as validações no command*/

        void Validate();
    }
}